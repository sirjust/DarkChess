using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public List<GameObject> tempList = new List<GameObject>();
    public List<GameObject> tempList2 = new List<GameObject>();
    public List<Vector3> playerPos = new List<Vector3>();
    public int limitHealthPercent;
    public GameObject closestTile;
    public GameObject furthermostTile;
    public float smallestDistance;
    public float largestDistance;

    private TurnSystem turnSystem;
    private GetStats getStats;
    private EditedGridGenerator gridGenerator;
    private AllSkills allSkills;
    private Card usedCard;
    private bool tracked = false;
    private int rotation = 0;

    private void Awake()
    {
        allSkills = FindObjectOfType<AllSkills>();
        turnSystem = FindObjectOfType<TurnSystem>();
        gridGenerator = FindObjectOfType<EditedGridGenerator>();
        getStats = GetComponent<GetStats>();
    }

    private void Update()
    {
        if (turnSystem.GetBattleStatus() == BattleStatus.EnemyMove && !tracked)
        {
            usedCard = PickRndCard(getStats.normalskills);
            ClearLists();
            EnemyMove();
        }
        else if (turnSystem.GetBattleStatus() == BattleStatus.EnemyCombat && !tracked)
        {
            ClearLists();
            EnemyCombat();
        }
    }

    private void EnemyMove()
    {
        GameObject placeHolder = new GameObject();
        tracked = true;

        int rotation = (360 - (360 - (int)this.transform.localEulerAngles.y)) / 90;

        for (int i = 0; i < 4; i++)
        {
            gridGenerator.GenerateSkillTiles(getStats.character.movementCard.ranges, getStats.character.movementCard.targetType, gameObject, TypesofValue.relative, false);

            tempList.AddRange(gridGenerator.rangeTiles.ToArray());

            RemoveDuplictas(tempList);

            CheckRotation(gameObject);
        }
        gridGenerator.DestroyTiles(DestroyOption.rangeTiles, true, false);

        rotation = (360 - (360 - (int)this.transform.localEulerAngles.y)) / 90;

        foreach (GameObject temptile in tempList)
        {
            for (int m = 0; m < 4; m++)
            {
                placeHolder.transform.position = temptile.transform.position;
                gridGenerator.GenerateSkillTiles(usedCard.ranges, usedCard.targetType, placeHolder, TypesofValue.relative, false);
                foreach (GameObject rangeTile in gridGenerator.rangeTiles)
                {
                    if (rangeTile.GetComponent<GetObjectonTile>().gameObjectOnTile != null)
                        if (rangeTile.GetComponent<GetObjectonTile>().gameObjectOnTile.GetComponent<GetStats>())
                        {
                            Debug.Log("Found enenmy");
                            gridGenerator.selectedTiles.Add(temptile);
                        }
                }
                gridGenerator.DestroyTiles(DestroyOption.rangeTiles, true, false);
                CheckRotation(placeHolder);
            }
            placeHolder.transform.localEulerAngles = new Vector3(0, 270, 0);
        }
        tempList2.AddRange(gridGenerator.selectedTiles.ToArray());
        gridGenerator.DestroyTiles(DestroyOption.selectedTiles, true, false);

        if (tempList2.Count == 0)
        {
            playerPos = FindEnemyPos();

            int index = Random.Range(0, playerPos.Count - 1);

            closestTile = tempList[0];
            furthermostTile = tempList[0];
            smallestDistance = Vector3.Distance(closestTile.transform.position, playerPos[index]);
            largestDistance = Vector3.Distance(furthermostTile.transform.position, playerPos[index]);

            foreach (GameObject rangeTile in tempList)
            {
                if (Vector3.Distance(rangeTile.transform.position, playerPos[index]) < smallestDistance)
                {
                    smallestDistance = Vector3.Distance(rangeTile.transform.position, playerPos[index]);
                    closestTile = rangeTile;
                }
                if (Vector3.Distance(rangeTile.transform.position, playerPos[index]) > largestDistance)
                {
                    largestDistance = Vector3.Distance(rangeTile.transform.position, playerPos[index]);
                    furthermostTile = rangeTile;
                }
            }
            if (limitHealthPercent != 0)
            {
                Debug.LogError((getStats.character.health / 100) * limitHealthPercent);
                if (getStats.character.currentHealth < (getStats.character.health / 100) * limitHealthPercent)
                {
                    tempList2.Add(furthermostTile);
                    Debug.Log("Defense");
                }
                else
                {
                    tempList2.Add(closestTile);
                    Debug.Log("Aggresiv");
                }
            }
        }

        RemoveDuplictas(tempList2);
        
        gridGenerator.DestroyTiles(DestroyOption.all, true, false);
        
        for (int i = 0; i < 4; i++)
        {
            int index = Random.Range(0, tempList2.Count - 1);
            rotation = (360 - (360 - (int)this.transform.localEulerAngles.y)) / 90;

            gridGenerator.selectedTiles.Add(tempList2[index]);
            gridGenerator.GenerateSkillTiles(getStats.character.movementCard.ranges, getStats.character.movementCard.targetType, gameObject, TypesofValue.relative, false);
            if (allSkills.cast(getStats.character.movementCard, gridGenerator, gameObject, BattleStatus.EnemyMove))
            {
                break;
            }
            CheckRotation(gameObject);
        }
    }

    private void EnemyCombat()
    {
        rotation = (360 - (360 - (int)this.transform.localEulerAngles.y)) / 90;

        for (int i = 0; i < 4; i++)
        {
            gridGenerator.GenerateSkillTiles(usedCard.ranges, usedCard.targetType, gameObject, TypesofValue.relative, false);
            tempList.AddRange(gridGenerator.rangeTiles.ToArray());
            foreach (GameObject rangeTile in tempList)
            {
                if (rangeTile.GetComponent<GetObjectonTile>().gameObjectOnTile != null)
                {
                    if (rangeTile.GetComponent<GetObjectonTile>().gameObjectOnTile.GetComponent<GetStats>())
                    {
                        gridGenerator.selectedTiles.Add(rangeTile);
                    }
                }
            }
            if (allSkills.cast(usedCard, gridGenerator, gameObject, BattleStatus.EnemyCombat))
            {
                tracked = false;
                break;
            }
            CheckRotation(gameObject);
        }
    }

    public List<Vector3> FindEnemyPos()
    {
        List<Vector3> positions = new List<Vector3>();
        List<GetObjectonTile> getObjectonTiles = new List<GetObjectonTile>();
        
        getObjectonTiles.AddRange(FindObjectsOfType<GetObjectonTile>());

        foreach (GetObjectonTile getObjectonTile in getObjectonTiles)
        {
            if (getObjectonTile.gameObjectOnTile != null)
            {
                if (getObjectonTile.gameObjectOnTile.GetComponent<GetStats>().character.realtion != RealtionType.Friendly)
                {
                    positions.Add(getObjectonTile.gameObjectOnTile.transform.localPosition);
                }
            }
        }
        return positions;
    }

    public Card PickRndCard(Card[] cards)
    {
        int index = Random.Range(0, cards.Length - 1);
        return cards[index];
    }

    public void CheckRotation(GameObject target)
    {
        rotation++;
        if ((90 * rotation) > 270) rotation = 0;
        target.transform.localEulerAngles = new Vector3(0, 90 * rotation, 0);
    }

    public void RemoveDuplictas(List<GameObject> list)
    {
        for (int m = 0; m < list.Count; m++)
        {
            for (int n = m + 1; n < list.Count; n++)
            {
                if (list[m].transform.position == list[n].transform.position)
                {
                    list.Remove(list[n]);
                }
            }
        }
    }

    public void ClearLists()
    {
        tempList.Clear();
        tempList2.Clear();
    }
}
