using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ASlotRender : MonoBehaviour, IDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler 
{
    protected SlotData m_SlotData;
    public SlotData SlotData => m_SlotData;

    protected SlotManager m_SlotManager;

    public void Start()
    {
        m_SlotManager = GetComponentInParent<SlotManager>();
        if (m_SlotManager == default)
            throw new System.InvalidOperationException("SlotData Has no manager");
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}

