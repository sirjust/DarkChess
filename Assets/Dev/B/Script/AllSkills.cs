using System.Collections.Generic;
using UnityEngine;

public enum Skills
{
    strike, move
}

public class AllSkills : MonoBehaviour
{
    private EditedGridGenerator gridGenerator;
    private int targets = 0;

    public bool cast(Card card, EditedGridGenerator _gridGenerator, GameObject _user)
    {
        gridGenerator = _gridGenerator;

        foreach(GameObject tile in gridGenerator.selectedTiles)
        {
            foreach(GameObject tile1 in gridGenerator.skillrangeTiles)
            {
                Debug.Log($"{tile.transform.position.x} == { tile1.transform.position.x} && { tile.transform.position.z} == { tile1.transform.position.z}");
                if (tile.transform.position.x == tile1.transform.position.x && tile.transform.position.z == tile1.transform.position.z)
                {
                    this.SendMessage(card.skill.ToString(), tile1);
                    targets++;

                    if (targets >= card.maxAmountOfTargets) return true;
                }
            }
        }
        if (targets == 0)
        {
            Debug.LogError("Select other tiles");
            return false;
        }

        return true;
    }

    public void strike(GameObject targetTile)
    {
        Debug.Log($"strike at {targetTile.GetComponent<GetObjectonTile>().gameObjectOnTile.name}");
    }
}
