using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerNotMagicSystem : BasicNotMagicUser
{
    public enum CastingInteraction
    {
        Empty = 0,
    };


    protected NotMagicList m_NotMagicList;
    protected NotMagic QueuedNotMagic;

    /// <summary>
    ///     should be switching to "AimingState" assuming spell isn't already aimed
    /// </summary>
    ///<returns> if a new spell was queued</returns>
    public bool QueueNotMagic(int GestureRef) { throw new System.NotImplementedException(); }
    /// <summary>
    /// Drop from aimingstate
    /// </summary>
    /// <returns>if there was a dequedSpell</returns>
    public bool DequeNotMagic() { throw new System.NotImplementedException(); }

    public bool InteractWithNotMagic(CastingInteraction castingAction) { throw new System.NotImplementedException(); }

}
