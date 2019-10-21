using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class ASlot : MonoBehaviour
{
    protected bool m_IsHighlighted;
    protected ASlottable.SlotTypes m_SlotFilter;
    protected ASlotManager m_SlotManager;
    protected ASlottable m_Slotted;
    public ASlottable Slotted { get { return Slotted; } }

    protected Vector3 m_SlottedOffset;
    protected Vector3 m_SlottedOrientation;

    public void Start()
    {
        m_IsHighlighted = true;
        ToggleHighlighting();
        RegisterWithManager();
    }

    /// <summary>
    /// this code should discover what slotmanager in it's parent chain
    /// and report to it. 
    /// </summary>
    private void RegisterWithManager()
    {
        throw new NotImplementedException();
    }

    public virtual bool Accept(ASlottable slottable)
    {
        if (!CanAccept(slottable))
            return false;

        throw new NotImplementedException();
        //out with the old
        //InwiththeNew

        return true;
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

    public abstract void WasEmptied();
}
