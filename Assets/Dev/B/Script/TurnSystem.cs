using System;
using System.Collections.Generic;
using UnityEngine;

public enum BattleStatus
{
    Move, Combat
}

public class TurnSystem : MonoBehaviour
{
    [Header("Required")]
    [SerializeField]
    private BattleStatus status;

    [Header("Assigned Automatically")]
    public GetStats currentTurn;
    public GetStats lastTurn;
    public List<GetStats> turnOrder = new List<GetStats>();

    private int currentTurnIndex = 0;
    private int index = 0;
    private int battleStatusLastIndex = Enum.GetNames(typeof(BattleStatus)).Length - 1;
    private GetStats[] getStats;
    private EditedGridGenerator gridGenerator;
    private CardSystem cardSystem;


    private void Awake()
    {
        cardSystem = FindObjectOfType<CardSystem>();
        gridGenerator = FindObjectOfType<EditedGridGenerator>();
        index = (int)status;

        getStats = FindObjectsOfType<GetStats>();
        foreach (GetStats getStat in getStats)
        {
            if (currentTurn != cardSystem.Player.GetComponent<GetStats>())
            {
                if (getStat != cardSystem.Player.GetComponent<GetStats>())
                    getStat.character.relation = RelationType.Friendly;
                else
                    getStat.character.relation = RelationType.Enemy;
            }
            else if (currentTurn == cardSystem.Player.GetComponent<GetStats>())
            {
                if (getStat == cardSystem.Player.GetComponent<GetStats>())
                    getStat.character.relation = RelationType.Friendly;
                else
                    getStat.character.relation = RelationType.Enemy;
            }
        }

        SetOrder();
        RefreshOrder();
    }

    private void Update()
    {
        index = (int)status;
    }

    public void RefreshOrder()
    {
        lastTurn = currentTurn;
        currentTurn = turnOrder[currentTurnIndex];
    }

    public void SetOrder()
    {
        turnOrder.Clear();
        turnOrder.AddRange(FindObjectsOfType<GetStats>());
    }

    public void NextTurn()
    {
        gridGenerator.DestroyTiles(DestroyOption.all, true, true);

        if (index < battleStatusLastIndex)
            index++;
        else
            index = 0;

        if (currentTurnIndex < (turnOrder.Count - 1) && (status == BattleStatus.Combat))
        {
            currentTurnIndex++;
            if (turnOrder[currentTurnIndex].character.relation != turnOrder[currentTurnIndex - 1].character.relation)
                SwitchRelation();
        }
        else if (status == BattleStatus.Combat)
        {
            SetOrder();
            currentTurnIndex = 0;

            if (turnOrder[currentTurnIndex].character.relation != lastTurn.character.relation)
                SwitchRelation();
        }

        status = (BattleStatus)index;

        RefreshOrder();
        //PrintBattleStatus();
    }

    public void BackTurn()
    {
        gridGenerator.DestroyTiles(DestroyOption.all, true, true);

        if (index > 0)
            index--;
        else
            index = battleStatusLastIndex;

        if (currentTurnIndex == 1 && (status == BattleStatus.Combat))
        {
            currentTurnIndex--;
            if (turnOrder[currentTurnIndex].character.relation != turnOrder[currentTurnIndex + 1].character.relation)
                SwitchRelation();
        }
        else if (status == BattleStatus.Combat)
        {
            SetOrder();
            currentTurnIndex = turnOrder.Count - 1;
            if (turnOrder[currentTurnIndex].character.relation != lastTurn.character.relation)
                SwitchRelation();
        }

        status = (BattleStatus)index;
    }

    public void SkipPlayerTurn()
    {
        if (cardSystem.Player.GetComponent<GetStats>() == currentTurn)
            NextTurn();
    }

    public void SwitchRelation()
    {
        GetStats[] characters = FindObjectsOfType<GetStats>();
        foreach (GetStats character in characters)
        {
            if (character.character.relation == RelationType.Enemy)
                character.character.relation = RelationType.Friendly;
            else if (character.character.relation == RelationType.Friendly)
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
}
