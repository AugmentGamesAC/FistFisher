using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSlot : ASlot
{
    public override void ToggleHighlighting(bool toggle)
    {
        m_IsHighlighted = toggle;
        //Debug.Log("Toggled Highlighting to " + m_IsHighlighted);
    }



}
