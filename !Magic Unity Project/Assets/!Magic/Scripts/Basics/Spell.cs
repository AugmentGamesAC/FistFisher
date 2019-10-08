using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell
{ 
    /// <summary>
    /// This is used to decide which elemental effect is displayed.
    /// </summary>
    [System.Flags]
    public enum Elements
    {
        Fire = 0x0001, //Damage Over Time  
        Ice = 0x0002, //Takes up space 
        Lightning = 0x0004, //Instant Damage
        Aiming = 0x0008, //used for aiming
    };

    /// <summary>
    /// This enum is for managing the different states and behavoirs
    /// that a player can interact with a spell.
    /// </summary>
    public enum InteractionType
    {
        StartCasting,
        StopCasting,
        BeginAiming,
        BypassAiming,
        Cancel
    };

    [SerializeField]
    public SpellDescription m_Description;
    [SerializeField]
    protected float m_ManaCost;
    public float ManaCost { get { return m_ManaCost; } }
    [SerializeField]
    protected Elements m_ElementTypes;
    public Elements ElementTypes { get { return m_ElementTypes; } }
    /// <summary>
    /// spelluser is updated every time aiming is begun;
    /// </summary>
    protected ASpellUser m_SpellUser;
    protected float m_ManaPerSecond;
    protected SpellInstance m_SpellState;
    protected SpellInstance m_SpellStateSecond;
    /// <summary>
    /// Away for the player to interact with the spell. 
    /// </summary>
    /// <param name="interaction">what the player is gesturing</param>
    /// <param name="interaction">what the player is gesturing</param>
    /// <param name="Intensity">if the gesture contains axis data</param>
    /// <returns>was a valid instruction</returns>
    public bool Interact(InteractionType interaction,ASpellUser spellUser = null, float Intensity = 0)
    {
        switch (interaction)
        {
            case InteractionType.BeginAiming:
                return ResolveBeginAiming(spellUser);
            case InteractionType.StartCasting:
                return BeginCasting();
            case InteractionType.Cancel:
                return Cancel();
            case InteractionType.StopCasting:
                return Cancel();
            default:
                return false;
        }
    }

    private bool ResolveBeginAiming(ASpellUser spellUser) { throw new System.NotImplementedException(); }
    private bool BeginCasting()
    {
        if ((m_SpellUser == null)|| (m_SpellState == null))
            return false;
        if (m_SpellState.InstantceState != SpellInstance.InstanceStates.IsAiming)
            return false;

       if (!m_Description.Aiming.HasFlag(SpellDescription.Aimings.CenteredBoxToFingerTip))
            m_SpellState.gameObject.transform.parent.SetParent(null);
        if (m_Description.Effect == SpellDescription.Effects.Summon)
            m_SpellState.gameObject.transform.parent.gameObject.layer = 0;

        m_SpellState.UpdateState(SpellInstance.InstanceStates.IsActive);

        return true;
    }
    private bool Cancel()
    {
        if (m_SpellState == null)
            return false;
        if (m_SpellState.InstantceState != SpellInstance.InstanceStates.IsAiming)
            throw new System.NotImplementedException();


        GameObject.Destroy(m_SpellState.gameObject);
        m_SpellState = null;


        return true;
    }
    
    private void ApplyRotaion(SpellDescription.Aimings aimings) { throw new System.NotImplementedException(); }
    private Vector3 CalculateTransform(SpellDescription.Aimings aiming) { throw new System.NotImplementedException(); }

    private bool ResolveFromFingerEndPoint() { throw new System.NotImplementedException(); }
    private bool ResolveFromFinger() { throw new System.NotImplementedException(); }
    private bool ResolveFromCaster() { throw new System.NotImplementedException(); }
}
