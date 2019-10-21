using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpellInstance : MonoBehaviour
{
    public Spell m_Spell;
    /// <summary>
    /// way to ensure that instant damage is not reapplied
    /// </summary>
    protected Dictionary<ASpellUser, float> DamageTimingResolution;

    public void UpdateMaterial(Spell.Elements elementalEffect) { }
}
