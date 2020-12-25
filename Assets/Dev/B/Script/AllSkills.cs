using System.Collections.Generic;
using UnityEngine;

public enum Skills
{
    Strike, Move
}

public class AllSkills : MonoBehaviour
{
    private TurnSystem turnSystem;
    private DamageHandler damageHandler;
    private EditedGridGenerator gridGenerator;
    private int targets = 0;
    private List<GameObject> parametersObjects = new List<GameObject>();

    private void Awake()
    {
        turnSystem = FindObjectOfType<TurnSystem>();
        damageHandler = FindObjectOfType<DamageHandler>();
        gridGenerator = FindObjectOfType<EditedGridGenerator>();
    }

    #region cast methods

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

                    if (targets >= card.maxAmountOfTargets) 
                        return true;
                }
            }
        }
        if (targets == 0)
        {
            gridGenerator.DestroyTiles();
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
                    user.GetComponent<GetStats>().lastcastedSkill = card;
                    this.SendMessage(card.skill.ToString(), parametersObjects);
                    targets++;

                    if (targets >= card.maxAmountOfTargets)
                        return true;
                }
            }
        }
        if (targets == 0)
        {
            Debug.LogError("Select other tiles");
            gridGenerator.DestroyTiles();
            return false;
        }
        return true;
    }

    #endregion

    public void Strike(List<GameObject> parameters)
    {
        turnSystem.NextTurn();
        damageHandler.DealDamage(parameters[0].GetComponent<GetStats>().lastcastedSkill.damage, parameters[1].GetComponent<GetObjectonTile>().gameObjectOnTile.GetComponent<GetStats>().character);
        Debug.Log($"{parameters[0].GetComponent<GetStats>().character.charName} stroke at {parameters[1].GetComponent<GetObjectonTile>().gameObjectOnTile.name}");
        parametersObjects.Clear();
        gridGenerator.DestroyTiles();
    }

    public void Move(List<GameObject> parameters)
    {
        turnSystem.NextTurn();
        Debug.Log($"damage: {parameters[0].GetComponent<GetStats>().lastcastedSkill.damage}");
        parameters[0].transform.position = parameters[1].transform.position;
        Debug.Log($"move to P({parameters[1].transform.position.x} | {parameters[1].transform.position.y} | {parameters[1].transform.position.z})");
        parametersObjects.Clear();
        gridGenerator.DestroyTiles();
    }
}
