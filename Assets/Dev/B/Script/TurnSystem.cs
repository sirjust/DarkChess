using System;
using System.Collections;
using UnityEngine;

public enum BattleStatus
{
    PlayerMove, PlayerCombat, EnemyMove, EnemyCombat
}

public class TurnSystem : MonoBehaviour
{
    [Header("Required")]
    [SerializeField]
    private BattleStatus status;

    [Header("Assigned Automatically")]
    private EditedGridGenerator gridGenerator;

    private int index = 0;
    private int battleStatusLastIndex = Enum.GetNames(typeof(BattleStatus)).Length - 1;
    private GetStats[] getStats;

    private void Awake()
    {
        gridGenerator = FindObjectOfType<EditedGridGenerator>();
        index = (int)status;

        getStats = FindObjectsOfType<GetStats>();

        foreach (GetStats getStat in getStats)
        {
            getStat.character.realtion = getStat.character.startRelationType;
        }
    }

    private void Update()
    {
        index = (int)status;
    }

    public void NextTurn()
    {
        if (status == BattleStatus.PlayerCombat || status == BattleStatus.EnemyCombat) 
            SwitchRelation();

        gridGenerator.DestroyTiles(DestroyOption.all, true, true);

        if (index < battleStatusLastIndex) 
        {
            index++;
        }
        else index = 0;

        status = (BattleStatus)index;

        PrintBattleStatus();
    }

    public void BackTurn()
    {
        if (status == BattleStatus.PlayerMove || status == BattleStatus.EnemyMove)
            SwitchRelation();

        gridGenerator.DestroyTiles(DestroyOption.all, true, true);
        
        if (index > 0) index--;
        else index = battleStatusLastIndex;

        status = (BattleStatus)index;
    }

    public void SkipPlayerTurn()
    {
        if (status == BattleStatus.PlayerMove || status == BattleStatus.PlayerCombat)
            NextTurn();
    }

    public void SwitchRelation()
    {
        GetStats[] characters = FindObjectsOfType<GetStats>();
        foreach(GetStats character in characters)
        {
            if (character.character.realtion == RealtionType.Enemy)
                character.character.realtion = RealtionType.Friendly;
            else if(character.character.realtion == RealtionType.Friendly)
                character.character.realtion = RealtionType.Enemy;
        }
    }

    public void PrintBattleStatus()
    {
        Debug.Log($"BattleStatus: {status}");
    }

    public BattleStatus GetBattleStatus()
    {
        return status;
    }
}
