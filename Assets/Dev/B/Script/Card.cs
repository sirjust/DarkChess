using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "ScObject/Card")]
public class Card : ScriptableObject
{
    public string skillName;
    public int manaCost;
    public float damage;
    public Sprite skillPic;
    public GameObject template;
}
