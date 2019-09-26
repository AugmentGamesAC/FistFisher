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
	public float m_Life;

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
		m_Life += Time.deltaTime;
        if (m_Spell == null)
            return;
        m_Spell.FixedUpdate();
		if(!m_IsAiming)
			resolveDuration();
    }

    private void resolveDuration()
    {
		if(m_Spell.m_Start.m_usage == SpellDescription.Usage.Instant && m_Life >= 1f)
			Destroy(this.gameObject);
		//TODO destroy the spell if/when apropriate
	}

    private void OnTriggerStay(Collider other)
    {
		IMagicUser otherMagicUser = (IMagicUser)other.gameObject.GetComponent(typeof(IMagicUser));

		if (otherMagicUser == null)
			return;

		otherMagicUser.TakeDamage(m_BaseSpellDamage);
    }

}
