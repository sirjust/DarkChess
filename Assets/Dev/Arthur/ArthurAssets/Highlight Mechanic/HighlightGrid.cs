using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightGrid : MonoBehaviour
{
    public GameObject highlight; // An emissive quad is attached to this prefab
    public GameObject clone; // A clone of the original highlight prefab that can be cloned/destroyed

    void OnMouseEnter()
    {
        Vector3 objectPosition = new Vector3(transform.position.x, 0.02f, transform.position.z);
        clone = (GameObject)Instantiate(highlight, objectPosition, Quaternion.Euler(Vector3.right * 90));
    }

    void OnMouseExit()
    {
        Destroy(clone);
    }
}
