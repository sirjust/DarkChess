using UnityEngine;

public enum BattleStatus
{
    PlayerMove, PlayerCombat, EnemyMove, EnemyCombat
}

public class TurnSystem : MonoBehaviour
{
    [Header("Requiered")]
    public BattleStatus status;

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
    }

    public void BackTurn()
    {

        if (index > 0) index--;
        else index = 3;

        status = (BattleStatus)index;

    }

    public void GetBattleStatus()
    {
        Debug.Log($"BattleStatus: {status}");
    }
}
