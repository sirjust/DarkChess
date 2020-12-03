using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharInfo : MonoBehaviour
{
    public Character character;
    public TMP_Text charname;
    public TMP_Text values;
    public TMP_Text stats;
    public Image charpic;

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
        charname.SetText(_character.charname);
        values.SetText($"{_character.currenthealth} / {_character.health} \n {_character.currentmana} / {_character.mana}");
        charpic.sprite = _character.picture;
        stats.SetText($"DMG         {_character.strenght.ToString("00")} \nCRIT         {_character.critRate.ToString("00")} \nDOGDE    {_character.dogdeRate.ToString("00")} \nARMOR    {_character.defense.ToString("00")}");
    }

    public void getCharID(Character _character)
    {
        character = _character;
    }
}
