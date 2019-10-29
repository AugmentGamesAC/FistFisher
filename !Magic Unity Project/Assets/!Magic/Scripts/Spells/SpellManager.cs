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
        {SpellDescription.TranslateSpellCode(0,SpellDescription.Effects.Swap,SpellDescription.Usages.Instant,SpellDescription.Aimings.FromFingerEndPointPlusHalfExtent) , CastDoubleSpell }
    };
    public SpellResolutionLookups SpellResolutionLookup { get { return m_SpellResolutionLookup; } }




    static public bool CastDoubleSpell(SpellInstance firstInstance, SpellInstance secondInstance)
    {
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


        SpellDescription spellDescription = firstInstance.m_Spell.Description;

        if (!spellDescription.Aiming.HasFlag(SpellDescription.Aimings.CenteredBoxToFingerTip))
            firstInstance.gameObject.transform.parent.SetParent(null);
        if (spellDescription.Effect == SpellDescription.Effects.Summon)
            firstInstance.gameObject.transform.parent.gameObject.layer = 0;

        firstInstance.UpdateState(SpellInstance.InstanceStates.IsActive);

        return true;
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
