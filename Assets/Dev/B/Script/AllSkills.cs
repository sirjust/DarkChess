using System.Collections.Generic;
using UnityEngine;

public enum Skills
{
    Strike, Move
}

public class AllSkills : MonoBehaviour
{
<<<<<<< HEAD
    private TurnSystem turnSystem;
    private EditedGridGenerator gridGenerator;
    private int targets = 0;
=======
    private int targets = 0;
    private TurnSystem turnSystem;
    private DamageHandler damageHandler;
    private EditedGridGenerator gridGenerator;
    private GetBarInfo getBarInfo;

>>>>>>> b509ddc8d8322b376f23c874791c023ceeeae1ef
    private List<GameObject> parametersObjects = new List<GameObject>();

    private void Awake()
    {
<<<<<<< HEAD
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.GetComponent<TurnSystem>()) turnSystem = gameObject.GetComponent<TurnSystem>();
        }
=======
        getBarInfo = FindObjectOfType<GetBarInfo>();
        turnSystem = FindObjectOfType<TurnSystem>();
        damageHandler = FindObjectOfType<DamageHandler>();
        gridGenerator = FindObjectOfType<EditedGridGenerator>();
>>>>>>> b509ddc8d8322b376f23c874791c023ceeeae1ef
    }

    #region cast methods

<<<<<<< HEAD
    public bool cast(string skillname, int maxAmountofTargets, EditedGridGenerator _gridGenerator, GameObject user, BattleStatus battleStatus)
=======
    public bool cast(Card card, EditedGridGenerator _gridGenerator, GameObject user, BattleStatus battleStatus)
>>>>>>> b509ddc8d8322b376f23c874791c023ceeeae1ef
    {
        gridGenerator = _gridGenerator;

        if (turnSystem.GetBattleStatus() != battleStatus)
<<<<<<< HEAD
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
=======
        {
            Debug.Log("Its not your turn");
            return false;
        }
        if (user.GetComponent<GetStats>().character.currentHealth >= card.manaCost)
        {
            foreach (GameObject tile in gridGenerator.selectedTiles)
            {
                foreach (GameObject tile1 in gridGenerator.rangeTiles)
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
>>>>>>> b509ddc8d8322b376f23c874791c023ceeeae1ef
                }
            }
            if (targets == 0)
            {
                Debug.LogError("Select other tiles");
                if (turnSystem.GetBattleStatus() != BattleStatus.PlayerMove) gridGenerator.DestroyTiles(DestroyOption.all);
                return false;
            }
        }
        else
        {
            Debug.Log("You dont have enough mana for this ability");
        }
<<<<<<< HEAD
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
=======
        return true;
    }

    public bool cast(Card card, List<GameObject> selectedTiles, List<GameObject> rangeTiles, GameObject user, BattleStatus battleStatus)
    {
        if (turnSystem.GetBattleStatus() != battleStatus)
        {
            Debug.Log("Its not your turn");
            return false;
        }

        if (user.GetComponent<GetStats>().character.currentHealth >= card.manaCost)
        {
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
                Debug.Log("Select valid targets");
                if (turnSystem.GetBattleStatus() != BattleStatus.PlayerMove) gridGenerator.DestroyTiles(DestroyOption.all);
                return false;
            }
            else
            {
                Debug.Log("You dont have enough mana for this ability");
            }
        }
        return true;
    }

    #endregion

    public void Strike(List<GameObject> parameters)
    {
        turnSystem.NextTurn();
        damageHandler.DealDamage(parameters[0].GetComponent<GetStats>().lastcastedSkill.damage, parameters[1].GetComponent<GetObjectonTile>().gameObjectOnTile.GetComponent<GetStats>().character);
        parameters[0].GetComponent<GetStats>().character.currentMana -= parameters[0].GetComponent<GetStats>().lastcastedSkill.manaCost;
        getBarInfo.RefreshBar();
        parametersObjects.Clear();
        gridGenerator.DestroyTiles(DestroyOption.all);
>>>>>>> b509ddc8d8322b376f23c874791c023ceeeae1ef
    }

    public void Move(List<GameObject> parameters)
    {
        turnSystem.NextTurn();
        parameters[0].transform.position = parameters[1].transform.position;
<<<<<<< HEAD
        Debug.Log($"move to P({parameters[1].transform.position.x} | {parameters[1].transform.position.y} | {parameters[1].transform.position.z})");
        parametersObjects.Clear();
=======
        parametersObjects.Clear();
        gridGenerator.DestroyTiles(DestroyOption.all);
>>>>>>> b509ddc8d8322b376f23c874791c023ceeeae1ef
    }
}
