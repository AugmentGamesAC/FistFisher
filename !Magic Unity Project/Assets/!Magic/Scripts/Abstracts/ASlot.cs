using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class ASlot : MonoBehaviour
{
    protected bool m_IsHighlighted;
    [SerializeField]
    protected ASlottable.SlotTypes m_SlotFilter;
    protected ASlotManager m_SlotManager;
    protected ASlottable m_Slotted;
    public ASlottable Slotted { get { return m_Slotted; } }

    [SerializeField]
    protected Vector3 m_SlottedOffset;
    [SerializeField]
    protected Vector3 m_SlottedOrientation;

    public void Start()
    {
        m_IsHighlighted = true;
        ToggleHighlighting();
        RegisterWithManager();
        //m_SlotFilter = ASlottable.SlotTypes.SpellCrystal;  //TEMPORARY TO MAKE SURE IT WORKS!
    }

    /// <summary>
    /// this code should discover what slotmanager in it's parent chain
    /// and report to it. 
    /// </summary>
    private void RegisterWithManager()
    {
        m_SlotManager = gameObject.GetComponentInParent<ASlotManager>();
        if(m_SlotManager!=null)
            m_SlotManager.RegisterSlot(this);
    }


    //accept a slottable into slot
    public virtual bool Accept(ASlottable slottable)
    {
        if (!CanAccept(slottable))
            return false;
        //if (slottable != null)
        //{
            if (m_Slotted != null && m_Slotted != slottable) //if it already has something in slot, drop it
            {
                m_Slotted.SlotDrop();
            }
            slottable.ToggleKinematicAndGravityAndSphereCollider(false);
            //Debug.LogWarning("attached to slot");
            slottable.gameObject.transform.parent = gameObject.transform;
            slottable.gameObject.transform.position = gameObject.transform.position + m_SlottedOffset;
            slottable.gameObject.transform.rotation = Quaternion.LookRotation(m_SlottedOrientation);
            m_Slotted = slottable;
            m_SlotFilter = slottable.SlotType;
        //}

        return true;
    }
    public bool CanAccept(ASlottable slotable)
    {
        return ((slotable.SlotType & m_SlotFilter) > 0);
    }


    public void Eject()
    {
        //ToggleHighlighting(false);
        //Debug.LogWarning("detached from slot");
        m_Slotted.gameObject.transform.SetParent(null);
        m_Slotted.SlotDrop();
        m_Slotted = null;
        WasEmptied();
    }

    public abstract void ToggleHighlighting(bool toggle);
    public void ToggleHighlighting()
    {
        m_IsHighlighted = !m_IsHighlighted;
        ToggleHighlighting(m_IsHighlighted);
    }

    public void WasEmptied()
    {
        if (m_SlotManager != null)
            m_SlotManager.SlotUpdate(this);
    }
}
