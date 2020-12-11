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
    public Skills skill;
    public Vector2 range;
    public bool horizontal;
    public bool vertical;

    [Header("Assigned Automatically")]
    public List<Vector2> ranges = new List<Vector2>();

    public void Init()
    {
        if (ranges.Count == 0)
        {
            ranges.Add(range);
            if (horizontal) ranges.Add(new Vector2(range.x, range.y - (range.y * 2)));
            if (vertical) ranges.Add(new Vector2(range.x - (range.x * 2), range.y));
        }
    }
}
