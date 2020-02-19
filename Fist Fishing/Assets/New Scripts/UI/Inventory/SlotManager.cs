using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
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

    public void UseSlot(SlotData slot)
    {
        if (slot == default)
            return;

        m_freeSlots.Remove(slot.Index);

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
        ///eventually add code to limitObjects. 
        HashSet<int> usedSlots = new HashSet<int>();
        int myCount = count;
        while(myCount > 0 &&  m_freeSlots.Count > 0)
        {
            int targetSlot = m_freeSlots.Min();
            usedSlots.Add(targetSlot);
            myCount = m_mySLots[targetSlot].CheckAddItem(item, myCount);
        }
        if (myCount > 0)
            return false;
        myCount = count;
        foreach (int slotkey in usedSlots)
        {
            myCount = m_mySLots[slotkey].AddItem(item, myCount);
        }
        return true;
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
        CommonMountPointer.DragImage.raycastTarget = false;
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
        if (slotref.Tracker.Count == 0)
           FreeSlot(slotref.Tracker);
       OnDrop(eventData);
    }


    public void HandleHover(ISlotData dropee)
    {
        CommonMountPointer.SlotTarget = dropee;
    }
}

