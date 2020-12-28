<<<<<<< HEAD
﻿using System.Collections;
=======
using System;
using System.Collections;
>>>>>>> b509ddc8d8322b376f23c874791c023ceeeae1ef
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
<<<<<<< HEAD
=======
    private int battleStatusLastIndex = Enum.GetNames(typeof(BattleStatus)).Length - 1;
>>>>>>> b509ddc8d8322b376f23c874791c023ceeeae1ef
    
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
<<<<<<< HEAD
        if (index < 3) 
=======
        if (index < battleStatusLastIndex) 
>>>>>>> b509ddc8d8322b376f23c874791c023ceeeae1ef
        {
            index++;
        }
        else index = 0;

        status = (BattleStatus)index;

        if (status == BattleStatus.EnemyMove) StartCoroutine(EnemyMove(time));
        if (status == BattleStatus.EnemyCombat) StartCoroutine(EnemyFight(time));

        PrintBattleStatus();
    }

    public void BackTurn()
    {
        if (index > 0) index--;
<<<<<<< HEAD
        else index = 3;
=======
        else index = battleStatusLastIndex;
>>>>>>> b509ddc8d8322b376f23c874791c023ceeeae1ef

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
