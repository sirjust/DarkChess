using TMPro;
using UnityEngine;

public class SkillInfo : MonoBehaviour
{
    [Header("Requiered")]
    public Card card;
    public TMP_Text textName;
    public TMP_Text textDescription;

    public void RefreshStats(Card _card)
    {
        textName.SetText(_card.skillName);
        textDescription.SetText(card.skillDesciption);
    }

    public void SetCardID(Card _card)
    {
        card = _card;
        RefreshStats(card);
    }
}
