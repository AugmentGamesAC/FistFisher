using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class ASlot : MonoBehaviour
{
    protected bool m_IsHighlighted;
    protected ASlottable.SlotTypes m_SlotFilter;
    protected SlotRef m_SlotRef;



    public void Start()
    {
        m_IsHighlighted = true;
        ToggleHighlighting();
    }

    protected virtual SlotRef DefineSlotRef()
    {
        if (m_SlotRef != null)
            return m_SlotRef;

        //TODO: slotref needs implementation
        throw new NotImplementedException("slotref not implemented");
    }

    public virtual SlotRef Accept(ASlottable slottable)
    {
        if (!CanAccept(slottable))
            return null;

        //out with the old
        //InwiththeNew

        return DefineSlotRef();
    }


    public bool CanAccept(ASlottable slotable)
    {
        return ((slotable.SlotType & m_SlotFilter) > 0);
    }

    public abstract void ToggleHighlighting(bool toggle);
    public void ToggleHighlighting()
    {
        m_IsHighlighted = !m_IsHighlighted;
        ToggleHighlighting(m_IsHighlighted);
    }


}
