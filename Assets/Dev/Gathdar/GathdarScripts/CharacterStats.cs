using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour, IComparable
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GameObject healthFill;
    [SerializeField]
    private GameObject magicFill;

    [Header("Stats")]
    public float health;
    public float magic;
    public float melee;
    public float magicRange;
    public float defense;
    public float speed;
    public float experience;

    private float startHealth;
    private float startMagic;

    [HideInInspector]
    public int nextActTurn;

    private bool isDead = false;

    private GameObject gameController;

    // Start is called before the first frame update
    void Start()
    {
        startHealth = health;
        startMagic = magic;

        gameController = GameObject.Find("GameController");
    }

    void ReceiveDamage(float damage)
    {
        health -= damage;
        anim.Play("Damage");

        // Set damage text

        if (health <= 0)
        {
            isDead = true;
            gameObject.tag = "Dead";
            Destroy(healthFill);
            Destroy(gameObject);
        }
        Invoke("ContinueGame", 2);
    }

    public bool GetDead()
    {
        return isDead;
    }

    public void ContinueGame()
    {
        GameObject.Find("GameController").GetComponent<GameController>().NextTurn();
    }

    public void CalculateNextTurn(int currentTurn)
    {
        nextActTurn = currentTurn + Mathf.CeilToInt(100f / speed);
    }

    public int CompareTo(object otherStats)
    {
        int nex = nextActTurn.CompareTo(((CharacterStats)otherStats).nextActTurn);
        return nex;
    }
}
