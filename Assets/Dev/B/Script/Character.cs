using UnityEngine;
using UnityEngine.UI;

public enum RealtionType
{
    Friendly, Neutral, Enemy
}

public enum HealthRepresentation
{
    healthbar, hearts
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
    public int actionAmount;

    public HealthRepresentation healthRepresentation;
    public RealtionType realtion;
}
