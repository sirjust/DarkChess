using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public float heightUI;

    private Vector3 lastPos;
    public int index;

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastPos = this.transform.position;   
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(this.transform.position.y <= heightUI)
        {
            this.transform.position = lastPos;
            return;
        }

        SendMessageUpwards("PlayCard", index);        
        Destroy(this.gameObject.GetComponentInParent<Transform>().gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

}
