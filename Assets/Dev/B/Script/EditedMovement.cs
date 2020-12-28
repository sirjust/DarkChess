using System.Collections.Generic;
using UnityEngine;

public class EditedMovement : MonoBehaviour
{
    [Header("Required")]
    public Camera mainCam;

    private AllSkills allSkills;
    private TurnSystem turnSystem;
    private GetStats getStats;
    private EditedGridGenerator gridGenerator;
    private bool tracked = false;

    private Vector3 up = Vector3.zero;
    private Vector3 right = new Vector3(0, 90, 0);
    private Vector3 down = new Vector3(0, 180, 0);
    private Vector3 left = new Vector3(0, 270, 0);

    private void Awake()
    {
<<<<<<< HEAD
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.GetComponent<EditedGridGenerator>()) gridGenerator = gameObject.GetComponent<EditedGridGenerator>();
            if (gameObject.GetComponent<AllSkills>()) allSkills = gameObject.GetComponent<AllSkills>();
            if (gameObject.GetComponent<TurnSystem>()) turnSystem = gameObject.GetComponent<TurnSystem>();
        }

=======
        gridGenerator = FindObjectOfType<EditedGridGenerator>();
        allSkills = FindObjectOfType<AllSkills>();
        turnSystem = FindObjectOfType<TurnSystem>();
>>>>>>> b509ddc8d8322b376f23c874791c023ceeeae1ef
        getStats = GetComponent<GetStats>();
    }

    void Update()
    {
        Rotate();

        if (turnSystem.GetBattleStatus() == BattleStatus.PlayerMove)
        {
            if (!tracked)
            {
<<<<<<< HEAD
                gridGenerator.GenerateSkillTiles(getStats.character.moveRanges, this.gameObject, TypesofValue.relative);
=======
                gridGenerator.GenerateSkillTiles(getStats.character.movementCard.ranges, this.gameObject, TypesofValue.relative);
>>>>>>> b509ddc8d8322b376f23c874791c023ceeeae1ef
                tracked = true;
            }
            checkRayCast();
        }
    }

    public void Rotate()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.transform.localEulerAngles = left;
            if(turnSystem.GetBattleStatus() == BattleStatus.PlayerMove) RefreshMoveTiles();
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.transform.localEulerAngles = right;
            if (turnSystem.GetBattleStatus() == BattleStatus.PlayerMove) RefreshMoveTiles();
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.transform.localEulerAngles = down;
            if (turnSystem.GetBattleStatus() == BattleStatus.PlayerMove) RefreshMoveTiles();
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.transform.localEulerAngles = up;
            if (turnSystem.GetBattleStatus() == BattleStatus.PlayerMove) RefreshMoveTiles();
        }
    }

    public void RefreshMoveTiles()
    {
<<<<<<< HEAD
        gridGenerator.DestroyTiles();
        gridGenerator.GenerateSkillTiles(getStats.character.moveRanges, this.gameObject, TypesofValue.relative);
=======
        gridGenerator.DestroyTiles(DestroyOption.rangeTiles);
        gridGenerator.GenerateSkillTiles(getStats.character.movementCard.ranges, this.gameObject, TypesofValue.relative);
>>>>>>> b509ddc8d8322b376f23c874791c023ceeeae1ef
        tracked = false;
    }

    public void checkRayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hit) && Input.GetMouseButtonDown(0))
        {
            if (hit.collider.gameObject.name == gridGenerator.GetTilePrefabClone().name && Input.GetMouseButtonDown(0))
            {
                List<GameObject> currentSelectedTiles = gridGenerator.selectedTiles; 

<<<<<<< HEAD
                if (allSkills.cast("Move", 1, currentSelectedTiles, gridGenerator.rangeTiles, this.gameObject, BattleStatus.PlayerMove))
                {
                    gridGenerator.DestroyTiles();
=======
                if (allSkills.cast(getStats.character.movementCard, currentSelectedTiles, gridGenerator.rangeTiles, this.gameObject, BattleStatus.PlayerMove))
                {
                    getStats.lastcastedSkill = getStats.character.movementCard;
                    gridGenerator.DestroyTiles(DestroyOption.all);
>>>>>>> b509ddc8d8322b376f23c874791c023ceeeae1ef
                    tracked = false;
                }
            }
        }
    }
}