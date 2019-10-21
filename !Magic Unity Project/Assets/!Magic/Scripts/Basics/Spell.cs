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

    public List<SpellDescription> m_Descriptions;

    protected float m_ManCost;
    protected Elements m_ElementTypes;
    /// <summary>
    /// spelluser is updated every time aiming is begun;
    /// </summary>
    protected ASpellUser m_SpellUser;
    protected float m_ManaPerSecond;
    protected SpellInstance m_SpellState;

    /// <summary>
    /// Away for the player to interact with the spell. 
    /// </summary>
    /// <param name="interaction">what the player is gesturing</param>
    /// <param name="Intensity">if the gesture contains axis data</param>
    /// <returns></returns>
    public bool Interact(InteractionType interaction, float Intensity = 0) { throw new System.NotImplementedException(); }

    private void ResolveBeginAiming() { throw new System.NotImplementedException(); }
    private void Cancel() { throw new System.NotImplementedException(); }

    private void ApplyRotaion(SpellDescription.Aimings aimings) { throw new System.NotImplementedException(); }
    private Vector3 CalculateTransform(SpellDescription.Aimings aiming) { throw new System.NotImplementedException(); }

    private void ResolveFromFingerEndPoint() { throw new System.NotImplementedException(); }
    private void ResolveFromFinger() { throw new System.NotImplementedException(); }
    private void ResolveFromCaster() { throw new System.NotImplementedException(); }
}
