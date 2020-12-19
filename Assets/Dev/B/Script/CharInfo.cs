using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharInfo : MonoBehaviour
{
    [Header("Required")]
    public TMP_Text charName;
    public TMP_Text charValues;
    public TMP_Text charStats;
    public Image charPic;

    [Header("Assigned Automatically")]
    public Character character;

    private Image[] allImages;
    private TextMeshProUGUI[] allGUI;

    public void DisableMenu(bool mode)
    {
        allImages = this.gameObject.GetComponentsInChildren<Image>();
        allGUI = this.gameObject.GetComponentsInChildren<TextMeshProUGUI>();

        foreach (Image image in allImages)
        {
            image.enabled = mode;
        }

        foreach (TextMeshProUGUI UGUI in allGUI)
        {
            UGUI.enabled = mode;
        }
    }

    public void RefreshStats(Character _character)
    {
        charName.SetText(_character.charName);
        charPic.sprite = _character.picture;
        charStats.SetText($"DMG         {_character.strength.ToString("00")} \nCRIT         {_character.critRate.ToString("00")} \nDOGDE    {_character.dodgeRate.ToString("00")} \nARMOR    {_character.defense.ToString("00")}");
        
        charValues.SetText($"{_character.currentHealth} / {_character.health} \n {_character.currentMana} / {_character.mana}");

        if (_character.realtion == RealtionType.Enemy) charPic.color = Color.red;
        else charPic.color = Color.green;
    }

    public void SetCharID(Character _character)
    {
        character = _character;
        RefreshStats(character);
    }
}