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

    private float m_maxDamageCooldown = 120f;
    private float m_damageCooldown;

    public bool m_IsAiming
    {
        get { return _IsAiming; }
        set
        {
            _IsAiming = value;
            //only sets the material if it's a reconized material
            Material newMaterial;
            if (m_SpellManager.m_SpellEffects.TryGetValue((_IsAiming) ? Spell.Elements.Aiming : m_Spell.m_elementType, out newMaterial))
                gameObject.GetComponentInChildren<Renderer>().material = newMaterial;
        }
    }

    private void Start()
    {
        if (m_Spell.m_Start.m_usage == SpellDescription.Usage.Duration)
            m_maxDamageCooldown = 1f;
        m_damageCooldown = m_maxDamageCooldown;
        if (m_Spell.m_Start.m_usage == SpellDescription.Usage.Instant)
            m_BaseSpellDamage *= 34f;
    }

    private void FixedUpdate()
    {
        if (!m_IsAiming)
        {
            m_Life += Time.deltaTime;

            if (m_damageCooldown != m_maxDamageCooldown)
            {
                m_damageCooldown -= 1f;

                if (m_damageCooldown <= 0f)
                    m_damageCooldown = m_maxDamageCooldown;
            }
        }
        if (m_Spell == null)
            return;
        m_Spell.FixedUpdate();
		if(!m_IsAiming)
			resolveDuration();
    }

    private void resolveDuration()
    {
        if (m_Spell.m_Start.m_usage == SpellDescription.Usage.Instant && m_Life >= 0.5f)
        {
            Destroy(this.gameObject);
            return;
        }

        if (m_Spell.m_Start.m_usage == SpellDescription.Usage.Duration && m_Life >= 5f)
        {
            Destroy(this.gameObject);
            return;
        }


        //TODO destroy the spell if/when apropriate
    }

    private void OnTriggerStay(Collider other)
    {
        if (m_IsAiming)
            return;

        if (m_Spell.m_elementType.HasFlag(Spell.Elements.Fire))
        {
            SpellInstance otherSpell = other.gameObject.GetComponent<SpellInstance>();
            if (otherSpell != null && otherSpell.m_Spell.m_elementType == Spell.Elements.Ice)
                GameObject.Destroy(other.gameObject);

        }

		IMagicUser otherMagicUser = (IMagicUser)other.gameObject.GetComponent(typeof(IMagicUser));

        if (m_Spell.m_Start.m_effect != SpellDescription.Effect.Damage)
            return;
        if (otherMagicUser == null || m_damageCooldown != m_maxDamageCooldown)
            return;

        otherMagicUser.TakeDamage(m_BaseSpellDamage);
        m_damageCooldown -= 1f;
    }

}
