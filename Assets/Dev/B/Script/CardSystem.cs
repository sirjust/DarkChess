using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardSystem : MonoBehaviour
{
    [Header("Requiered")]
    public GameObject Player;
    public int startingCardCount;
    public int maxCardCount;
    public int x_start;
    public int y_start;
    public int gap;

    [Header("Optional")]
    public Vector3 selectedPos;

    private List<GameObject> places = new List<GameObject>();
    private List<Card> handcards = new List<Card>();
    private int lastIndex;
    private Card[] drawableCards;
    private Card[] uniqueCards;

    private void Awake()
    {
        drawableCards = Player.GetComponentInChildren<GetStats>().normalskills;
        uniqueCards = Player.GetComponentInChildren<GetStats>().uniqueSkills;

        GameObject empty = new GameObject();
        empty.name = "place";

        for (int i = 0; i < maxCardCount; i++)
        {
            var place = Instantiate(empty, new Vector3(x_start + ((drawableCards[0].template.GetComponentInChildren<RectTransform>().rect.width + 19 + gap) * i), y_start, this.transform.position.z), this.transform.rotation);
            place.transform.SetParent(this.transform);
            places.Add(place);
        }

        if (startingCardCount <= maxCardCount)
        {
            for (int i = 0; i < startingCardCount; i++)
            {
                InstantiateCard(i);
            }
            lastIndex = startingCardCount;
        }
        else
        {
            for (int i = 0; i < maxCardCount; i++)
            {
                InstantiateCard(i);
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
            new_cardObj.GetComponentInChildren<DragDrop>().CardGameObject = new_cardObj;
            new_cardObj.GetComponentInChildren<DragDrop>().selectedPos = selectedPos;
            new_cardObj.GetComponentInChildren<DragDrop>().index = i - 1;
        }
        handcards.RemoveAt(index);
        lastIndex--;
    }

    public void DrawCard()
    {
        if (lastIndex != maxCardCount)
        {
            InstantiateCard(lastIndex);
            lastIndex++;
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

    public void InstantiateCard(int index)
    {
        var card = PickRandCard(drawableCards, uniqueCards);
        var cardObj = Instantiate(card.template, places[index].transform);
        cardObj.transform.SetParent(places[index].transform);
        cardObj.AddComponent<Identify>();
        cardObj.GetComponentInChildren<DragDrop>().index = index;
        cardObj.GetComponentInChildren<DragDrop>().CardGameObject = cardObj;
        cardObj.GetComponentInChildren<DragDrop>().selectedPos = selectedPos;
        handcards.Add(card);
    }

    public void ResetCardSelection(int index)
    {
        foreach (GameObject place in places)
        {
            try
            {
                if (place.GetComponentInChildren<DragDrop>().GetSelectionStatus() && place.GetComponentInChildren<DragDrop>().index != index)
                {
                    place.GetComponentInChildren<DragDrop>().Deselect();
                }
            }
            catch (Exception)
            {
                continue;
            }
        }
    }
}
