using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObjectonTile : MonoBehaviour
{
    [Header("Assigned Automatically")]
    public GameObject gameObjectOnTile;

    private void Awake()
    {
        RaycastHit hit;
        Debug.DrawRay(this.gameObject.transform.position, new Vector3(0, 1, 0));
        if (Physics.Raycast(this.gameObject.transform.position, new Vector3(0, 1, 0), out hit, 100f))
        {
            gameObjectOnTile = hit.collider.gameObject;
        }
        else
        {
            gameObjectOnTile = null;
        }
    }

    private void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(this.gameObject.transform.position, new Vector3(0, 1, 0));
        if (Physics.Raycast(this.gameObject.transform.position, new Vector3(0,1,0), out hit, 100f))
        {
                gameObjectOnTile = hit.collider.gameObject;
        }else
        {
            gameObjectOnTile = null;
        }
    }
}
