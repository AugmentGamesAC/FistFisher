using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


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

    public float m_projectileMoveSpeed = 5.0f;
    public float m_maxExplosionScale = 6.0f;
    public Rigidbody m_rigidBody;
    public float m_destroyDelay = 1.0f;
    public bool m_allowExplodeUpdate = false;

    [SerializeField]
    protected InstanceStates m_InstanceState;
    public InstanceStates InstantceState { get { return m_InstanceState; } }

    public void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_CooldownList = new Dictionary<ASpellUser, float>();
    }

    public Spell m_Spell;
    /// <summary>
    /// way to ensure that instant damage is not reapplied
    /// </summary>
    protected Dictionary<ASpellUser, float> m_CooldownList;

    public void UpdateMaterial(Spell.Elements elementalEffect)
    {
        Material newMaterial;
        if (SpellManager.Instance.SpellInstaceMatLookup.TryGetValue((m_InstanceState == InstanceStates.IsAiming) ? Spell.Elements.Aiming : m_Spell.ElementType, out newMaterial))
            gameObject.GetComponentInChildren<Renderer>().material = newMaterial;
    }

    public void UpdateState(InstanceStates state)
    {
        m_InstanceState = state;

        if (m_InstanceState != InstanceStates.IsActive)
            return;

        resolveDuration();

        if (m_Spell.Description.Effect != SpellDescription.Effects.Summon)
            return;
    }


    private void resolveDuration()
    {
        if (m_Spell.Description.Usage == SpellDescription.Usages.Instant)
            Destroy(this.gameObject, 0.5f);
        else if (m_Spell.Description.Usage == SpellDescription.Usages.SetTime)
            Destroy(this.gameObject, 5f);
    }

    private void FixedUpdate()
    {
        //this block stops everything after it from working.
        ASpellUser[] keys = m_CooldownList.Keys.ToArray();

        foreach (ASpellUser damaged in keys)
        {
            m_CooldownList[damaged] -= Time.deltaTime;
            if (m_CooldownList[damaged] <= 0)
                m_CooldownList.Remove(damaged);
        }

        ResolveProjectileBehaviour();

        if (!m_allowExplodeUpdate)
            return;

        gameObject.transform.localScale += Vector3.one * m_projectileMoveSpeed * Time.deltaTime;
 
        if (gameObject.transform.localScale.magnitude < m_maxExplosionScale)
            Destroy(gameObject,  m_destroyDelay);
       
    }

    //TODO: some logic for how exploision is to be handled.
    private void ResolveExplosionBehavoir()
    {
        if (!m_Spell.Description.Effect.HasFlag(SpellDescription.Effects.ImpactGenade))
            return;
        //grow speed, stop movement, and dissapear after it's done.
        //while smaller than maximum explosion size, grow.
        m_allowExplodeUpdate = true;

        m_rigidBody.useGravity = false;
        m_rigidBody.velocity = Vector3.zero;
    }


    private void ResolveProjectileBehaviour()
    {
        if (!m_Spell.Description.Effect.HasFlag(SpellDescription.Effects.Projectile))
            return;

        //turn off gravity for projectiles.
        m_rigidBody.useGravity = false;

        //movement
        gameObject.transform.position += gameObject.transform.forward * m_projectileMoveSpeed * Time.deltaTime;
    }

    private void ProjectileOnHit(Collider other)
    {
        //cast other to spell user to apply damage to.
        ASpellUser otherSpellUser = other.gameObject.GetComponent<ASpellUser>();

        if (otherSpellUser == null)
        {
            Object.Destroy(gameObject);
            return;
        }

        //deal damage.
        otherSpellUser.TakeDamage(m_Spell.Damage);
        //apply knockback if there is some.

        //apply additional effects.

        //projectile always destroys itself on hit.
        Object.Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        //check effect type, if projectile, deal damage to other, destroy object.
        if (m_Spell.Description.Effect.HasFlag(SpellDescription.Effects.Projectile))
            ProjectileOnHit(other);

        if (m_Spell.Description.Effect.HasFlag(SpellDescription.Effects.ImpactGenade))
            ResolveExplosionBehavoir();

        //conditions for each effect type needed.
    }


    private void OnTriggerStay(Collider other)
    {
        if (!((m_InstanceState == InstanceStates.IsAiming) || (m_InstanceState == InstanceStates.IsEviromental)))
            return;

        ASpellUser otherSpellUser = other.gameObject.GetComponent<ASpellUser>();

        if (otherSpellUser == null)
            return;

        GameObject otherEnemy = other.gameObject;

        if (!m_CooldownList.ContainsKey(otherSpellUser))
        {
            otherSpellUser.TakeDamage(m_Spell.Damage);
            if (!otherSpellUser.IsDead())
                m_CooldownList.Add(otherSpellUser, m_Spell.Cooldown);
            return;
        }
    }
}
