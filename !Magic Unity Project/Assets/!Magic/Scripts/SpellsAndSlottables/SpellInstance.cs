using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInstance : MonoBehaviour
{
    public SpellManager m_SpellManager;
    public float m_BaseSpellDamage;
    public float m_ManaPerTick;
    public Spell m_Spell;
    public bool _IsAiming;

    public bool m_IsAiming
    {
        get { return _IsAiming; }
        set
        {
            _IsAiming = value;
            //only sets the material if it's a reconized material
            Material newMaterial;
            if (m_SpellManager.m_SpellEffects.TryGetValue((_IsAiming) ? Spell.Elements.Aiming : m_Spell.m_elementType, out newMaterial))
                gameObject.GetComponent<Renderer>().material = newMaterial;
        }
    }

    private void FixedUpdate()
    {
        if (m_Spell == null)
            return;
        m_Spell.FixedUpdate();
    }

    private void resolveDuration()
    {
        //TODO destroy the spell if/when apropriate
    }

    private void OnCollisionEnter(Collision collision)
    {
        //TODO determine if it's a object with an IManaUser component, then apply the damage.  
    }

}
