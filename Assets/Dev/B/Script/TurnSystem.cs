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
            getStat.character.realtion = getStat.character.startRelationType;
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
            if (turnOrder[currentTurnIndex].character.realtion != turnOrder[currentTurnIndex - 1].character.realtion)
                SwitchRelation();
        }
        else if (status == BattleStatus.Combat)
        {
            SetOrder();
            currentTurnIndex = 0;

            if (turnOrder[currentTurnIndex].character.realtion != lastTurn.character.realtion)
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
            if (turnOrder[currentTurnIndex].character.realtion != turnOrder[currentTurnIndex + 1].character.realtion)
                SwitchRelation();
        }
        else if (status == BattleStatus.Combat)
        {
            SetOrder();
            currentTurnIndex = turnOrder.Count - 1;
            if (turnOrder[currentTurnIndex].character.realtion != lastTurn.character.realtion)
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
            if (character.character.realtion == RealtionType.Enemy)
                character.character.realtion = RealtionType.Friendly;
            else if (character.character.realtion == RealtionType.Friendly)
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
