using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spell
{ 
    /// <summary>
    /// This is used to decide which elemental effect is displayed.
    /// </summary>
    [System.Flags]
    public enum Elements
    {
        Undefined = 0,
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
        Undefined = 0,
        StartCasting,
        StopCasting,
        BeginAiming,
        BeginAiming2,
        BypassAiming,
        Cancel
    };



    [SerializeField]
    protected SpellDescription m_Description;
    public SpellDescription Description { get { return m_Description; } }

    [SerializeField]
    protected float m_ManaCost;
    public float ManaCost { get { return m_ManaCost; } }
    [SerializeField]
    protected Elements m_ElementType;
    public Elements ElementType { get { return m_ElementType; } }
    /// <summary>
    /// spelluser is updated every time aiming is begun;
    /// </summary>
    protected ASpellUser m_SpellUser;
    public ASpellUser SpellUser;
    protected float m_ManaPerSecond;
    protected SpellInstance m_SpellState;
    protected SpellInstance m_SpellStateSecond;
    [SerializeField]
    protected GameObject m_DefaultSpellInstance;

    [SerializeField]
    protected float m_MaxSpellRange = float.MaxValue;
    [SerializeField]
    protected float m_MaxDistance = 20.0f;
    [SerializeField]
    protected bool m_ShowRayTrace = true;

    [SerializeField]
    protected float m_Damage;
    public float Damage { get { return m_Damage; } }
    [SerializeField]
    protected float m_Cooldown = 2.0f;
    public float Cooldown { get { return m_Cooldown; } }

    [SerializeField]
    protected InteractionType m_NextState = InteractionType.BeginAiming;
    public InteractionType NextState { get { return m_NextState; } }


    public Spell(SpellDescription spellProperties)
    {
        m_Description = spellProperties;
        m_ElementType = ResolveElements(spellProperties);
        m_ManaCost = ResolveDescriptionCost(spellProperties);

    }

    private Elements ResolveElements(SpellDescription discription)
    {
        if (discription.Effect == SpellDescription.Effects.Damage)
            return (discription.Usage == SpellDescription.Usages.SetTime) ? Elements.Fire : Elements.Lightning;
        if (discription.Effect == SpellDescription.Effects.Summon)
            return Elements.Ice;
        return Elements.Aiming;
    }
    //TODO: oversimplified for now needs discussion
    private float ResolveDescriptionCost(SpellDescription spell)
    {
        return ((float)m_ElementType) * ((float)spell.Shape) * ((float)spell.Usage);
    }

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
                return ResolveCasting();
            case InteractionType.Cancel:
                return Cancel();
            case InteractionType.StopCasting:
                return Cancel();
            default:
                return false;
        }
    }

    private bool ResolveBeginAiming2()
    {
        GameObject spellTemplate;
        if (!SpellManager.Instance.SpellShapeLookup.TryGetValue(m_Description.Shape, out spellTemplate))
            spellTemplate = m_DefaultSpellInstance;

        m_SpellStateSecond = MonoBehaviour.Instantiate(
            spellTemplate,
            Vector3.zero,
            Quaternion.identity,
            (m_Description.Aiming.HasFlag(SpellDescription.Aimings.FromCaster)) ? throw new System.NotImplementedException() :
            (m_Description.Aiming.HasFlag(SpellDescription.Aimings.FromFinger)) ? m_SpellUser.Aiming : null
            ).GetComponent<SpellInstance>();
        m_SpellStateSecond.m_Spell = this;
        m_SpellState.UpdateState(SpellInstance.InstanceStates.IsAiming);
        m_SpellStateSecond.UpdateMaterial(Elements.Aiming);
        //Figure out next state
        m_NextState = InteractionType.StartCasting;


        if (m_Description.Aiming.HasFlag(SpellDescription.Aimings.FromFingerEndPoint))
            return true;

        ResolveAimingFormatting(m_SpellStateSecond, m_Description.Aiming);
        return true;
    }

    private bool ResolveBeginAiming(ASpellUser spellUser)
    {
        if ((spellUser == null) || (spellUser.transform == null))
            return false;
        m_SpellUser = spellUser;

        GameObject spellTemplate;
        if (!SpellManager.Instance.SpellShapeLookup.TryGetValue(m_Description.Shape, out spellTemplate))
            spellTemplate = m_DefaultSpellInstance;

        m_SpellState = MonoBehaviour.Instantiate(
            spellTemplate,
            Vector3.zero,
            Quaternion.identity,
            (m_Description.Aiming.HasFlag(SpellDescription.Aimings.FromCaster)) ? throw new System.NotImplementedException() :
            (m_Description.Aiming.HasFlag(SpellDescription.Aimings.FromFinger)) ? m_SpellUser.Aiming : null
            ).GetComponent<SpellInstance>();
        m_SpellState.m_Spell = this;
        m_SpellState.UpdateState(SpellInstance.InstanceStates.IsAiming);
        m_SpellState.UpdateMaterial(Elements.Aiming);
        m_SpellUser.PredictManaModify(m_ManaCost);


        //Figure out next state
        m_NextState = (m_Description.Effect.HasFlag(SpellDescription.Effects.Swap)) ? InteractionType.BeginAiming2 : InteractionType.StartCasting;


        if (m_Description.Aiming.HasFlag(SpellDescription.Aimings.FromFingerEndPoint))
            return true;

        ResolveAimingFormatting(m_SpellState, m_Description.Aiming);
        return true;
    }

    /// <summary>
    /// contining from checks in ResolveBeginAiming
    /// </summary>
    /// <param name="aiming"></param>
    void ResolveAimingFormatting(SpellInstance targetSpellState, SpellDescription.Aimings aiming)
    {
        if ((aiming & SpellDescription.Aimings.HasRotation) > 0)
            ApplyRotation(targetSpellState, aiming);

        //TODO: Resolve question about summon placementvs rotation control
        targetSpellState.transform.LookAt(targetSpellState.transform.position + m_SpellUser.Aiming.forward, Vector3.up);
        targetSpellState.transform.localPosition = CalculateTransform(targetSpellState, aiming);
    }

    void ApplyRotation(SpellInstance targetSpellState, SpellDescription.Aimings aiming)
    {
        if (targetSpellState == null)
            return;
        //sets the world transform to the correct angle
        targetSpellState.transform.Rotate(
            aiming.HasFlag(SpellDescription.Aimings.ReverseX) ? 180 : 0 + (aiming.HasFlag(SpellDescription.Aimings.NinetyX) ? 90 : 0),
            aiming.HasFlag(SpellDescription.Aimings.ReverseY) ? 180 : 0 + (aiming.HasFlag(SpellDescription.Aimings.NinetyY) ? 90 : 0),
            aiming.HasFlag(SpellDescription.Aimings.ReverseZ) ? 180 : 0 + (aiming.HasFlag(SpellDescription.Aimings.NinetyZ) ? 90 : 0));
    }

    private Vector3 CalculateTransform(SpellInstance targetSpellState, SpellDescription.Aimings aiming)
    {
        if (targetSpellState == null)
            return Vector3.zero;

        Vector3 dimentions = targetSpellState.GetComponentInChildren<Renderer>().bounds.size;
        return new Vector3
        (
            CalculateOffset(dimentions.x,
                aiming.HasFlag(SpellDescription.Aimings.HalfOffsetX), aiming.HasFlag(SpellDescription.Aimings.HalfOffsetNeg),
                aiming.HasFlag(SpellDescription.Aimings.FullOffsetX), aiming.HasFlag(SpellDescription.Aimings.FullOffsetNeg)),
            CalculateOffset(dimentions.y,
                aiming.HasFlag(SpellDescription.Aimings.HalfOffsetY), aiming.HasFlag(SpellDescription.Aimings.HalfOffsetNeg),
                aiming.HasFlag(SpellDescription.Aimings.FullOffsetY), aiming.HasFlag(SpellDescription.Aimings.FullOffsetNeg)),
            CalculateOffset(dimentions.z,
                aiming.HasFlag(SpellDescription.Aimings.HalfOffsetZ), aiming.HasFlag(SpellDescription.Aimings.HalfOffsetNeg),
                aiming.HasFlag(SpellDescription.Aimings.FullOffsetZ), aiming.HasFlag(SpellDescription.Aimings.FullOffsetNeg))
        );
    }
    //Applies offsetcalculation
    float CalculateOffset(float positionDimention,
                                bool incrementHalfDimention, bool isNegativeHalfDimention,
                                bool incrementDimention, bool isNegativeDimention)
    {
        return ((isNegativeHalfDimention) ? -1 : 1 * ((incrementHalfDimention) ? positionDimention / 2 : 0)) +
            ((isNegativeDimention) ? -1 : 1 * ((incrementDimention) ? positionDimention : 0));
    }

    //TODO: code requires a mana check before casting and then a using of the mana
    private bool ResolveCasting()
    {
        if ((m_SpellUser == null)|| (m_SpellState == null))
            return false;

        if (!HadTheMana())
            return false;

        SpellManager.CastSpell spellToCast = default(SpellManager.CastSpell);

        if (!SpellManager.Instance.SpellResolutionLookup.TryGetValue(SpellDescription.TranslateSpellCode(Description), out spellToCast))
            spellToCast = SpellManager.CastStandardSpell;

        m_NextState = InteractionType.BeginAiming;

        bool returncatch = spellToCast(m_SpellState, m_SpellStateSecond);
        if (!returncatch)
            return false;
        m_SpellState = null;
        m_SpellStateSecond = null;
        return true;
    }


    bool HadTheMana()
    {
        if (m_SpellUser.ManaCurrent < m_ManaCost)
            return false;
        m_SpellUser.ModifyMana(m_ManaCost);
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

        if (m_SpellUser == null)
            return true;

        m_SpellUser.PredictManaModify(0);

        return true;
    }

    public string[] m_LayerExclusionMask = { "Ignore Raycast" };

    public bool ResolveFromFingerEndPoint(SpellInstance instance)
    {
        if ((m_SpellUser == null) || !Description.Aiming.HasFlag(SpellDescription.Aimings.FromFingerEndPoint))
            return false;

       // int mask = ~LayerMask.GetMask(m_LayerExclusionMask);


        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (!Physics.Raycast(m_SpellUser.Aiming.position,m_SpellUser.Aiming.forward, out hit, m_MaxSpellRange))
        {
            Vector3 pos = m_SpellUser.Aiming.position + m_SpellUser.Aiming.forward.normalized * m_MaxDistance;
            instance.transform.SetPositionAndRotation(pos, Quaternion.Euler(Vector3.zero));
            return false;
        }
        if (m_ShowRayTrace)
            Debug.DrawRay(m_SpellUser.Aiming.position, m_SpellUser.Aiming.forward, Color.yellow);

        instance.transform.SetPositionAndRotation(hit.point, Quaternion.FromToRotation(instance.transform.up, hit.normal) * instance.transform.rotation);

        instance.transform.position += instance.transform.TransformDirection(CalculateTransform(instance, m_Description.Aiming));
        ApplyRotation(instance, m_Description.Aiming);
        return true;
    }
}
