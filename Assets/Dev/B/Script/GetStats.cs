using UnityEngine;
using UnityEngine.UI;

public enum HealthRepresentation
{
    healthbar, hearts
}

[RequireComponent(typeof(BoxCollider))]
public class GetStats : MonoBehaviour
{
    public Character character;
    public CharInfo charInfo;

    public HealthRepresentation healthRepresentation;
    public Sprite heartsPic;
    public int size = 4;

    private BoxCollider boxCollider;
    private GameObject body;
    private GameObject[] allObj;
    private Slider healthbar;
    private Image heartsContainer;

    private void Awake()
    {
        healthbar = GetComponentInChildren<Slider>();
        heartsContainer = GetComponentInChildren<Image>();

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

        var charObj = Instantiate(character.Model, this.gameObject.transform);
        charObj.transform.SetParent(this.gameObject.transform);
        body = charObj;

        if(healthRepresentation == HealthRepresentation.healthbar)
        {
            heartsContainer.enabled = false;
        }
        else
        {
            heartsContainer.enabled = false;
        }

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
