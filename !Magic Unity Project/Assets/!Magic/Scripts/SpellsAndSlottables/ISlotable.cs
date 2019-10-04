using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// properties to apply to things that can be picked up and/or slotted into something 
/// </summary>
public interface ISlotable
{
    GameObject gameObject { get; }
    SlotTypes m_slotType { get; }
    bool m_isHeldSlot { get; set; }
    bool m_isHeldPlayer { get; set; }
    float m_timeToDissolve { get; } //set time for how long it CAN last, internal value unmentioned for timetodie
    ISlot m_lastSelected { get; set; } // this is so that when the raycasted/highlighted object is no longer targed it can be turned off

}
