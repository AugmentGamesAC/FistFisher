using UnityEngine;
using System.Collections;
using Valve.VR.InteractionSystem;

public class SpellGrenadeInstance : SpellInstance
{
    public GameObject explodePartPrefab;
    public int explodeCount = 1;

    public float minMagnitudeToExplode = 0f;

    private Valve.VR.InteractionSystem.Interactable interactable;

	private void Start()
    {
		interactable = GetComponent<Valve.VR.InteractionSystem.Interactable>();
		//interactable = gameObject.AddComponent<Valve.VR.InteractionSystem.Interactable>();
		//gameObject.AddComponent<Valve.VR.InteractionSystem.Throwable>();
		m_IsAiming = false;
		m_BaseSpellDamage = 0;
		m_Spell.m_Start.m_usage = SpellDescription.Usage.ManualDuration;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (interactable != null && interactable.attachedToHand != null) //don't explode in hand
            return;

        if (collision.impulse.magnitude > minMagnitudeToExplode)
        {
            for (int explodeIndex = 0; explodeIndex < explodeCount; explodeIndex++)
            {
				GameObject explodePart = (GameObject)GameObject.Instantiate(explodePartPrefab, this.transform.position, this.transform.rotation);
				if (m_SpellManager.m_SpellEffects.TryGetValue(m_Spell.m_elementType, out Material newMaterial))
					explodePart.GetComponentInChildren<Renderer>().material = newMaterial;
				explodePart.GetComponent<SpellInstance>().m_Spell = m_Spell;
				explodePart.GetComponent<SpellInstance>().m_Spell.m_isAiming = false;
				explodePart.GetComponent<SpellInstance>().m_Spell.m_Start.m_usage = SpellDescription.Usage.Instant;
            }

            Destroy(this.gameObject);
        }
    }
}
