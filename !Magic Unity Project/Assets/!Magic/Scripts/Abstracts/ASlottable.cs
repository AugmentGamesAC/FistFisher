using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class ASlottable : AGrabable
{
    /// <summary>
    /// used to gate what is acceptable for a slot
    /// </summary>
    [Flags]
    public enum SlotTypes
    {
        Undefined = 0x0000, //to clear slot type

        Shape = 0x0001, //The form the spell takes
        Effect = 0x0002, //The spell's interaction with the world
        Function = 0x0004, //The player's interaction with the spell
        Detail = 0x0008, //Placeholder with potential for use
        IsDescriptionCrystal = Shape | Effect | Function | Detail,

        SpellCrystal = 0x0010,
    }

    protected bool m_IsSlotted;
    protected float m_TimeToDie = 30;
    protected float m_TimeToDissolve = 30;
    protected ASlot m_SlotRef;
    //Proposed readonly values
    [SerializeField]
    protected SlotTypes m_SlotType;
    public SlotTypes SlotType { get { return m_SlotType; } }


    public float m_MinDistanceToDetectSlots = 0;
    public float m_MaxDistanceToDetectSlots = 0.1f;
    public string[] m_LayerMask = { "Slot Interaction" };
    protected ASlot m_LastSelected;

    void Start()
    {
        m_TimeToDie = m_TimeToDissolve;
    }

    public abstract void SlotDrop();
    public abstract void AssignSlot(ASlot targetSlot);
}
