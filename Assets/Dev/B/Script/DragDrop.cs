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

    private bool isSelected = false;
    private bool isDraging = false;
    private bool successful = false;
    private Vector3 lastPos;
    private GetCardInfo getCardInfo;
    private CardSystem cardSystem;
    private SkillInfo skillInfo;
    private AllSkills allSkills;
    private EditedGridGenerator gridGenerator;

    private void Awake()
    {

        GameObject[] gameObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.GetComponent<CardSystem>()) cardSystem = gameObject.GetComponent<CardSystem>();
            if (gameObject.GetComponent<EditedGridGenerator>()) gridGenerator = gameObject.GetComponent<EditedGridGenerator>();
            if (gameObject.GetComponent<AllSkills>()) allSkills = gameObject.GetComponent<AllSkills>();
            if (gameObject.GetComponent<SkillInfo>()) skillInfo = gameObject.GetComponent<SkillInfo>();
        }
        getCardInfo = GetComponent<GetCardInfo>();
    }

    private void Update()
    {
        if (isSelected)
        {
            gridGenerator.DestroySkillTiles();
            gridGenerator.GenerateSkillTiles(getCardInfo.card.ranges, cardSystem.Player);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastPos = this.transform.position - selectedPos;
        isDraging = true;
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
            ResetCardPos();
            return;
        }

        skillInfo.SetCardID(getCardInfo.card);
        successful = allSkills.cast(getCardInfo.card, gridGenerator, cardSystem.Player);

        if (successful)
        {
          
            SendMessageUpwards("PlayCard", index);
            gridGenerator.DestroySkillTiles();

        }
        else
        {
            ResetCardPos();
            gridGenerator.DestroySkillTiles();
        }
        isDraging = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SendMessageUpwards("ResetCardSelection", index);
        if (!isSelected && !isDraging) Select();
        else if (!isDraging) Deselect();
    }

    public void Select()
    {
        CardGameObject.transform.position += selectedPos;
        gridGenerator.GenerateSkillTiles(getCardInfo.card.ranges, cardSystem.Player);
        isSelected = true;
    }

    public void Deselect()
    {
        CardGameObject.transform.position -= selectedPos;
        gridGenerator.DestroySkillTiles();
        isSelected = false;
    }

    public void ResetCardPos()
    {
        this.transform.position = lastPos;
        isDraging = false;
    }

    public bool GetSelectionStatus()
    {
        return isSelected;
    }
}
