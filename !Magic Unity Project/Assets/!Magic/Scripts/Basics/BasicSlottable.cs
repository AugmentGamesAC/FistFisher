using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Valve.VR.InteractionSystem.Throwable))]
public class BasicSlottable : ASlottable
{
    public override void PlayerDrop()
    {
        throw new System.NotImplementedException();
    }

    public override void PlayerGrab()
    {
        throw new System.NotImplementedException();
    }

    public override void SlotDrop()
    {
        throw new System.NotImplementedException();
    }
}
