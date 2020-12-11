using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getObjectonTile : MonoBehaviour
{
    [Header("Assigned Automatically")]
    public GameObject gameObjectOnTile;
    public LayerMask layer;

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(this.gameObject.transform.position, Vector3.up, out hit, layer))
        {
            gameObjectOnTile = hit.collider.gameObject;
        }
    }
}
