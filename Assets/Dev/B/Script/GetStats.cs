using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class GetStats : MonoBehaviour
{
    [Header("Requiered")]
    public Character character;
    public GameObject hearttemplate;
    public Card[] normalskills;
    public Card[] uniqueSkills;
    public bool haveBody = false;


    [Header("Optional")]
    public Vector3 collidersize = new Vector3(1,1,1);
    public bool health = false;
    public float gapBeforeLast = 2;
    public float multiplier = 2;

    [Header("Assigned Automatically")]
    public CharInfo charInfo;

    private BoxCollider boxCollider;
    private GameObject[] allObj;
    private Slider healthbar;
    private GameObject heartsContainer;
    private List<GameObject> hearts = new List<GameObject>();


    private void Awake()
    {
        if (health)
        {
            healthbar = GetComponentInChildren<Slider>();
            heartsContainer = GetComponentInChildren<Identify>().gameObject;

            if (character.healthRepresentation == HealthRepresentation.healthbar)
            {
                heartsContainer.SetActive(false);
                healthbar.maxValue = character.health;
                healthbar.value = character.currentHealth;
            }
            else
            {
                healthbar.gameObject.SetActive(false);
                for (int i = 0; i < character.hearts; i++)
                {
                    InstantiateHearts(i);
                }
            }
        }

        allObj = FindObjectsOfType<GameObject>();

        foreach (GameObject _gameObject in allObj)
        {
            if (_gameObject.GetComponent<CharInfo>())
            {
                charInfo = _gameObject.GetComponent<CharInfo>();
            }
        }

        boxCollider = GetComponent<BoxCollider>();
        boxCollider.size = collidersize;

        if (!haveBody)
        {
            var charObj = Instantiate(character.Model, this.gameObject.transform);
            charObj.transform.SetParent(this.gameObject.transform);
        }
      
        charInfo.DisableMenu(false);
    }

    public void InstantiateHearts(int index)
    {
        if (multiplier > 0)
        {
            var pos = new Vector3((0 + ((gapBeforeLast / multiplier) * index)), 0, 0);
            var heartObj = Instantiate(hearttemplate, pos, this.gameObject.transform.rotation);
            heartObj.transform.SetParent(heartsContainer.GetComponentInParent<Transform>().gameObject.transform);
            heartObj.transform.localPosition = pos;
            heartObj.AddComponent<UILookToCanvas>();
            hearts.Add(heartObj);
            hearts[index].transform.localPosition -= new Vector3(gapBeforeLast, 0, 0);
        }
    }

    private void OnMouseDown()
    {
        charInfo.SetCharID(character);
    }
}
