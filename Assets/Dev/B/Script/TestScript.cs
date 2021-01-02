using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public List<GameObject> oldList = new List<GameObject>();
    public List<GameObject> newList = new List<GameObject>();

    private void Awake()
    {
        newList.AddRange(oldList.ToArray());
    }

    public void Change()
    {
        oldList.Add(new GameObject());
    }

    public void Delete()
    {
        oldList.RemoveAt(0);
    }
}
