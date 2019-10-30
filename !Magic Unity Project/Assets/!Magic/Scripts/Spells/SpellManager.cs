using UnityEngine;

/// <summary>
/// Singleton Variatent
/// </summary>
[System.Serializable]
public class SpellManager : MonoBehaviour
{
    [System.Serializable]
    public class SpellShapeLookups : InspectorDictionary<SpellDescription.Shapes, GameObject> { }
    [SerializeField]
    protected SpellShapeLookups m_SpellShapeLookup = new SpellShapeLookups();
    public SpellShapeLookups SpellShapeLookup { get { return m_SpellShapeLookup; } }

    [System.Serializable]
    public class SpellInstaceMatLookups : InspectorDictionary<Spell.Elements, Material> { }
    [SerializeField]
    protected SpellInstaceMatLookups m_SpellInstaceMatLookup = new SpellInstaceMatLookups();
    public SpellInstaceMatLookups SpellInstaceMatLookup { get { return m_SpellInstaceMatLookup; } }

    //TODO: move final verison into SpellManager
    [System.Serializable]
    public class ElementColorLookups : InspectorDictionary<Spell.Elements, Color> { }
    [SerializeField]
    protected ElementColorLookups m_ElementColorLookup = new ElementColorLookups()
    {
        { Spell.Elements.Fire, new Color(1f, 0.75f, 0f)     },
        { Spell.Elements.Ice, new Color(0.4f, 0.4f, 1.0f)  },
        { Spell.Elements.Lightning, new Color(1f, 1f, 0.2f)     }
        };
    public ElementColorLookups ElementColorLookup { get { return m_ElementColorLookup; } }


    [System.Serializable]
    public class ShapeColorLookups : InspectorDictionary<SpellDescription.Shapes, Color> { }
    [SerializeField]
    protected ShapeColorLookups m_ShapeColorLookup = new ShapeColorLookups()
    {
        { SpellDescription.Shapes.Cone, new Color(1f, 0.1f, 1f)       },
        { SpellDescription.Shapes.Cube, new Color(1f, 0.2f, 0.2f)     },
        { SpellDescription.Shapes.Sphere, new Color(0.2f, 1f, 0.2f)   }
    };
    public ShapeColorLookups ShapeColorLookup { get { return m_ShapeColorLookup; } }

    [SerializeField]
    private static SpellManager m_SpellBehaviour;


    public delegate bool CastSpell(SpellInstance firstInstance, SpellInstance secondInstance);

    [System.Serializable]
    public class SpellResolutionLookups : InspectorDictionary<double, CastSpell> { }
    [SerializeField]
    protected SpellResolutionLookups m_SpellResolutionLookup = new SpellResolutionLookups()
    {
        //Needs one for each Spell description combinations. 16 for now.
        {SpellDescription.TranslateSpellCode(0,SpellDescription.Effects.Swap,SpellDescription.Usages.Instant,SpellDescription.Aimings.FromFingerEndPointPlusHalfExtent) , CastDoubleSpell }

    };
    public SpellResolutionLookups SpellResolutionLookup { get { return m_SpellResolutionLookup; } }

    static public bool CastDoubleSpell(SpellInstance firstInstance, SpellInstance secondInstance)
    {
        throw new System.NotImplementedException();


        SpellDescription spellDescription = firstInstance.m_Spell.Description;

        if (!spellDescription.Aiming.HasFlag(SpellDescription.Aimings.CenteredBoxToFingerTip))
            firstInstance.gameObject.transform.parent.SetParent(null);
        if (spellDescription.Effect == SpellDescription.Effects.Summon)
            firstInstance.gameObject.transform.parent.gameObject.layer = 0;

        firstInstance.UpdateState(SpellInstance.InstanceStates.IsActive);

        spellDescription = secondInstance.m_Spell.Description;
        if (!spellDescription.Aiming.HasFlag(SpellDescription.Aimings.CenteredBoxToFingerTip))
            secondInstance.gameObject.transform.parent.SetParent(null);
        if (spellDescription.Effect == SpellDescription.Effects.Summon)
            secondInstance.gameObject.transform.parent.gameObject.layer = 0;

        firstInstance.UpdateState(SpellInstance.InstanceStates.IsActive);

        return true;
    }

