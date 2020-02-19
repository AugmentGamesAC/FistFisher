using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SlotUI)),System.Serializable]
public class ASlotRender : CoreUIUpdater<SlotData,SlotUI,ISlotData>, IEndDragHandler, IDropHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    protected SlotManager m_SlotManager;


    public new void Awake()
    {
        base.Awake();
        if (m_tracker == default)
            m_tracker = new SlotData();
    }

    public void SetSlotIndex(int index) => m_tracker.SetIndex(index);

    public void Start()
    {
        m_SlotManager = GetComponentInParent<SlotManager>();
        if (m_SlotManager == default)
            throw new System.InvalidOperationException("SlotData Has no manager");
        m_SlotManager.RegisterSlot(m_tracker);
        UpdateTracker(m_tracker);
        var dropHandler = GetComponentInParent<SlotSpace>();
        dropHandler.RegisterSlot(Tracker);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if ((m_tracker == null) || (m_tracker.Item == default))
            return;
        m_SlotManager.HandleDrag(eventData);
    }

    protected override void UpdateState(ISlotData value)
    {
        m_UIElement.UpdateUI(value);
    }

    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_SlotManager.OnDrop(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_SlotManager.HandleHover(m_tracker, eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_SlotManager.HandleHover(default, eventData);
    }
}

