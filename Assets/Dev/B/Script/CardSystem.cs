using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSystem : MonoBehaviour
{
    public int startingCardCount;
    public int maxCardCount;
    public Card[] drawableCards;
    public Card[] uniqueCards;
    public GameObject template;
    public int x_start;
    public int y_start;
    public int gap;

    private List<GameObject> places = new List<GameObject>();
    private List<Card> handcards = new List<Card>();
    private int lastIndex;

    private void Awake()                    
    {
        GameObject empty = new GameObject();

        for (int i = 0; i < maxCardCount; i++)
        {
            var obj = Instantiate(empty, drawableCards[0].template.transform);
            obj.transform.SetParent(this.transform);
            obj.transform.position = new Vector3(x_start + ((drawableCards[0].template.GetComponentInChildren<RectTransform>().rect.width + 19 + gap) * i), y_start, obj.transform.position.z);
            places.Add(obj);
        }

        if (startingCardCount <= maxCardCount)
        {
            for (int i = 0; i < startingCardCount; i++)
            {
                var cardobj = PickRandCard(drawableCards, uniqueCards);
                var obj = Instantiate(cardobj.template, places[i].transform);
                obj.transform.SetParent(places[i].transform);
                obj.GetComponentInChildren<DragDrop>().index = i;
                obj.AddComponent<Identify>();
                handcards.Add(cardobj);
            }
            lastIndex = startingCardCount;
        }
        else
        {
            for (int i = 0; i < maxCardCount; i++)
            {
                var cardobj = PickRandCard(drawableCards, uniqueCards);
                var obj = Instantiate(cardobj.template, places[i].transform);
                obj.transform.SetParent(places[i].transform);
                obj.GetComponentInChildren<DragDrop>().index = i;
                obj.AddComponent<Identify>();
                handcards.Add(cardobj);
            }
            lastIndex = maxCardCount;
        }
    }

    public void PlayCard(int index)
    {
        Destroy(places[index].GetComponentInChildren<Identify>().gameObject);
        for (int i = index + 1; i < lastIndex; i++)
        {
            var old_cardObj = places[i].GetComponentInChildren<Identify>().gameObject;
            var old_card = places[i].GetComponentInChildren<GetCardInfo>().card;
            Destroy(old_cardObj);

            var new_cardObj = Instantiate(old_card.template, places[i - 1].transform);
            new_cardObj.transform.SetParent(places[i - 1].transform);
            new_cardObj.AddComponent<Identify>();
            new_cardObj.GetComponentInChildren<DragDrop>().index = i - 1;
        }
        handcards.RemoveAt(index);
        lastIndex--;
    }

    public void DrawCard()
    {
        if (lastIndex != maxCardCount)
        {
            var cardobj = PickRandCard(drawableCards, uniqueCards);
            var new_obj = Instantiate(cardobj.template, places[lastIndex].transform);
            new_obj.transform.SetParent(places[lastIndex].transform);
            new_obj.AddComponent<Identify>();
            new_obj.GetComponentInChildren<DragDrop>().index = lastIndex;
            lastIndex++;
            handcards.Add(cardobj);
        }
        else
        {
            Debug.LogError("Max card reached");
        }
    }

    public Card PickRandCard(Card[] possibilities, Card[] unique)
    {
        bool pass = false;

        int random = Random.Range(0, possibilities.Length);
        while (pass == false)
        {
            pass = true;

            for (int m = 0; m < unique.Length; m++)
            {
                if (unique[m] == possibilities[random])
                {
                    for (int i = 0; i < handcards.Count; i++)
                    {
                        if (handcards[i] == possibilities[random])
                        {
                            pass = false;
                            random = Random.Range(0, possibilities.Length);
                        }
                    }
                }
            }
        }
        return possibilities[random];
    }
}
