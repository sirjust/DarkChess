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

<<<<<<< HEAD
=======
private GetBarInfo getBarInfo;
>>>>>>> b509ddc8d8322b376f23c874791c023ceeeae1ef
    private EditedGridGenerator gridGenerator;

    private void Awake()
    {
<<<<<<< HEAD
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.GetComponent<CardSystem>()) cardSystem = gameObject.GetComponent<CardSystem>();
            if (gameObject.GetComponent<EditedGridGenerator>()) gridGenerator = gameObject.GetComponent<EditedGridGenerator>();
            if (gameObject.GetComponent<AllSkills>()) allSkills = gameObject.GetComponent<AllSkills>();
            if (gameObject.GetComponent<SkillInfo>()) skillInfo = gameObject.GetComponent<SkillInfo>();
           // if (gameObject.GetComponent<TurnSystem>()) turnSystem = gameObject.GetComponent<TurnSystem>();
        }
=======
        getBarInfo = FindObjectOfType<GetBarInfo>();
        gridGenerator = FindObjectOfType<EditedGridGenerator>();
        cardSystem = FindObjectOfType<CardSystem>();
        skillInfo = FindObjectOfType<SkillInfo>();
        allSkills = FindObjectOfType<AllSkills>();
>>>>>>> b509ddc8d8322b376f23c874791c023ceeeae1ef
        getCardInfo = GetComponent<GetCardInfo>();
    }

    private void Update()
    {
        if (isSelected)
        {
<<<<<<< HEAD
            gridGenerator.DestroyTiles();
=======
            gridGenerator.DestroyTiles(DestroyOption.rangeTiles);
>>>>>>> b509ddc8d8322b376f23c874791c023ceeeae1ef
            gridGenerator.GenerateSkillTiles(getCardInfo.card.ranges, cardSystem.Player, TypesofValue.relative);
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
<<<<<<< HEAD
        skillInfo.SetCardID(getCardInfo.card);
        successful = allSkills.cast(getCardInfo.card, gridGenerator, cardSystem.Player, BattleStatus.PlayerCombat) && this.transform.position.y <= heightUI;
        if (successful)
        {
            SendMessageUpwards("PlayCard", index);
            gridGenerator.DestroyTiles();
=======
        successful = allSkills.cast(getCardInfo.card, gridGenerator, cardSystem.Player, BattleStatus.PlayerCombat) && this.transform.position.y <= heightUI;
        if (successful)
        {
            skillInfo.SetCardID(getCardInfo.card);
            getBarInfo.RefreshBar();
            SendMessageUpwards("PlayCard", index);
>>>>>>> b509ddc8d8322b376f23c874791c023ceeeae1ef
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
        gridGenerator.GenerateSkillTiles(getCardInfo.card.ranges, cardSystem.Player, TypesofValue.relative);
        isSelected = true;
    }

    public void Deselect()
    {
        CardGameObject.transform.position -= selectedPos;
<<<<<<< HEAD
        gridGenerator.DestroyTiles();
=======
        gridGenerator.DestroyTiles(DestroyOption.rangeTiles);
>>>>>>> b509ddc8d8322b376f23c874791c023ceeeae1ef
        isSelected = false;
    }

    public void ResetCardPos()
    {
        this.transform.position = lastPos;
<<<<<<< HEAD
        gridGenerator.DestroyTiles();
=======
        gridGenerator.DestroyTiles(DestroyOption.all);
>>>>>>> b509ddc8d8322b376f23c874791c023ceeeae1ef
        isSelected = false;
    }

    public bool GetSelectionStatus()
    {
        return isSelected;
    }
}
