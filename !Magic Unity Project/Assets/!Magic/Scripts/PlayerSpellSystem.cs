using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSpellSystem : BasicSpellUser
{
    public enum CastingInteraction
    {
        Empty = 0,
    };


    protected SpellList m_SpellList;
    protected Spell QueuedSpell;

    /// <summary>
    ///     should be switching to "AimingState" assuming spell isn't already aimed
    /// </summary>
    ///<returns> if a new spell was queued</returns>
    public bool QueueSpell(int GestureRef) { throw new System.NotImplementedException(); }
    /// <summary>
    /// Drop from aimingstate
    /// </summary>
    /// <returns>if there was a dequedSpell</returns>
    public bool DequeSpell() { throw new System.NotImplementedException(); }

    public bool InteractWithSpell(CastingInteraction castingAction) { throw new System.NotImplementedException(); }

}
