using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private List<CharacterStats> characterStats;

    [SerializeField]
    private GameObject battleMenu;

    public Text battleText;

    // Start is called before the first frame update
    void Start()
    {
        characterStats = new List<CharacterStats>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        CharacterStats currentFighterStats = player.GetComponent<CharacterStats>();
        currentFighterStats.CalculateNextTurn(0);
        characterStats.Add(currentFighterStats);

        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        CharacterStats currentEnemyStats = enemy.GetComponent<CharacterStats>();
        currentEnemyStats.CalculateNextTurn(0);
        characterStats.Add(currentEnemyStats);

        characterStats.Sort();
        this.battleMenu.SetActive(false);

        NextTurn();
    }

    public void NextTurn()
    {
        battleText.gameObject.SetActive(false);
        CharacterStats currentFighterStats = characterStats[0];
        characterStats.Remove(currentFighterStats);
        if (!currentFighterStats.GetDead())
        {
            GameObject currentUnit = currentFighterStats.gameObject;
            currentFighterStats.CalculateNextTurn(currentFighterStats.nextActTurn);
            characterStats.Add(currentFighterStats);
            characterStats.Sort();

            if (currentUnit.tag == "Player")
            {
                Debug.Log("Player's turn");
                this.battleMenu.SetActive(true);
                //currentUnit.Movement.canMove = true;
                // set canMove and canAttack to true
            }
            else
            {
                Debug.Log("Enemy's turn");
                Thread.Sleep(3000);
                //this.battleMenu.SetActive(false);
                //string attackType = Random.Range(0, 2) == 1 ? "Melee" : "Magic";
                //currentUnit.GetComponent<FighterAction>().SelectAttack(attackType);
            }
        }
        else
        {
            NextTurn();
        }
    }
}
