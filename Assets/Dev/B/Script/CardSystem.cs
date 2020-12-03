using System.Collections.Generic;
using UnityEngine;

public class CardSystem : MonoBehaviour
{
    public int amountstart;
    public int maxamount;
    public Card[] drawableCards;
    public GameObject template;
    public int x_start;
    public int y_start;
    public int gap;
    private List<GameObject> places = new List<GameObject>();
    public int lastIndex;
    
    private void Awake()
    {
        GameObject empty = new GameObject();

        for (int i = 0; i < maxamount; i++)
        {
            var obj = Instantiate(empty, template.transform);
            obj.transform.SetParent(this.transform);
            obj.transform.position = new Vector3(x_start + ((template.GetComponentInChildren<RectTransform>().rect.width + 19 + gap) * i), y_start, obj.transform.position.z);
            places.Add(obj);
        }

        if (amountstart <= maxamount)
        {
            for (int i = 0; i < amountstart; i++)
            {
                var obj = Instantiate(template, places[i].transform);
                obj.transform.SetParent(places[i].transform);
                obj.GetComponentInChildren<DragDrop>().index = i;
                obj.AddComponent<Identify>();
            }
            lastIndex = amountstart;
        }
        else
        {
            for (int i = 0; i < maxamount; i++)
            {
                var obj = Instantiate(template, places[i].transform);
                obj.transform.SetParent(places[i].transform);
                obj.GetComponentInChildren<DragDrop>().index = i;
                obj.AddComponent<Identify>();
            }
            lastIndex = maxamount;
        }
    }

    public void PlayCard(int index)
    {
        Destroy(places[index].GetComponentInChildren<Identify>().gameObject);
        for (int i = index + 1; i < lastIndex; i++)
        {
            var old_obj = places[i].GetComponentInChildren<Identify>().gameObject;
            Destroy(old_obj);

            var new_obj = Instantiate(template, places[i - 1].transform);
            new_obj.transform.SetParent(places[i - 1].transform);
            new_obj.AddComponent<Identify>();
            new_obj.GetComponentInChildren<DragDrop>().index = i - 1;
        }
        lastIndex--;
    }

    public void DrawCard()
    {
        if (lastIndex != maxamount)
        {
            var new_obj = Instantiate(template, places[lastIndex].transform);
            new_obj.transform.SetParent(places[lastIndex].transform);
            new_obj.AddComponent<Identify>();
            new_obj.GetComponentInChildren<DragDrop>().index = lastIndex;
            lastIndex++;
        }
        else
        {
            Debug.LogError("Max card reached");
        }
    }
}
