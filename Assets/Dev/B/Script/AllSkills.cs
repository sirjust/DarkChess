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
    private List<GameObject> parametersObjects = new List<GameObject>();

    private void Awake()
    {
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.GetComponent<TurnSystem>()) turnSystem = gameObject.GetComponent<TurnSystem>();
        }
    }

    #region cast methods

    public bool cast(string skillname, int maxAmountofTargets, EditedGridGenerator _gridGenerator, GameObject user, BattleStatus battleStatus)
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
                    parametersObjects.Add(user);
                    parametersObjects.Add(tile);
                    this.SendMessage(skillname, parametersObjects);
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

    public bool cast(string name, int maxAmountofTargets, List<GameObject> selectedTiles, List<GameObject> rangeTiles, GameObject user, BattleStatus battleStatus)
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
                    parametersObjects.Add(user);
                    parametersObjects.Add(tile);
                    this.SendMessage(name, parametersObjects);
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

    public bool cast(Card card, EditedGridGenerator _gridGenerator, GameObject user, BattleStatus battleStatus)
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
                    parametersObjects.Add(user);
                    parametersObjects.Add(tile);
                    this.SendMessage(card.skill.ToString(), parametersObjects);
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

    public bool cast(Card card, List<GameObject> selectedTiles, List<GameObject> rangeTiles, GameObject user, BattleStatus battleStatus)
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
                    parametersObjects.Add(user);
                    parametersObjects.Add(tile);
                    this.SendMessage(card.skill.ToString(), parametersObjects);
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

    public void Strike(List<GameObject> parameters)
    {
        turnSystem.NextTurn();
        Debug.Log($"{parameters[0].GetComponent<GetStats>().character.charName} stroke at {parameters[1].GetComponent<GetObjectonTile>().gameObjectOnTile.name}");
        parametersObjects.Clear();
    }

    public void Move(List<GameObject> parameters)
    {
        turnSystem.NextTurn();
        parameters[0].transform.position = parameters[1].transform.position;
        Debug.Log($"move to P({parameters[1].transform.position.x} | {parameters[1].transform.position.y} | {parameters[1].transform.position.z})");
        parametersObjects.Clear();
    }
}
