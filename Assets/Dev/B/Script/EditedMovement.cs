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
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.GetComponent<EditedGridGenerator>()) gridGenerator = gameObject.GetComponent<EditedGridGenerator>();
            if (gameObject.GetComponent<AllSkills>()) allSkills = gameObject.GetComponent<AllSkills>();
            if (gameObject.GetComponent<TurnSystem>()) turnSystem = gameObject.GetComponent<TurnSystem>();
        }

        getStats = GetComponent<GetStats>();
    }

    void Update()
    {
        Rotate();

        if (turnSystem.GetBattleStatus() == BattleStatus.PlayerMove)
        {
            if (!tracked)
            {
                gridGenerator.GenerateSkillTiles(getStats.character.moveRanges, this.gameObject, TypesofValue.relative);
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
            RefreshMoveTiles();
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.transform.localEulerAngles = right;
            RefreshMoveTiles();
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.transform.localEulerAngles = down;
            RefreshMoveTiles();
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.transform.localEulerAngles = up;
            RefreshMoveTiles();
        }
    }

    public void RefreshMoveTiles()
    {
        gridGenerator.DestroyTiles();
        gridGenerator.GenerateSkillTiles(getStats.character.moveRanges, this.gameObject, TypesofValue.relative);
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

                if (allSkills.cast("Move", 1, currentSelectedTiles, gridGenerator.rangeTiles, this.gameObject, BattleStatus.PlayerMove))
                {
                    gridGenerator.DestroyTiles();
                    tracked = false;
                }
            }
        }
    }
}