using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string skillName;
    public int manaCost;
    public int damage;
    public Sprite skillPic;
    public GameObject template;
    public Skills skill;
    public int maxAmountOfTargets;
    public bool canTargetObjects;
    public List<Vector3> ranges = new List<Vector3>();
    [TextArea(1, 50)]
    public string skillDesciption;
}
