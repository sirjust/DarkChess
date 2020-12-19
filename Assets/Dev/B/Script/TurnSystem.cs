using UnityEngine;

public enum BattleStatus
{
    PlayerMove, PlayerCombat, EnemyMove, EnemyCombat
}

public class TurnSystem : MonoBehaviour
{
    [Header("Requiered")]
    [SerializeField]
    private BattleStatus status;

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
}
