using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpellInstance : MonoBehaviour
{
    /// <summary>
    /// This allow spells to resolve specific conditions and behavoirs
    /// </summary>
    public enum InstanceStates
    {
        Unset = 0,
        IsAiming,
        IsActive,
        IsEviromental
    }
    [SerializeField]
    protected InstanceStates m_InstanceState;
    public InstanceStates InstantceState { get { return m_InstanceState; } }


    public Spell m_Spell;
    /// <summary>
    /// way to ensure that instant damage is not reapplied
    /// </summary>
    protected Dictionary<ASpellUser, float> DamageTimingResolution;

    public void UpdateMaterial(Spell.Elements elementalEffect) { throw new System.NotImplementedException(); }
    public void UpdateState(InstanceStates state) { m_InstanceState = state; }
}
