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

    public string charname;
    public int health;
    public int currenthealth;
    public int mana;
    public int currentmana;
    public int strenght;
    public int defense;
    public int movement;
    public int critRate;
    public int dogdeRate;

    public RealtionType realtion;

    
}
