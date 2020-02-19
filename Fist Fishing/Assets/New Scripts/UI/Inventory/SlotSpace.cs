using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotSpace :MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    protected SlotManager m_SlotManager;
    protected ISlotData m_slotRef;
    public void Start()
    {
        m_SlotManager = GetComponentInParent<SlotManager>();
        if (m_SlotManager == default)
            throw new System.InvalidOperationException("SlotData Has no manager");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (m_slotRef != default)
            m_SlotManager.HandleHover(m_slotRef);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_SlotManager.HandleHover(default);
    }

    public void RegisterSlot(ISlotData slotref)
    {
        m_slotRef = slotref;
    }

    public void OnDrop(PointerEventData eventData)
    {
        m_SlotManager.HandleSlotDrop(eventData, m_slotRef);
    }
}

