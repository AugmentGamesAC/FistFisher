using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Gauntlet is used for maintaining spellcasting states and resolutions
/// until new behavoir is required 
/// </summary>
[RequireComponent(typeof(SpellList))]
public class Gauntlet : BasicSpellUser
{
    [SerializeField]
    protected SpellList m_spellList;
    public SpellList SpellList 
    { 
        get 
        {
            if (m_spellList == null)
                m_spellList = GetComponent<SpellList>();
            return m_spellList;
        }
    }

    protected Spell m_currentSpell;
    public Spell CurrentSpell {  get { return m_currentSpell; } }


    /// <summary>
    /// This simple triggering should be called to switch between spell states, spell selection is a different call
    /// </summary>
    public void ResolveSimpleTriggering()
    {
        if (m_currentSpell == default(Spell))
            throw new System.InvalidOperationException("Gauntlet trying to interact with no spell");

        //right now this should only return false if mana is too low
        if (!m_currentSpell.Interact(m_currentSpell.NextState, this))
            ManaFailed();
    }

    public void PickSpell(int GestureId)
    {
        int spellindex = SpellList.GetSpellIndex(GestureId);

        if (spellindex == -1)
        {
            SpellNotReconized();
            return;
        }

        m_currentSpell = SpellList.ReadSpell(spellindex);
    }

    protected void ManaFailed(){ throw new System.NotImplementedException(); }
    protected void SpellNotReconized() { throw new System.NotImplementedException(); }
}
