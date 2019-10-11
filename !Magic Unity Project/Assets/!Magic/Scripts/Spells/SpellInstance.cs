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

    public void UpdateMaterial(Spell.Elements elementalEffect)
    {
        Material newMaterial;
        if (SpellManager.SpellBehaviour.SpellInstaceMatLookup.TryGetValue((m_InstanceState == InstanceStates.IsAiming) ? Spell.Elements.Aiming : m_Spell.ElementType, out newMaterial))
            gameObject.GetComponentInChildren<Renderer>().material = newMaterial;
    }

    public void UpdateState(InstanceStates state)
    {
        m_InstanceState = state;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!((m_InstanceState == InstanceStates.IsAiming) || (m_InstanceState == InstanceStates.IsEviromental)))
            return;



    }
}
