using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PremadeSpell : MonoBehaviour
{
    public SpellDescription.Shape shape;
    public SpellDescription.Usage usage;
    public SpellDescription.Effect effect;
    public SpellDescription.Aiming aiming;
    public GauntletAiming m_AimingModule;
    public SpellManager m_spellManager;
    public int m_SpellID;
    public Spell m_Spell;
    public PlayerData m_SpellOwner;

    // Start is called before the first frame update
    void Start()
    {
        //shape = SpellDescription.Shape.Cube;
        //usage = SpellDescription.Usage.Instant;
        //effect = SpellDescription.Effect.Summon;
        //aiming = SpellDescription.Aiming.FromFinger;
        GetComponent<SlottableSpellPrism>().m_spellID = m_SpellID;
        m_Spell = new Spell(new SpellDescription(shape, effect, usage, aiming), null);
        m_Spell.m_AimPoint = m_AimingModule;
        m_Spell.m_spellOwner = m_SpellOwner;
        m_Spell.m_SpellManager = m_spellManager;
        GetComponent<SlottableSpellPrism>().m_spell = m_Spell;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
