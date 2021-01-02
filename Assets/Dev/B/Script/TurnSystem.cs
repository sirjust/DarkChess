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

    [Header("Optional")]
    public float time = 1;
    private int index = 0;
    private int battleStatusLastIndex = Enum.GetNames(typeof(BattleStatus)).Length - 1;
    
    private void Awake()
    {
        index = (int)status;
    }

    private void Update()
    {
        index = (int)status;
    }

    public void NextTurn()
    {
        if (index < battleStatusLastIndex) 
        {
            index++;
        }
        else index = 0;

        status = (BattleStatus)index;
        if (status == BattleStatus.EnemyMove || status == BattleStatus.PlayerMove) 
            SwitchRelation();
        
        PrintBattleStatus();

        if (status == BattleStatus.EnemyMove)
            StartCoroutine(EnemyMove(time));
        else if (status == BattleStatus.EnemyCombat)
            StartCoroutine(EnemyFight(time));

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
            if (character.character.relation == RelationType.Enemy)
                character.character.relation = RelationType.Friendly;
            else if(character.character.relation == RelationType.Friendly)
                character.character.relation = RelationType.Enemy;
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

    IEnumerator EnemyMove(float _time)
    {
        Debug.Log("Enemy is moving...");
        yield return new WaitForSecondsRealtime(_time);
        NextTurn();
    }

    IEnumerator EnemyFight(float _time)
    {
        Debug.Log("Enemy is fighting...");
        yield return new WaitForSecondsRealtime(_time);
        NextTurn();
    }
}
