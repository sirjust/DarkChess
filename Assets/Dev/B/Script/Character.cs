using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RealtionType
{
    Friendly, Neutral, Enemy
}

public enum HealthRepresentation
{
    healthbar, none
}

[CreateAssetMenu(fileName = "New Character", menuName = "ScObject/Character")]
public class Character : ScriptableObject
{
    public Sprite picture;
    public GameObject Model;

    public string charName;
    public int health;
    public int currentHealth;
    public int hearts;
    public int currenthearts;
    public int mana;
    public int currentMana;
    public int strength;
    public int defense;
    public int movement;
    public int critRate;
    public int dodgeRate;
    public List<Vector3> moveRanges = new List<Vector3>();

    public HealthRepresentation healthRepresentation;
    public RealtionType realtion;
}
