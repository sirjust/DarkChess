using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetCardInfo : MonoBehaviour
{
    [Header("Required")]
    public Card card;
    public TMP_Text textName;
    public TMP_Text textMana;
    public Image image;

    private void Awake()
    {
        textName.SetText(card.skillName);
        textMana.SetText(card.manaCost.ToString("n0"));
        image.sprite = card.skillPic;
        image.enabled = true;
    }
}
