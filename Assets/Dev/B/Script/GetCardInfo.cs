using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetCardInfo : MonoBehaviour
{
    public Card card;

    public TMP_Text textname;
    public TMP_Text textmana;
    public Image image;

    private void Awake()
    {
        textname.SetText(card.skillname);
        textmana.SetText(card.manacost.ToString("n0"));
        image.sprite = card.pic;
    }
}
