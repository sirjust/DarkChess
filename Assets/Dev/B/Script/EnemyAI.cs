using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public List<GameObject> tempList = new List<GameObject>();
    public List<GameObject> tempList2 = new List<GameObject>();
    public List<Vector3> playerPos = new List<Vector3>();
    public int limitHealthPercent = 20;
    public GameObject closestTile;
    public GameObject furthermostTile;
    public float smallestDistance;
    public float largestDistance;

    private TurnSystem turnSystem;
    private GetStats getStats;
    private EditedGridGenerator gridGenerator;
    private AllSkills allSkills;
    private Card usedCard;
    private bool alreadyWent = false;
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
        if (turnSystem.GetBattleStatus() == BattleStatus.EnemyMove && !alreadyWent)
        {
            usedCard = PickRndCard(getStats.normalskills);
            ClearLists();
            EnemyMove();
        }
        else if (turnSystem.GetBattleStatus() == BattleStatus.EnemyCombat && !alreadyWent)
        {
            ClearLists();
            EnemyCombat();
        }
    }

    private void EnemyMove()
    {
        GameObject placeHolder = new GameObject();
        alreadyWent = true;

        int rotation = (360 - (360 - (int)this.transform.localEulerAngles.y)) / 90;

        for (int i = 0; i < 4; i++)
        {
            gridGenerator.GenerateSkillTiles(getStats.character.movementCard.ranges, getStats.character.movementCard.targetType, gameObject, TypesofValue.relative, false);

            tempList.AddRange(gridGenerator.rangeTiles.ToArray());

            RemoveDuplictas(tempList);

            CheckRotation(gameObject);
        }
        gridGenerator.DestroyTiles(DestroyOption.rangeTiles, true, false);

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
                            gridGenerator.selectedTiles.Add(temptile);
                        }
                }
                gridGenerator.DestroyTiles(DestroyOption.rangeTiles, true, false);
                CheckRotation(placeHolder);
            }
            placeHolder.transform.localEulerAngles = new Vector3(0, 270, 0);
        }

        playerPos = FindEnemyPos();

        int indexPlayerPos = Random.Range(0, playerPos.Count - 1);

        closestTile = tempList[0];
        furthermostTile = tempList[0];
        smallestDistance = Vector3.Distance(closestTile.transform.position, playerPos[indexPlayerPos]);
        largestDistance = Vector3.Distance(furthermostTile.transform.position, playerPos[indexPlayerPos]);

        foreach (GameObject rangeTile in tempList)
        {
            if (Vector3.Distance(rangeTile.transform.position, playerPos[indexPlayerPos]) < smallestDistance)
            {
                smallestDistance = Vector3.Distance(rangeTile.transform.position, playerPos[indexPlayerPos]);
                closestTile = rangeTile;
            }
            if (Vector3.Distance(rangeTile.transform.position, playerPos[indexPlayerPos]) > largestDistance)
            {
                largestDistance = Vector3.Distance(rangeTile.transform.position, playerPos[indexPlayerPos]);
                furthermostTile = rangeTile;
            }
        }

        if (limitHealthPercent != 0)
        {
            if (getStats.character.currentHealth < (getStats.character.health / 100) * limitHealthPercent)
            {
                tempList2.Add(furthermostTile);
            }
            else
            {
                if (gridGenerator.selectedTiles.Count == 0)
                    tempList2.Add(closestTile);

                tempList2.AddRange(gridGenerator.selectedTiles.ToArray());

            }
        }

        RemoveDuplictas(tempList2);
        gridGenerator.DestroyTiles(DestroyOption.all, true, false);

        for (int i = 0; i < 4; i++)
        {
            int indexTempList2 = Random.Range(0, tempList2.Count - 1);

            gridGenerator.selectedTiles.Add(tempList2[indexTempList2]);
            gridGenerator.GenerateSkillTiles(getStats.character.movementCard.ranges, getStats.character.movementCard.targetType, gameObject, TypesofValue.relative, false);
            if (allSkills.cast(getStats.character.movementCard, gridGenerator, gameObject, BattleStatus.EnemyMove))
            {
                alreadyWent = false;
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
                alreadyWent = false;
                return;
            }
            CheckRotation(gameObject);
        }
        if (gridGenerator.selectedTiles.Count == 0)
        {
            alreadyWent = false;
            turnSystem.NextTurn();
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
                if (getObjectonTile.gameObjectOnTile.GetComponent<GetStats>().character.relation != RelationType.Friendly)
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
        playerPos.Clear();
    }
}

