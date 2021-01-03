using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using Random = UnityEngine.Random;

public class CardSystem : MonoBehaviour
{
    [Header("Required")]
    public GameObject Player;
    public int startingCardCount;
    public int maxCardCount;
    public int y_start;
    public int gap;

    [Header("Optional")]
    public Vector3 selectedPos;

    private List<GameObject> places = new List<GameObject>();
    private List<Card> handcards = new List<Card>();
    private int lastIndex;
    private Card[] drawableCards;
    private Card[] uniqueCards;
    private Vector3 placePos;

    private TurnSystem turnSystem;

    private void Awake()
    {
        drawableCards = Player.GetComponentInChildren<GetStats>().normalskills;
        uniqueCards = Player.GetComponentInChildren<GetStats>().uniqueSkills;
        turnSystem = FindObjectOfType<TurnSystem>();

        GameObject empty = new GameObject("place");

        for (int i = 0; i < maxCardCount; i++)
        {
            placePos = new Vector3(this.transform.position.x - (this.GetComponent<RectTransform>().rect.width / 2) + ((drawableCards[0].template.GetComponent<RectTransform>().rect.width + gap) * i) + drawableCards[0].template.GetComponent<RectTransform>().rect.width / 2 ,  y_start, this.transform.position.z);
            
            var place = Instantiate(empty,placePos, this.transform.rotation);
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
        Destroy(places[index].GetComponentInChildren<DragDrop>().gameObject);
        for (int i = index + 1; i < lastIndex; i++)
        {
            var old_cardObj = places[i].GetComponentInChildren<DragDrop>().gameObject;
            var old_card = places[i].GetComponentInChildren<GetCardInfo>().card;
            Destroy(old_cardObj);

            var new_cardObj = Instantiate(old_card.template, places[i - 1].transform);
            new_cardObj.transform.SetParent(places[i - 1].transform);
            new_cardObj.GetComponent<DragDrop>().CardGameObject = new_cardObj;
            new_cardObj.GetComponent<DragDrop>().selectedPos = selectedPos;
            new_cardObj.GetComponent<DragDrop>().index = i - 1;
        }
        handcards.RemoveAt(index);
        lastIndex--;
    }

    public void DrawCard()
    {
        if (turnSystem.GetBattleStatus() == BattleStatus.Combat && turnSystem.currentTurn == Player.GetComponent<GetStats>())
        {
            //turnSystem.NextTurn();
            if (lastIndex != maxCardCount)
            {
                InstantiateCard(lastIndex);
                lastIndex++;
            }
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
        cardObj.GetComponent<DragDrop>().index = index;
        cardObj.GetComponent<DragDrop>().CardGameObject = cardObj;
        cardObj.GetComponent<DragDrop>().selectedPos = selectedPos;
        handcards.Add(card);
    }

    public void ResetCardSelection(int index)
    {
        foreach (GameObject place in places)
        {
            try
            {
                if (place.GetComponentInChildren<DragDrop>().GetSelectionStatus() && place.GetComponentInChildren<DragDrop>().index != index)
                    place.GetComponentInChildren<DragDrop>().Deselect();
                
            }
            catch (Exception)
            {
                continue;
            }
        }
    }
}


