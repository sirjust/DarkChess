using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class GetStats : MonoBehaviour
{
    [Header("Requiered")]
    public Character character;
    public bool haveBody = false;

    public Card[] normalskills;
    public Card[] uniqueSkills;


    [Header("Optional")]
    public Vector3 collidersize = new Vector3(1, 1, 1);
    public bool health = false;
    public float gapBeforeLast = 2;
    public float multiplier = 2;

    [Header("Assigned Automatically")]
    public CharInfo charInfo;

    private BoxCollider boxCollider;
    private GameObject[] allObj;
    private Slider healthbar;


    private void Awake()
    {

        healthbar = GetComponentInChildren<Slider>();

        if (character.healthRepresentation == HealthRepresentation.healthbar)
        {
            healthbar.maxValue = character.health;
            healthbar.value = character.currentHealth;
        }
        else
        {
            healthbar.gameObject.SetActive(false);
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

    private void OnMouseDown()
    {
        charInfo.SetCharID(character);
    }
}
