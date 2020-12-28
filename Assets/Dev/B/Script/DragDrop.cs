using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [Header("Required")]
    public float heightUI;

    [Header("Assigned Automatically")]
    public int index;
    public GameObject CardGameObject;
    public Vector3 selectedPos;

    private bool isSelected = false;
    private bool successful = false;

    private Vector3 lastPos;
    private GetCardInfo getCardInfo;
    private CardSystem cardSystem;
    private SkillInfo skillInfo;
    private AllSkills allSkills;
    private GetBarInfo getBarInfo;
    private EditedGridGenerator gridGenerator;

    private void Awake()
    {
        getBarInfo = FindObjectOfType<GetBarInfo>();
        gridGenerator = FindObjectOfType<EditedGridGenerator>();
        cardSystem = FindObjectOfType<CardSystem>();
        skillInfo = FindObjectOfType<SkillInfo>();
        allSkills = FindObjectOfType<AllSkills>();
        getCardInfo = GetComponent<GetCardInfo>();
    }

    private void Update()
    {
        if (isSelected)
        {
            gridGenerator.DestroyTiles(DestroyOption.rangeTiles);
            gridGenerator.GenerateSkillTiles(getCardInfo.card.ranges, getCardInfo.card.canTargetObjects, cardSystem.Player, TypesofValue.relative);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isSelected) lastPos = this.transform.position - selectedPos;
        else lastPos = this.transform.position;

        Select();
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        successful = allSkills.cast(getCardInfo.card, gridGenerator, cardSystem.Player, BattleStatus.PlayerCombat, true) && this.transform.position.y >= heightUI;
        if (successful)
        {
            skillInfo.SetCardID(getCardInfo.card);
            getBarInfo.RefreshBar();
            SendMessageUpwards("PlayCard", index);
        }
        else
        {
            ResetCardPos();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SendMessageUpwards("ResetCardSelection", index);
        if (!isSelected) Select();
        else Deselect();
    }

    public void Select()
    {
        CardGameObject.transform.position += selectedPos;
        gridGenerator.GenerateSkillTiles(getCardInfo.card.ranges, getCardInfo.card.canTargetObjects, cardSystem.Player, TypesofValue.relative);
        isSelected = true;
    }

    public void Deselect()
    {
        CardGameObject.transform.position -= selectedPos;
        gridGenerator.DestroyTiles(DestroyOption.rangeTiles);
        isSelected = false;
    }

    public void ResetCardPos()
    {
        this.transform.position = lastPos;
        gridGenerator.DestroyTiles(DestroyOption.all);
        isSelected = false;
    }

    public bool GetSelectionStatus()
    {
        return isSelected;
    }
}
