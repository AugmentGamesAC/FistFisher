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
        ASpellUser[] keys = new List<ASpellUser>(m_CooldownList.Keys).ToArray();

        foreach (ASpellUser damaged in keys)
        {
            m_CooldownList[damaged] -= Time.deltaTime;
            if (m_CooldownList[damaged] <= 0)
                m_CooldownList.Remove(damaged);
        }
        ResolveProjectileBehavoir();
        ResolveExplosionBehavoir();
    }

    //TODO: some logic for how exploision is to be handled.
    private void ResolveExplosionBehavoir()
    {
        
    }


    //TODO: MISSING Progectile movement should be call in fixedUpdate
    private void ResolveProjectileBehavoir()
    {
        if (!m_Spell.Description.Effect.HasFlag(SpellDescription.Effects.Projectile))
            return;
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if there is a contact explosion code goes here
        throw new System.NotImplementedException();
    }

    private void ResolveExplosion()
    {

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
