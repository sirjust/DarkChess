//Attach this script to a GameObject to have it output messages when your mouse hovers over it.
using System.Runtime.ExceptionServices;
using UnityEngine;

public class GenerateGrid : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject clone; // A clone of the tilePrefab that can be edited without harming the original prefab.

    // Change dimensions in the inspector. Defaulted to 8x8.
    public Vector2 gridSize  = new Vector2(8, 8);


    void Start()
    {
        // Generates a grid of whatever "tilePrefab" is set to. 
        // In this case, it is an invisible cube.
        GenerateMap();
    }

    public void GenerateMap()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector3 tilePosition = new Vector3(-gridSize.x / 2 + 0.5f + x, 0.02f, -gridSize.y/2 + 0.5f + y);
                clone = (GameObject)Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 0));
            }
        }
    }
}