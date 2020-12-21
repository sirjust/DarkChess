using System.Collections.Generic;
using UnityEngine;

public enum Skills
{
    Strike
}

public class AllSkills : MonoBehaviour
{
    private TurnSystem turnSystem;
    private EditedGridGenerator gridGenerator;
    private int targets = 0;

    private void Awake()
    {
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.GetComponent<TurnSystem>()) turnSystem = gameObject.GetComponent<TurnSystem>();
        }
    }

    #region cast methods

    public bool cast(string skillname, int maxAmountofTargets, EditedGridGenerator _gridGenerator, GameObject _user, BattleStatus battleStatus)
    {
        gridGenerator = _gridGenerator;

        if (turnSystem.GetBattleStatus() != battleStatus)
        {
            Debug.Log("Its not your turn");
            return false;
        }

        foreach (GameObject tile in gridGenerator.selectedTiles)
        {
            foreach (GameObject tile1 in gridGenerator.rangeTiles)
            {
                if (tile.transform.position.x == tile1.transform.position.x && tile.transform.position.z == tile1.transform.position.z)
                {
                    this.SendMessage(skillname, tile);
                    targets++;

                    if (targets >= maxAmountofTargets) return true;
                }
            }
        }
        if (targets == 0)
        {
            Debug.LogError("Select other tiles");
            return false;
        }
        return false;
    }

    public bool cast(string name, int maxAmountofTargets, List<GameObject> selectedTiles, List<GameObject> rangeTiles, GameObject _user, BattleStatus battleStatus)
    {
        if (turnSystem.GetBattleStatus() != battleStatus)
        {
            Debug.Log("Its not your turn");
            return false;
        }

        foreach (GameObject tile in selectedTiles)
        {
            foreach (GameObject tile1 in rangeTiles)
            {
                if (tile.transform.position.x == tile1.transform.position.x && tile.transform.position.z == tile1.transform.position.z)
                {
                    this.SendMessage(name, tile);
                    targets++;

                    if (targets >= maxAmountofTargets) return true;
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

    public bool cast(Card card, EditedGridGenerator _gridGenerator, GameObject _user, BattleStatus battleStatus)
    {
        gridGenerator = _gridGenerator;

        if (turnSystem.GetBattleStatus() != battleStatus)
        {
            Debug.Log("Its not your turn");
            return false;
        }

        foreach (GameObject tile in gridGenerator.selectedTiles)
        {
            foreach (GameObject tile1 in gridGenerator.rangeTiles)
            {
                if (tile.transform.position.x == tile1.transform.position.x && tile.transform.position.z == tile1.transform.position.z)
                {
                    this.SendMessage(card.skill.ToString(), tile);
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

    public bool cast(Card card, List<GameObject> selectedTiles, List<GameObject> rangeTiles, GameObject _user, BattleStatus battleStatus)
    {
        if (turnSystem.GetBattleStatus() != battleStatus)
        {
            Debug.Log("Its not your turn");
            return false;
        }

        foreach (GameObject tile in selectedTiles)
        {
            foreach (GameObject tile1 in rangeTiles)
            {
                if (tile.transform.position.x == tile1.transform.position.x && tile.transform.position.z == tile1.transform.position.z)
                {
                    this.SendMessage(card.skill.ToString(), tile);
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

    #endregion

    public void Strike(GameObject targetTile)
    {
        turnSystem.NextTurn();
        Debug.Log($"strike at {targetTile.GetComponent<GetObjectonTile>().gameObjectOnTile.name}");
    }

    public void Move(GameObject targetTile)
    {
        turnSystem.NextTurn();
        Debug.Log($"move to P({targetTile.transform.position.x} | {targetTile.transform.position.y} | {targetTile.transform.position.z})");
    }
}
