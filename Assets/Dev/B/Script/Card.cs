using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "ScObject/Card")]
public class Card : ScriptableObject
{
    public string skillname;
    public int manacost;
    public Sprite pic;
}
