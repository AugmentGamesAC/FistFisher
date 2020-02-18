using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    /// <summary>
    /// contains all the slot data for the manager, the key being the index of the slot 
    /// so a "next" previous logic can be maintained/observed.
    /// </summary>
    protected Dictionary<int, ISlotData> m_mySLots = new Dictionary<int, ISlotData>();

    protected HashSet<int> m_freeSlots = new HashSet<int>();



    /// <summary>
    /// Adds a new slote to the SlotManager to handle
    /// </summary>
    /// <param name="slot"></param>
    public void RegisterSlot(ISlotData slot)
    {
        if (slot == default)
            return;

        m_mySLots.Add(slot.Index, slot);
        if (slot.Item == default)
            m_freeSlots.Add(slot.Index);
    }
    /// <summary>
    /// records when a slot's data is emptied
    /// </summary>
    /// <param name="slot"></param>
    public void FreeSlot(SlotData slot)
    {
        if (slot == default)
            return;

        m_freeSlots.Add(slot.Index);
    }

    /// <summary>
    /// Handle Slot Drop handles the logic between different slots and determins solution
    /// </summary>
    /// <param name="Droper">The slot being draged</param>
    /// <param name="Dropee">The slot being draged to</param>
    /// <returns></returns>
    public virtual bool HandleSlotDrop(SlotData Droper, SlotData Dropee)
    {
        return false;
    }

    /// <summary>
    /// Adds the item as many times as needed depending on slot restrictions 
    /// returns fall if cannot add.
    /// </summary>
    /// <param name="item"> the item desired to be added</param>
    /// <param name=""></param>
    /// <returns></returns>
    public virtual bool AddItem(IItem item, int count)
    {
        return false;
    }

    [SerializeField]
    protected List<FishDefintion> m_Fishies = new List<FishDefintion>();



    public void Update()
    {
        if (ALInput.GetKeyDown(KeyCode.Y))
            m_mySLots[1].AddItem(m_Fishies[0], 1);
    }




    public void Start()
    {
        if (CommonMountPointer != default)
            return;
        var mouseObject = new GameObject();
        CommonMountPointer = mouseObject.AddComponent<DragTracker>();
        CommonMountPointer.Rect = mouseObject.AddComponent<RectTransform>();
        CommonMountPointer.Rect.sizeDelta = new Vector2(32, 32);
        CommonMountPointer.DragImage = mouseObject.AddComponent<Image>();
    }

    protected static DragTracker CommonMountPointer;

    protected void HandleDragStart(PointerEventData eventData)
    {
        CommonMountPointer.transform.SetParent(transform);
        CommonMountPointer.transform.SetAsLastSibling();
        CommonMountPointer.gameObject.SetActive(true);
        CommonMountPointer.eventData = eventData;
        CommonMountPointer.DragImage.sprite = eventData.pointerDrag.GetComponentInChildren<Image>().sprite;
    }

    public void HandleDrag(PointerEventData eventData)
    {
        if (CommonMountPointer.eventData == default || CommonMountPointer.eventData.pointerDrag != eventData.pointerDrag)
        {
            HandleDragStart(eventData);
        }
        CommonMountPointer.Rect.position = Input.mousePosition + Vector3.forward * 20;
    }

    public void OnDrop(PointerEventData eventData)
    {
        CommonMountPointer.gameObject.SetActive(false);
        CommonMountPointer.eventData = default;
    }

    public void HandleSlotDrop(PointerEventData eventData, ISlotData dropped)
    {
        var slotref = CommonMountPointer.eventData.pointerDrag.GetComponent<ASlotRender>();


       int newvalue = dropped.CheckAddItem(slotref.Tracker.Item, slotref.Tracker.Count);
       if (newvalue == slotref.Tracker.Count)
       {
            OnDrop(eventData);
            return;
       }
       var delta = slotref.Tracker.Count - newvalue;
        //dropped needs to be added to first so that we don't loose ref to the IItem;
       dropped.AddItem(slotref.Tracker.Item, delta);
       slotref.Tracker.RemoveCount(delta);
    }


    public void HandleHover(IDropHandler dropee)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}

