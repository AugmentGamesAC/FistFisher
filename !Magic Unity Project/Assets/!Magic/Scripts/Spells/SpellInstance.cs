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

    public float m_projectileMoveSpeed = 5.0f;
    public float m_maxExplosionScale = 6.0f;
    public Rigidbody m_rigidBody;
    public int m_growAmount;

    [SerializeField]
    protected InstanceStates m_InstanceState;
    public InstanceStates InstantceState { get { return m_InstanceState; } }

    public void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
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
    }

    private void FixedUpdate()
    {
        //this block stops everything after it from working.
        //ASpellUser[] keys = new List<ASpellUser>(m_CooldownList.Keys).ToArray();

        //foreach (ASpellUser damaged in keys)
        //{
        //    m_CooldownList[damaged] -= Time.deltaTime;
        //    if (m_CooldownList[damaged] <= 0)
        //        m_CooldownList.Remove(damaged);
        //}
        //created inherited class and override function.

        ResolveProjectileBehavoir();
    }

    //TODO: some logic for how exploision is to be handled.
    private void ResolveExplosionBehavoir()
    {
        if (!m_Spell.Description.Effect.HasFlag(SpellDescription.Effects.ImpactGenade))
            return;
        //grow speed, stop movement, and dissapear after it's done.
        //while smaller than maximum explosion size, grow.
        while (gameObject.transform.localScale.magnitude < m_maxExplosionScale)
        {
            gameObject.transform.localScale += new Vector3(1, 1, 1) * m_projectileMoveSpeed * Time.deltaTime;
        }
        m_rigidBody.useGravity = false;
        m_rigidBody.velocity = Vector3.zero;

        Destroy(gameObject, m_growAmount);
    }


    //TODO: MISSING Progectile movement should be call in fixedUpdate
    private void ResolveProjectileBehavoir()
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
        ASpellUser otherSpellUser = (ASpellUser)other.gameObject.GetComponent(typeof(ASpellUser));

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

        ASpellUser otherSpellUser = (ASpellUser)other.gameObject.GetComponent(typeof(ASpellUser));

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
