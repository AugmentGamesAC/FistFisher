using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AGrabable : MonoBehaviour
{
    protected bool m_IsGrabbed;

    public abstract void PlayerGrab();
    public abstract void PlayerDrop();
}
