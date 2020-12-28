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

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject, IDamageable<int>
{
    public Sprite picture;
    public GameObject Model;

    public string charName;
    public int health;
    public int currentHealth;
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

    public Card movementCard;

    public void ReceiveDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Debug.Log($"{charName} died..");
    }
}
