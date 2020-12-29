using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public List<GameObject> tempList = new List<GameObject>();

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
            EnemyMove();
        }
        else if (turnSystem.GetBattleStatus() == BattleStatus.EnemyCombat)
        {
            EnemyCombat();
        }
    }

    private void EnemyMove()
    {
        tracked = true;
        gridGenerator.GenerateSkillTiles(getStats.character.movementCard.ranges, getStats.character.movementCard.targetType, gameObject, TypesofValue.relative, false);

        tempList = gridGenerator.rangeTiles;
    }

    private void EnemyCombat()
    {

    }
}
