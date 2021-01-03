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
    private TurnSystem turnSystem;
    private GetBarInfo getBarInfo;
    private EditedGridGenerator gridGenerator;

    private void Awake()
    {
        turnSystem = FindObjectOfType<TurnSystem>();
        getBarInfo = FindObjectOfType<GetBarInfo>();
        gridGenerator = FindObjectOfType<EditedGridGenerator>();
        cardSystem = FindObjectOfType<CardSystem>();
        skillInfo = FindObjectOfType<SkillInfo>();
        allSkills = FindObjectOfType<AllSkills>();
        getCardInfo = GetComponent<GetCardInfo>();
    }

    private void Update()
    {
        if (isSelected && turnSystem.GetBattleStatus() == BattleStatus.Combat && turnSystem.currentTurn == cardSystem.Player.GetComponent<GetStats>()) 
        {
            gridGenerator.DestroyTiles(DestroyOption.rangeTiles, true, true);
            gridGenerator.GenerateSkillTiles(getCardInfo.card.ranges, getCardInfo.card.targetType, cardSystem.Player, TypesofValue.relative, true);
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
        successful = allSkills.cast(getCardInfo.card, gridGenerator, cardSystem.Player, BattleStatus.Combat, cardSystem.Player.GetComponent<GetStats>()) && this.transform.position.y >= heightUI;
        if (successful)
        {
            skillInfo.SetCardID(getCardInfo.card);
            getBarInfo.RefreshBar();
            SendMessageUpwards("PlayCard", index);
            gridGenerator.DestroyTiles(DestroyOption.all, true, true);
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
        if (turnSystem.GetBattleStatus() == BattleStatus.Combat && turnSystem.currentTurn == cardSystem.Player.GetComponent<GetStats>())
            gridGenerator.GenerateSkillTiles(getCardInfo.card.ranges, getCardInfo.card.targetType, cardSystem.Player, TypesofValue.relative, true);
        isSelected = true;
    }

    public void Deselect()
    {
        CardGameObject.transform.position -= selectedPos;
        if (turnSystem.GetBattleStatus() == BattleStatus.Combat && turnSystem.currentTurn == cardSystem.Player.GetComponent<GetStats>())
            gridGenerator.DestroyTiles(DestroyOption.rangeTiles, true, true);
        isSelected = false;
    }

    public void ResetCardPos()
    {
        this.transform.position = lastPos;
        if(turnSystem.GetBattleStatus() == BattleStatus.Combat && turnSystem.currentTurn == cardSystem.Player.GetComponent<GetStats>())
            gridGenerator.DestroyTiles(DestroyOption.all, true, true);
        isSelected = false;
    }

    public bool GetSelectionStatus()
    {
        return isSelected;
    }
}
