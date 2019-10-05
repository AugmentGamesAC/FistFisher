using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NotMagicInstance : MonoBehaviour
{
    public NotMagic m_NotMagic;
    /// <summary>
    /// way to ensure that instant damage is not reapplied
    /// </summary>
    protected Dictionary<ANotMagicUser, float> DamageTimingResolution;

    public void UpdateMaterial(NotMagic.Elements elementalEffect) { }
}
