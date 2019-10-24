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



    public void ResolveInteraction()
    {
        
    }

    protected void ManaFailed(){ throw new System.NotImplementedException(); }
    protected void SpellNotReconized() { throw new System.NotImplementedException(); }
}
