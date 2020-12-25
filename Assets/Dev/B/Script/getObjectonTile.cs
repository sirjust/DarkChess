using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObjectonTile : MonoBehaviour
{
    [Header("Optional")]
    public LayerMask layer;

    [Header("Assigned Automatically")]
    public GameObject gameObjectOnTile;

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(this.gameObject.transform.position, new Vector3(0,1,0), out hit))
        {
                gameObjectOnTile = hit.collider.gameObject;
        }
    }
}
