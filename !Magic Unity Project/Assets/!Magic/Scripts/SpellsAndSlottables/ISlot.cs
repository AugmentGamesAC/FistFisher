using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// used to gate what is acceptable for a slot
/// </summary>
[Flags]
public enum SlotTypes
{
    Shape = 0x0001, //The form the spell takes
    Effect = 0x0002, //The spell's interaction with the world
    Function = 0x0004, //The player's interaction with the spell
    Detail = 0x0008, //Placeholder with potential for use
    IsDescriptionCrystal = Shape | Effect | Function | Detail,

    SpellCrystal = 0x0010,
    /// <summary>
    /// filler section for demo purposes
    /// </summary>
    Dunno = 0x0020,
    Filler = 0x0040,
    Concept = 0x0080,
}

/// <summary>
/// properties to give to a slot so it knows what to do and/or what can be slotted in it
/// </summary>
public interface ISlot
{
    void Eject();
    GameObject gameObject { get; }
    ISlotable m_slotted { get; set; } 
    bool CanAccept(ISlotable slotableType);
    bool Accept(ISlotable slottingObject);
    void ToggleHighlighting(); // this will need more work if we ever network the game //refer to whatever sean's already done
    void ToggleHighlighting(bool toggle);
}
