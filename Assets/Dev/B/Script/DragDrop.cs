using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [Header("Requiered")]
    public float heightUI;

    [Header("Assigned Automatically")]
    public int index;
    public GameObject CardGameObject;
    public Vector3 selectedPos;
    private Vector3 lastPos;
    private GetCardInfo getCardInfo;
    private AllSkills allSkills;
    public bool isSelected = false;

    private void Awake()
    {
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();
        foreach(GameObject gameObject in gameObjects)
        {
            if (gameObject.GetComponent<AllSkills>())
            {
                allSkills = gameObject.GetComponent<AllSkills>();
            }
        }

        getCardInfo = GetComponent<GetCardInfo>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastPos = this.transform.position - selectedPos;
        isSelected = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (this.transform.position.y <= heightUI)
        {
            this.transform.position = lastPos;
            return;
        }

        SendMessageUpwards("PlayCard", index);
        allSkills.cast(getCardInfo.card.skill);
        Destroy(this.gameObject.GetComponentInParent<Transform>().gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isSelected)
        {
            CardGameObject.transform.position += selectedPos;
            isSelected = true;
        }
        else
        {
            CardGameObject.transform.position -= selectedPos;
            isSelected = false;
        }
    }
}
