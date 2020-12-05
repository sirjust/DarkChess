using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharInfo : MonoBehaviour
{
    public Character character;
    public TMP_Text charName;
    public TMP_Text charValues;
    public TMP_Text charStats;
    public Image charPic;

    private Image[] allimage;
    private TextMeshProUGUI[] allGUI;

    public void DisableMenu(bool mode)
    {
        allimage = this.gameObject.GetComponentsInChildren<Image>();
        allGUI = this.gameObject.GetComponentsInChildren<TextMeshProUGUI>();

        foreach (Image image in allimage)
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
        charValues.SetText($"{_character.currentHealth} / {_character.health} \n {_character.currentMana} / {_character.mana}");
        charPic.sprite = _character.picture;
        charStats.SetText($"DMG         {_character.strenght.ToString("00")} \nCRIT         {_character.critRate.ToString("00")} \nDOGDE    {_character.dogdeRate.ToString("00")} \nARMOR    {_character.defense.ToString("00")}");
    }

    public void getCharID(Character _character)
    {
        character = _character;
    }
}
