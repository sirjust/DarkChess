using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mode
{
    hover, click
}

public enum TypesofValue
{
    absolute, relative
}

public enum DestroyOption
{
    all, selectedTiles, rangeTiles
}

public class EditedGridGenerator : MonoBehaviour
{
    [Header("Required")]
    public Camera mainCam;
    public Mode mode;
    public GameObject tilePrefab;
    public GameObject highlight;
    public GameObject player;

    [Header("Optional")]
    public bool takeObjectTransform = false;
    public bool destroy = false;
    public float timeBeforeDestroy = 0;
    public Vector3 gridstart;
    public Vector2 gridSize = new Vector2(8, 8);
    public KeyCode selectionkey;
    public KeyCode clearSelectionkey;

    [Header("Assigned Automatically")]
    public List<GameObject> selectedTiles = new List<GameObject>();
    public List<GameObject> rangeTiles = new List<GameObject>();

    private List<GameObject> invisGridTiles = new List<GameObject>();    
    private GameObject tilePrefabclone;
    private float gridstartX;
    private float gridstartY;
    private float gridstartZ;
    private GameObject tile;
    private bool valid = false;
    void Start()
    {
        GenerateMap();
    }

    void Update()
    {
        if (mode == Mode.click) ClickHighlight();
        else HoverHighlight();

        if (Input.GetKey(clearSelectionkey)) DestroyTiles(DestroyOption.selectedTiles, true, true);
    }

