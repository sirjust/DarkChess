using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public List<GameObject> h = new List<GameObject>();
    public List<GameObject> n = new List<GameObject>();
    
    private void Awake()
    {
        n = h;
    }

    public void Change()
    {
        h.Add(new GameObject());
    }

}