    static public bool CastStandardSpell(SpellInstance firstInstance, SpellInstance secondInstance)
    {
        throw new System.NotImplementedException();

        SpellDescription spellDescription = firstInstance.m_Spell.Description;

        if (!spellDescription.Aiming.HasFlag(SpellDescription.Aimings.CenteredBoxToFingerTip))
            firstInstance.gameObject.transform.parent.SetParent(null);
        if (spellDescription.Effect == SpellDescription.Effects.Summon)
            firstInstance.gameObject.transform.parent.gameObject.layer = 0;

        firstInstance.UpdateState(SpellInstance.InstanceStates.IsActive);

        return true;
    }

    //selects one area and teleports. like swap but with the player.
    static public bool CastTeleportSpell(SpellInstance firstInstance, SpellInstance secondInstance)
    {
        throw new System.NotImplementedException();
    }

    //fires a growing cylinder projectile. restricts movement?
    static public bool CastBeamSpell(SpellInstance firstInstance, SpellInstance secondInstance)
    {
        throw new System.NotImplementedException();
    }

    //summons a wall
    static public bool CastWallSpell(SpellInstance firstInstance, SpellInstance secondInstance)
    {
        throw new System.NotImplementedException();
    }

    //swaps objects after two area selections.
    static public bool CastObjectSwapSpell(SpellInstance firstInstance, SpellInstance secondInstance)
    {
        throw new System.NotImplementedException();
    }

    //fires projectile from finger point.
    static public bool CastFingerGunSpell(SpellInstance firstInstance, SpellInstance secondInstance)
    {
        throw new System.NotImplementedException();
    }

    //summon damaging pillar from the ground. Similar to Wall.
    static public bool CastPillarStrikeSpell(SpellInstance firstInstance, SpellInstance secondInstance)
    {
        throw new System.NotImplementedException();
    }

    //same physics as grenade but less gravity effect on rigid body. Sticks into objects on contact.
    static public bool CastHeldLanceSpell(SpellInstance firstInstance, SpellInstance secondInstance)
    {
        throw new System.NotImplementedException();
    }

    //held object to be thrown and explodes on contact.
    static public bool CastGrenadeSpell(SpellInstance firstInstance, SpellInstance secondInstance)
    {
        throw new System.NotImplementedException();
    }

    //same as grenade but drains more mana and deals more damage the longer you hold it.
    static public bool CastChargingBombSpell(SpellInstance firstInstance, SpellInstance secondInstance)
    {
        throw new System.NotImplementedException();
    }

    //Flamethrower type summon, deals damage over time inside of its mesh collider.
    static public bool CastSprayerSpell(SpellInstance firstInstance, SpellInstance secondInstance)
    {
        throw new System.NotImplementedException();
    }

    //select an from finger point, then gain control of the object's transform.
    static public bool CastTelekenesisSpell(SpellInstance firstInstance, SpellInstance secondInstance)
    {
        throw new System.NotImplementedException();
    }

    //applies force to all objects in front of the player.
    static public bool CastForcePushSpell(SpellInstance firstInstance, SpellInstance secondInstance)
    {
        throw new System.NotImplementedException();
    }

    //pick a zone where damage is delt overtime.
    static public bool CastHailSpell(SpellInstance firstInstance, SpellInstance secondInstance)
    {
        throw new System.NotImplementedException();
    }

    //saw type blade projectile, bounces off walls? remote control?
    static public bool CastDestructorDiscSpell(SpellInstance firstInstance, SpellInstance secondInstance)
    {
        throw new System.NotImplementedException();
    }




    private void Awake()
    {
        if (m_SpellBehaviour == null)
            m_SpellBehaviour = this;
    }

    public static SpellManager Instance
    {
        get
        {
            if (m_SpellBehaviour == null)
                m_SpellBehaviour = GameObject.FindObjectOfType<SpellManager>();
            return m_SpellBehaviour;
        }
    }




}
