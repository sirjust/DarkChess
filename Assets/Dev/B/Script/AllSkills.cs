using System.Collections.Generic;
using UnityEngine;

public enum Skills
{
    Strike, Move
}

public enum TargetType
{
    ally, enemies, tiles
}

public class AllSkills : MonoBehaviour
{
    [Header("Required")]
    public GameObject player;

    private int targets = 0;
    private TurnSystem turnSystem;
    private DamageHandler damageHandler;
    private EditedGridGenerator gridGenerator;
    private GetBarInfo getBarInfo;
    private CardSystem cardSystem;

    private List<GameObject> parametersObjects = new List<GameObject>();

    private void Awake()
    {
        cardSystem = FindObjectOfType<CardSystem>();
        getBarInfo = FindObjectOfType<GetBarInfo>();
        turnSystem = FindObjectOfType<TurnSystem>();
        damageHandler = FindObjectOfType<DamageHandler>();
        gridGenerator = FindObjectOfType<EditedGridGenerator>();
    }

    #region cast methods
    public bool cast(Card card, EditedGridGenerator _gridGenerator, GameObject user, BattleStatus battleStatus, GetStats turn)
    {
        targets = 0;
        gridGenerator = _gridGenerator;

        if (turnSystem.GetBattleStatus() != battleStatus && turnSystem.currentTurn != turn && turnSystem.currentTurn == cardSystem.Player.GetComponent<GetStats>())
        {
            if (turnSystem.currentTurn == player.GetComponent<GetStats>())
            {
                gridGenerator.DestroyTiles(DestroyOption.selectedTiles, true, true);
                Debug.Log("Its not the right stage to play a card");
            }

            return false;
        }
        if (user.GetComponent<GetStats>().character.currentMana >= card.manaCost)
        {
            foreach (GameObject tile in gridGenerator.selectedTiles)
            {
                foreach (GameObject tile1 in gridGenerator.rangeTiles)
                {
                    //Debug.Log($"{tile.transform.position.x} == {tile1.transform.position.x} && {tile.transform.position.z} == {tile1.transform.position.z} ");
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
                if (turnSystem.GetBattleStatus() != BattleStatus.Move && turnSystem.currentTurn == cardSystem.Player.GetComponent<GetStats>())
                {
                    Debug.Log("Select valid target");
                    gridGenerator.DestroyTiles(DestroyOption.all, true, true);
                }
                return false;
            }
        }
        else
        {
            if (turnSystem.currentTurn == cardSystem.Player.GetComponent<GetStats>())
                Debug.Log("You dont have enough mana for this ability");
            return false;
        }
        return false;
    }

    public bool cast(Card card, List<GameObject> selectedTiles, List<GameObject> rangeTiles, GameObject user, BattleStatus battleStatus, GetStats turn)
    {
        targets = 0;

        if (turnSystem.GetBattleStatus() != battleStatus && turnSystem.currentTurn != turn && turnSystem.currentTurn == cardSystem.Player.GetComponent<GetStats>())
        {
            if (turnSystem.currentTurn == player.GetComponent<GetStats>())
            {
                gridGenerator.DestroyTiles(DestroyOption.selectedTiles, true, true);
                Debug.Log("Its not the right stage to play a card");
            }
            return false;
        }

        if (user.GetComponent<GetStats>().character.currentMana >= card.manaCost)
        {
            foreach (GameObject tile in selectedTiles)
            {
                foreach (GameObject tile1 in rangeTiles)
                {
                    //Debug.Log($"{tile.transform.position.x} == {tile1.transform.position.x} && {tile.transform.position.z} == {tile1.transform.position.z} ");
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
                if (turnSystem.GetBattleStatus() != BattleStatus.Move && turnSystem.currentTurn == cardSystem.Player.GetComponent<GetStats>())
                {
                    Debug.Log("Select valid targets");
                    gridGenerator.DestroyTiles(DestroyOption.all, true, true);
                }
                return false;
            }
        }
        else
        {
            if (turnSystem.currentTurn == cardSystem.Player.GetComponent<GetStats>())
                Debug.Log("You dont have enough mana for this ability");
            return false;
        }
        return false;
    }
    #endregion

    public void Strike(List<GameObject> parameters)
    {
        damageHandler.DealDamage(parameters[0].GetComponent<GetStats>().lastcastedSkill.damage, parameters[1].GetComponent<GetObjectonTile>().gameObjectOnTile.GetComponent<GetStats>().character);
        parameters[0].GetComponent<GetStats>().character.currentMana -= parameters[0].GetComponent<GetStats>().lastcastedSkill.manaCost;
        getBarInfo.RefreshBar();
        parametersObjects.Clear();
        gridGenerator.DestroyTiles(DestroyOption.all, true, true);
        turnSystem.NextTurn();
    }

    public void Move(List<GameObject> parameters)
    {
        parameters[0].transform.position = parameters[1].transform.position;
        parametersObjects.Clear();
        gridGenerator.DestroyTiles(DestroyOption.all, true, true);
        turnSystem.NextTurn();
    }
}