    public void GenerateMap()
    {
        if (takeObjectTransform)
        {
            gridstartX = this.transform.position.x;
            gridstartY = this.transform.position.y;
            gridstartZ = this.transform.position.z;
        }
        else
        {
            gridstartX = gridstart.x;
            gridstartY = player.transform.position.y;
            gridstartZ = gridstart.z;
        }
        for (float x = gridstartX; x < gridstartX + gridSize.x; x++)
        {
            for (float z = gridstartZ; z < gridstartZ + gridSize.y; z++)
            {
                Vector3 tilePosition = new Vector3(0.5f + x, gridstartY + 0.01f, 0.5f + z);
                tilePrefabclone = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 0));
                tilePrefabclone.transform.SetParent(this.gameObject.transform);
                invisGridTiles.Add(tilePrefabclone);
            }
        }
    }

    public void ClickHighlight()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hit) && Input.GetMouseButtonDown(0))
        {
            if (hit.collider.gameObject.name == tilePrefabclone.name && Input.GetMouseButtonDown(0))
            {
                Vector3 objectPosition = new Vector3(hit.collider.gameObject.transform.position.x, gridstartY + 0.01f, hit.collider.gameObject.transform.position.z);
                var clone = Instantiate(highlight, objectPosition, Quaternion.Euler(Vector3.right * 90));
                clone.transform.SetParent(this.gameObject.transform);
                selectedTiles.Add(clone);
                if (!Input.GetKey(selectionkey)) StartCoroutine(WaitUntilDestroy(clone, destroy, timeBeforeDestroy));
            }
        }
    }

    public void HoverHighlight()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (hit.collider.gameObject.name == tilePrefabclone.name)
            {
                Vector3 objectPosition = new Vector3(hit.collider.gameObject.transform.position.x, gridstartY + 0.01f, hit.collider.gameObject.transform.position.z);
                var clone = Instantiate(highlight, objectPosition, Quaternion.Euler(Vector3.right * 90));
                clone.transform.SetParent(this.gameObject.transform);
                selectedTiles.Add(clone);
                StartCoroutine(WaitUntilDestroy(clone, destroy, timeBeforeDestroy));
            }
        }
    }

    public void GenerateSkillTiles(List<Vector3> relativepositions, TargetType targetType, GameObject user, TypesofValue typesofValue, bool visible)
    {
        if (user.transform.position.x % 0.5f == 0 && user.transform.position.z % 0.5f == 0)
        {
            foreach (Vector3 realtiveposition in relativepositions)
            {
                var newRealtiveposition = realtiveposition;
                valid = false;

                if (user.transform.localEulerAngles == Vector3.zero && typesofValue == TypesofValue.relative)
                {
                    //Up
                    if (realtiveposition.z > 0) newRealtiveposition = new Vector3(-realtiveposition.x, realtiveposition.y, realtiveposition.z);
                    else if (realtiveposition.z < 0) newRealtiveposition = new Vector3(realtiveposition.x, realtiveposition.y, -realtiveposition.z);
                    else if (realtiveposition.z == 0) newRealtiveposition = new Vector3(realtiveposition.z, realtiveposition.y, realtiveposition.x);

                    if (Mathf.Abs(realtiveposition.x) > Mathf.Abs(realtiveposition.z)) newRealtiveposition = new Vector3(-realtiveposition.z, realtiveposition.y, realtiveposition.x);
                    else if (Mathf.Abs(realtiveposition.x) < Mathf.Abs(realtiveposition.z)) newRealtiveposition = new Vector3(-realtiveposition.z, realtiveposition.y, realtiveposition.x);
                }
                if (user.transform.localEulerAngles == new Vector3(0, 270, 0) && typesofValue == TypesofValue.relative)
                {
                    //Left
                    newRealtiveposition = new Vector3(-realtiveposition.x, realtiveposition.y, -realtiveposition.z);
                }
                if (user.transform.localEulerAngles == new Vector3(0, 180, 0) && typesofValue == TypesofValue.relative)
                {
                    //Back
                    if (realtiveposition.z > 0) newRealtiveposition = new Vector3(realtiveposition.x, realtiveposition.y, -realtiveposition.z);
                    else if (realtiveposition.z < 0) newRealtiveposition = new Vector3(-realtiveposition.x, realtiveposition.y, realtiveposition.z);
                    else if (realtiveposition.z == 0) newRealtiveposition = new Vector3(realtiveposition.z, realtiveposition.y, -realtiveposition.x);

                    if (Mathf.Abs(realtiveposition.x) > Mathf.Abs(realtiveposition.z)) newRealtiveposition = new Vector3(realtiveposition.z, realtiveposition.y, -realtiveposition.x);
                    else if (Mathf.Abs(realtiveposition.x) < Mathf.Abs(realtiveposition.z)) newRealtiveposition = new Vector3(realtiveposition.z, realtiveposition.y, -realtiveposition.x);
                }

                var position = newRealtiveposition + user.transform.position;
                tile = Instantiate(highlight, new Vector3(position.x, gridstartY + 0.01f, position.z), Quaternion.Euler(Vector3.right * 90));

                foreach (GameObject invisGridTile  in invisGridTiles)
                {
                    if(invisGridTile.transform.position == tile.transform.position)
                    {
                        valid = true;
                    }
                }

                if (valid)
                {
                    tile.transform.SetParent(this.gameObject.transform);

                    if (!visible)
                        tile.GetComponent<MeshRenderer>().enabled = false;

                    rangeTiles.Add(tile);
                    CheckTargetType(targetType);
                }
                else
                {
                    Destroy(tile);
                }
            }
        }
    }

    public void CheckTargetType(TargetType targetType)
    {
        for (int i = 0; i < rangeTiles.Count; i++)
        {
            if (targetType == TargetType.tiles)
            {
                if (rangeTiles[i].GetComponent<GetObjectonTile>().gameObjectOnTile != null)
                {
                    Destroy(tile);
                    rangeTiles.Remove(rangeTiles[i]);
                }
            }
            else if (targetType == TargetType.enemies)
            {
                if (rangeTiles[i].GetComponent<GetObjectonTile>().gameObjectOnTile != null)
                {
                    if (rangeTiles[i].GetComponent<GetObjectonTile>().gameObjectOnTile.GetComponent<GetStats>().character.relation != RelationType.Enemy)
                    {
                        Destroy(tile);
                        rangeTiles.Remove(rangeTiles[i]);
                    }
                }
                else
                {
                    Destroy(tile);
                    rangeTiles.Remove(rangeTiles[i]);
                }
            }
            else if (targetType == TargetType.ally)
            {
                if (rangeTiles[i].GetComponent<GetObjectonTile>().gameObjectOnTile != null)
                {
                    if (rangeTiles[i].GetComponent<GetObjectonTile>().gameObjectOnTile.GetComponent<GetStats>().character.relation != RelationType.Friendly)
                    {
                        Destroy(tile);
                        rangeTiles.Remove(rangeTiles[i]);
                    }
                }
                else
                {
                    Destroy(tile);
                    rangeTiles.Remove(rangeTiles[i]);
                }
            }
        }
    }

    public GameObject GetTilePrefabClone()
    {
        return tilePrefabclone;
    }

    public void DestroyTiles(DestroyOption destroyOption, bool clearList, bool destroyTiles)
    {
        if (destroyOption != DestroyOption.selectedTiles)
        {
            if(destroyTiles)
                foreach (GameObject tile in rangeTiles)
                {
                    Destroy(tile);
                }
            if(clearList)
                rangeTiles.Clear();
        }
        if (destroyOption != DestroyOption.rangeTiles)
        {
            if(destroyTiles)
                foreach (GameObject tile in selectedTiles)
                {
                    Destroy(tile);
                }
            if(clearList)
                selectedTiles.Clear();
        }
    }

    IEnumerator WaitUntilDestroy(GameObject gameObject, bool _destroy, float _timeBeforeDestroy)
    {
        yield return new WaitForSecondsRealtime(_timeBeforeDestroy);
        if (_destroy)
        {
            selectedTiles.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
