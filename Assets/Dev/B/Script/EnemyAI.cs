using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public List<GameObject> tempList = new List<GameObject>();
    public List<GameObject> tempList2 = new List<GameObject>();

    private TurnSystem turnSystem;
    private GetStats getStats;
    private EditedGridGenerator gridGenerator;
    private AllSkills allSkills;
    private GameObject selectedTile;
    private bool tracked = false;

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
            ClearLists();
            EnemyMove();
        }
        else if (turnSystem.GetBattleStatus() == BattleStatus.EnemyCombat)
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

            RemoveDuplicates(tempList);

            rotation++;
            if ((90 * rotation) > 270) rotation = 0;

            this.transform.localEulerAngles = new Vector3(0, 90 * rotation, 0);
        }
        gridGenerator.DestroyTiles(DestroyOption.rangeTiles, true, false);

        rotation = (360 - (360 - (int)this.transform.localEulerAngles.y)) / 90;

        foreach (GameObject temptile in tempList)
        {
            for (int m = 0; m < 4; m++)
            {
                placeHolder.transform.position = temptile.transform.position;
                gridGenerator.GenerateSkillTiles(getStats.normalskills[0].ranges, getStats.normalskills[0].targetType, placeHolder, TypesofValue.relative, true);
                foreach (GameObject rangeTile in gridGenerator.rangeTiles)
                {
                    if (rangeTile.GetComponent<GetObjectonTile>().gameObjectOnTile != null)
                        if (rangeTile.GetComponent<GetObjectonTile>().gameObjectOnTile.GetComponent<GetStats>())
                        {
                            gridGenerator.selectedTiles.Add(temptile);
                        }
                }
                gridGenerator.DestroyTiles(DestroyOption.rangeTiles, true, false);

                rotation++;
                if ((90 * rotation) > 270) rotation = 0;
                placeHolder.transform.localEulerAngles = new Vector3(0, 90 * m, 0);
            }
            placeHolder.transform.localEulerAngles = new Vector3(0, 270, 0);
        }

        tempList2.AddRange(gridGenerator.selectedTiles.ToArray());

        RemoveDuplicates(tempList2);

        gridGenerator.DestroyTiles(DestroyOption.all, true, false);

        for (int i = 0; i < 4; i++)
        {
            int index = Random.Range(0, tempList2.Count);
            rotation = (360 - (360 - (int)this.transform.localEulerAngles.y)) / 90;

            gridGenerator.selectedTiles.Add(tempList2[index]);
            gridGenerator.GenerateSkillTiles(getStats.character.movementCard.ranges, getStats.character.movementCard.targetType, gameObject, TypesofValue.relative, true);
            if (allSkills.cast(getStats.character.movementCard, gridGenerator, gameObject, BattleStatus.EnemyMove))
            {
                break;
            }
            rotation++;
            if ((90 * rotation) > 270) rotation = 0;

            this.transform.localEulerAngles = new Vector3(0, 90 * rotation, 0);
        }
    }

    private void EnemyCombat()
    {

    }

    public void RemoveDuplicates(List<GameObject> list)
    {
        for (int m = 0; m < list.Count; m++)
        {
            for (int n = m + 1; n < list.Count; n++)
            {
                if (list[m].transform.position == list[n].transform.position)
                {
                    list.RemoveAt(n);
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

/*
 *   gridGenerator.GenerateSkillTiles(tempList, getStats.character.movementCard.targetType, gameObject, TypesofValue.relative, true);

            for (int m = 0; m < 4; m++)
            {
                int rotation2 = (360 - (360 - (int)this.transform.localEulerAngles.y)) / 90;
                foreach (GameObject rangeTiles in gridGenerator.rangeTiles)
                {
                    if (rangeTiles.GetComponent<GetObjectonTile>().gameObjectOnTile != null)
                    {
                        if (rangeTiles.GetComponent<GetObjectonTile>().gameObjectOnTile.GetComponent<GetStats>().character.realtion != RealtionType.Enemy)
                        {
                            gridGenerator.selectedTiles.Add(rangeTiles);
                        }
                    }

                }
                if (90 * m > 360) m = 0;
                this.transform.localEulerAngles = new Vector3(0, 90 * m, 0);
            }
*/