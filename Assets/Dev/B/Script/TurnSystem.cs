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
    
    private void Awake()
    {
        index = (int)status;
    }

    public void NextTurn()
    {
        if (index < 3) index++;
        else index = 0;

        status = (BattleStatus)index;

        if (status == BattleStatus.EnemyMove) StartCoroutine(EnemyMove(time));
        if (status == BattleStatus.EnemyCombat) StartCoroutine(EnemyFight(time));

        PrintBattleStatus();
    }

    public void BackTurn()
    {
        if (index > 0) index--;
        else index = 3;

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
        NextTurn();
    }

    IEnumerator EnemyFight(float _time)
    {
        Debug.Log("Enemy is fighting...");
        yield return new WaitForSecondsRealtime(_time);
        NextTurn();
    }
}
