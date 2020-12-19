using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mode
{
    hover, click
}

public class EditedGridGenerator : MonoBehaviour
{
    [Header("Required")]
    public Camera mainCam;
    public Mode mode;
    public GameObject tilePrefab;
    public GameObject highlight;

    [Header("Optional")]
    public bool takeObjectTransform = false;
    public bool destroy = false;
    public float timeBeforeDestroy = 0;
    public Vector2 gridstart;
    public Vector2 gridSize = new Vector2(8, 8);
    public LayerMask layer;
    public KeyCode selectionkey;
    public KeyCode clearSelectionkey;
    public float highlightHeight = 0.01f;

    [Header("Assigned Automatically")]
    public List<GameObject> selectedTiles = new List<GameObject>();
    public List<GameObject> skillrangeTiles = new List<GameObject>();
    private GameObject tilePrefabclone;
    private float gridstartX;
    private float gridstartY;

    void Start()
    {
        GenerateMap();
    }

    void Update()
    {
        if (mode == Mode.click) ClickHighlight();
        else HoverHighlight();

        if (Input.GetKey(clearSelectionkey)) ResetSelection();
    }

    public void GenerateMap()
    {
        if (takeObjectTransform)
        {
            gridstartX = this.transform.position.x;
            gridstartY = this.transform.position.z;
        }
        else
        {
            gridstartX = gridstart.x;
            gridstartY = gridstart.y;
        }
        for (float x = gridstartX; x < gridstartX + gridSize.x; x++)
        {
            for (float y = gridstartY; y < gridstartY + gridSize.y; y++)
            {
                Vector3 tilePosition = new Vector3(-gridSize.x / 2 + 0.5f + x, highlightHeight, -gridSize.y / 2 + 0.5f + y);
                tilePrefabclone = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 0));
                tilePrefabclone.transform.SetParent(this.gameObject.transform);
            }
        }
    }

    public void ClickHighlight()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hit, layer) && Input.GetMouseButtonDown(0))
        {
            if (hit.collider.gameObject.name == tilePrefabclone.name && Input.GetMouseButtonDown(0))
            {
                Vector3 objectPosition = new Vector3(hit.collider.gameObject.transform.position.x, highlightHeight, hit.collider.gameObject.transform.position.z);
                var clone = Instantiate(highlight, objectPosition, Quaternion.Euler(Vector3.right * 90));
                clone.transform.SetParent(this.gameObject.transform);
                selectedTiles.Add(clone);
                if (!Input.GetKey(selectionkey)) StartCoroutine(Wait(clone, destroy, timeBeforeDestroy));
            }
        }
    }

    public void ResetSelection()
    {
        foreach (GameObject tile in selectedTiles)
        {
            Destroy(tile);
        }
    }

    public void HoverHighlight()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hit, layer))
        {
            if (hit.collider.gameObject == tilePrefabclone)
            {
                Vector3 objectPosition = new Vector3(hit.collider.gameObject.transform.position.x, highlightHeight, hit.collider.gameObject.transform.position.z);
                var clone = Instantiate(highlight, objectPosition, Quaternion.Euler(Vector3.right * 90));
                clone.transform.SetParent(this.gameObject.transform);
                selectedTiles.Add(clone);
                StartCoroutine(Wait(clone, destroy, timeBeforeDestroy));
            }
        }
    }

    public void GenerateSkillTiles(List<Vector3> relativepositions, GameObject user)
    {
        if (user.transform.position.x % 0.5f == 0 && user.transform.position.z % 0.5f == 0)
        {
            foreach (Vector3 realtiveposition in relativepositions)
            {
                var newRealtiveposition = realtiveposition;

                if (user.transform.localEulerAngles == Vector3.zero)
                {
                    if (realtiveposition.z > 0) newRealtiveposition = new Vector3(-realtiveposition.x, realtiveposition.y, realtiveposition.z);
                    else if (realtiveposition.z < 0) newRealtiveposition = new Vector3(realtiveposition.x, realtiveposition.y, -realtiveposition.z);
                }
                if (user.transform.localEulerAngles == new Vector3(0, 270, 0))
                {
                    newRealtiveposition = new Vector3(-realtiveposition.x, realtiveposition.y, -realtiveposition.z);
                }
                if (user.transform.localEulerAngles == new Vector3(0, 180, 0))
                {
                    if (realtiveposition.z > 0) newRealtiveposition = new Vector3(realtiveposition.x, realtiveposition.y, -realtiveposition.z);
                    else if (realtiveposition.z < 0) newRealtiveposition = new Vector3(-realtiveposition.x, realtiveposition.y, realtiveposition.z);
                }

                var position = newRealtiveposition + user.transform.position;
                var tile = Instantiate(highlight, new Vector3(position.x, highlightHeight, position.z), Quaternion.Euler(Vector3.right * 90));
                tile.transform.SetParent(this.gameObject.transform);
                skillrangeTiles.Add(tile);
            }
        }
    }

    public void DestroySkillTiles()
    {
        foreach (GameObject tile in skillrangeTiles)
        {
            Destroy(tile);
        }
        skillrangeTiles.Clear();
    }

    IEnumerator Wait(GameObject gameObject, bool _destroy, float _timeBeforeDestroy)
    {
        yield return new WaitForSecondsRealtime(_timeBeforeDestroy);
        if (_destroy)
        {
            selectedTiles.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
