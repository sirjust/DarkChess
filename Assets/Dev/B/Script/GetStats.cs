using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class GetStats : MonoBehaviour
{
    public Character character;
    public CharInfo charInfo;
    public int size = 4;

    private BoxCollider boxCollider;
    private GameObject body;
    private GameObject[] allObj;

    private void Awake()
    {
        allObj = FindObjectsOfType<GameObject>();
        foreach (GameObject _gameObject in allObj)
        {
            if (_gameObject.GetComponent<CharInfo>())
            {
                charInfo = _gameObject.GetComponent<CharInfo>();
            }
        }

        boxCollider = GetComponent<BoxCollider>();
        boxCollider.size = new Vector3(size, size, size);

        var obj = Instantiate(character.Model, this.gameObject.transform);
        obj.transform.SetParent(this.gameObject.transform);
        body = obj;

        charInfo.DisableMenu(false);
    }

    private void Update()
    {
        this.gameObject.transform.position = body.transform.position;
    }

    private void OnMouseDown()
    {
        charInfo.getCharID(character);
        charInfo.RefreshStats(character);
    }
}
