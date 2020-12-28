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
        gridGenerator = FindObjectOfType<EditedGridGenerator>();
        allSkills = FindObjectOfType<AllSkills>();
        turnSystem = FindObjectOfType<TurnSystem>();
        getStats = GetComponent<GetStats>();
    }

    void Update()
    {
        Rotate();

        if (turnSystem.GetBattleStatus() == BattleStatus.PlayerMove)
        {
            if (!tracked)
            {
                gridGenerator.GenerateSkillTiles(getStats.character.movementCard.ranges, getStats.character.movementCard.canTargetObjects, this.gameObject, TypesofValue.relative);
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
            if (turnSystem.GetBattleStatus() == BattleStatus.PlayerMove) RefreshMoveTiles();
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
        gridGenerator.DestroyTiles(DestroyOption.rangeTiles);
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

                if (allSkills.cast(getStats.character.movementCard, currentSelectedTiles, gridGenerator.rangeTiles, this.gameObject, BattleStatus.PlayerMove, false))
                {
                    getStats.lastcastedSkill = getStats.character.movementCard;
                    gridGenerator.DestroyTiles(DestroyOption.all);
                    tracked = false;
                }
            }
        }
    }
}