using UnityEngine;
using UnityEngine.UI;

public enum RealtionType
{
    Friendly, Neutral, Enemy
}

[CreateAssetMenu(fileName = "New Character", menuName = "ScObject/Character")]
public class Character : ScriptableObject
{
    public Sprite picture;
    public GameObject Model;

    public string charName;
    public int health;
    public int currentHealth;
    public int mana;
    public int currentMana;
    public int strenght;
    public int defense;
    public int movement;
    public int critRate;
    public int dogdeRate;

    public RealtionType realtion;
}
