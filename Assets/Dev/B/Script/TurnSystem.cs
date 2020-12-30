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

    public void BackTurn()
    {
        if (index > 0) index--;
        else index = battleStatusLastIndex;

        status = (BattleStatus)index;
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
    }

    IEnumerator EnemyFight(float _time)
    {
        Debug.Log("Enemy is fighting...");
        yield return new WaitForSecondsRealtime(_time);
    }
}
