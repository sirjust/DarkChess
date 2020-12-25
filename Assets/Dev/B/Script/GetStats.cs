using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class GetStats : MonoBehaviour
{
    [Header("Required")]
    public Character character;
    public bool haveBody = false;

    public Card[] normalskills;
    public Card[] uniqueSkills;


    [Header("Optional")]
    public Vector3 collidersize = new Vector3(1, 1, 1);
    public bool health = false;

    [Header("Assigned Automatically")]
    public CharInfo charInfo;
    public Card lastcastedSkill;

    private BoxCollider boxCollider;
    private Slider healthbar;

    private void Awake()
    {
        healthbar = (GetComponentInChildren<Slider>()) ? GetComponentInChildren<Slider>() : null;

        if (character.healthRepresentation == HealthRepresentation.healthbar && healthbar != null)
        {
            healthbar.maxValue = character.health;
            healthbar.value = character.currentHealth;
        }
        else if(healthbar != null)
        {
            healthbar.gameObject.SetActive(false);
        }

        charInfo = FindObjectOfType<CharInfo>();

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
