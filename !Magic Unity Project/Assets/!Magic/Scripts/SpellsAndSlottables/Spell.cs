using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : ICastingInterface
{
    /// <summary>
    /// This is used to decide which elemental effect is displayed.
    /// </summary>
    [Flags]
    public enum Elements
    {
        Fire = 0x0001, //Damage Over Time  
        Ice = 0x0002, //Takes up space 
        Lightning = 0x0004, //Instant Damage
        Aiming = 0x0008, //used for aiming
    };
    public SpellDescription _Start;
    public SpellDescription m_Start { get { return _Start; } protected set { _Start = value; } }
    public SpellDescription _Finish; 
    public SpellDescription m_Finish { get { return _Finish; } protected set { _Finish = value; } }
    public float m_ManaCost { get; protected set; }
    public Elements m_elementType { get; protected set; }
    public IAimingDirection m_AimPoint { get; set; }

    public SpellManager m_SpellManager;

    private float _m_manaCostPerSecond = 0;
    public float m_manaCostPerSecond
    {
        get
        {
            if (_m_manaCostPerSecond == 0)
            {
                _m_manaCostPerSecond = ResolveDescriptionCost(m_Start);
                if (m_Finish != null)
                    _m_manaCostPerSecond += ResolveDescriptionCost(m_Start);
            }
            return _m_manaCostPerSecond;
        }
    }

    //TODO: oversimplified for now needs discussion
    private float ResolveDescriptionCost(SpellDescription spell)
    {
        return ((float)DetermineElement(spell)) * ((float)spell.m_shape) * ((float)spell.m_usage);
    }

    public Spell(SpellDescription start, SpellDescription finish)
    {
        m_Start = start;
        m_Finish = finish;

        m_elementType = DetermineElement(start);
        if (finish != null)
            m_elementType |= DetermineElement(finish);
    }

    private Elements DetermineElement(SpellDescription discription)
    {
        if (discription.m_effect == SpellDescription.Effect.Damage)
            return (SpellDescription.Usage.Duration == discription.m_usage) ? Elements.Fire : Elements.Lightning;
        if (discription.m_effect == SpellDescription.Effect.Summon)
            return Elements.Ice;
        return Elements.Aiming;
    }

    public bool m_isAiming { get; set; }

    public GameObject m_targeting;
    private bool m_targetingNeedsRotation = true;
    //TODO: resolve where to find Aiming Infor
    public void BeginAiming()
    {
        if (m_AimPoint == null)
            return;
        if (!m_SpellManager.m_AimingShapes.ContainsKey(m_Start.m_shape))
            return;

        m_isAiming = true;

        if (m_Start.m_aiming.HasFlag(SpellDescription.Aiming.Hidden))
            return;

        m_targeting = MonoBehaviour.Instantiate(
            m_SpellManager.m_AimingShapes[m_Start.m_shape],
            Vector3.zero,
            Quaternion.identity,
            m_AimPoint.LatchForSpells);
        m_targeting.GetComponent<SpellInstance>().m_SpellManager = m_SpellManager;
        m_targeting.GetComponent<SpellInstance>().m_ManaPerTick = _m_manaCostPerSecond;
        m_targeting.GetComponent<SpellInstance>().m_Spell = this;
        m_targeting.GetComponent<SpellInstance>().m_IsAiming = true;

        if (m_Start.m_aiming.HasFlag(SpellDescription.Aiming.FromFingerEndPoint))
            return;

        ResolveAimingFormatting(m_Start.m_aiming);


    }
    public bool m_showRayTrace = false;
    public void ResolveAimingTick()
    {
        if (m_Start.m_aiming.HasFlag(SpellDescription.Aiming.Hidden))
            return;
        if (m_targeting == null)
            return;

        if (m_Start.m_aiming.HasFlag(SpellDescription.Aiming.FromFingerEndPoint))
            ResolveAimingFromFingerEnd();
    }

    void ResolveAimingFormatting(SpellDescription.Aiming aiming)
    {
        if (m_targeting == null)
            return;
        if (m_AimPoint == null)
            return;

        if ((aiming & SpellDescription.Aiming.HasRotation) > 0)
            ApplyRotation(aiming);
        
        m_targeting.transform.LookAt(m_targeting.transform.position + m_AimPoint.Direction, Vector3.up);
        m_targeting.transform.localPosition = ApplyTransfrom(aiming);
        ApplyRotation(m_Start.m_aiming);
    }
    void ApplyRotation(SpellDescription.Aiming aiming)
    {
        if ((m_targeting == null) || (!m_targetingNeedsRotation))
            return;
        //sets the world transform to the correct angle
        m_targeting.transform.Rotate(
            aiming.HasFlag(SpellDescription.Aiming.ReverseX) ? 180 : 0,
            aiming.HasFlag(SpellDescription.Aiming.ReverseY) ? 180 : 0,
            aiming.HasFlag(SpellDescription.Aiming.ReverseZ) ? 180 : 0);
         
    }
    Vector3 ApplyTransfrom(SpellDescription.Aiming aiming)
    {
        if (m_targeting == null)
            return Vector3.zero;

        Vector3 dimentions = m_targeting.GetComponent<Renderer>().bounds.size;
        return new Vector3(
                CalculateOffset(dimentions.x,
                    aiming.HasFlag(SpellDescription.Aiming.HalfOffsetX), aiming.HasFlag(SpellDescription.Aiming.HalfOffsetNeg),
                    aiming.HasFlag(SpellDescription.Aiming.FullOffsetX), aiming.HasFlag(SpellDescription.Aiming.FullOffsetNeg)),
                CalculateOffset(dimentions.y,
                    aiming.HasFlag(SpellDescription.Aiming.HalfOffsetY), aiming.HasFlag(SpellDescription.Aiming.HalfOffsetNeg),
                    aiming.HasFlag(SpellDescription.Aiming.FullOffsetY), aiming.HasFlag(SpellDescription.Aiming.FullOffsetNeg)),
                CalculateOffset(dimentions.z,
                    aiming.HasFlag(SpellDescription.Aiming.HalfOffsetZ), aiming.HasFlag(SpellDescription.Aiming.HalfOffsetNeg),
                    aiming.HasFlag(SpellDescription.Aiming.FullOffsetZ), aiming.HasFlag(SpellDescription.Aiming.FullOffsetNeg))
            );
    }
    //Applies offsetcalculation
    float CalculateOffset(float positionDimention, 
                                bool incrementHalfDimention, bool isNegativeHalfDimention, 
                                bool incrementDimention, bool isNegativeDimention )
    {
        return ((isNegativeHalfDimention) ? -1 : 1 * ((incrementHalfDimention) ? positionDimention / 2 : 0)) +
            ((isNegativeDimention) ? -1 : 1 * ((incrementDimention) ? positionDimention : 0));
    }



    void ResolveAimingFromFingerEnd()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (!Physics.Raycast(m_AimPoint.StartingPoint, m_AimPoint.Direction, out hit, Mathf.Infinity))
            return;
        if (m_showRayTrace)
            Debug.DrawRay(m_AimPoint.StartingPoint, m_AimPoint.Direction * hit.distance, Color.yellow);

        m_targeting.transform.SetPositionAndRotation(hit.point, Quaternion.FromToRotation(m_targeting.transform.up, hit.normal) * m_targeting.transform.rotation);
        ApplyRotation(m_Start.m_aiming);
    }


    //don't have a manual trigger yet
    private void CancelCasting()
    {
        throw new NotImplementedException();
    }
    private void CancelAiming()
    {
        if (!m_isAiming)
            return;
        m_isAiming = false;
        GameObject.Destroy(m_targeting);
        m_targeting = null;
        m_spellOwner.UseMana(-m_manaCostPerSecond);
    }


    public void Cancel()
    {
        if (m_isAiming)
            CancelAiming();
        if (m_isCasting)
            CancelCasting();
    }


    private bool m_isCasting = false;
    public IMagicUser m_spellOwner {get;set;}
    /// <summary>
    /// is used to resolve when a spell is cast over time
    /// </summary>
    public void FixedUpdate()
    {
        if (m_isAiming)
            ResolveAimingTick();

        if ((!m_isCasting)||(m_spellOwner == null))
            return;

        m_spellOwner.UseMana(m_manaCostPerSecond * Time.deltaTime);
        if (m_spellOwner.m_mana <= 0)
            m_isCasting = false;
    }

    public void Interact(InteractionType interaction)
    {
        if ((interaction.HasFlag(InteractionType.StartCasting)) && (m_isAiming))
        {
            m_spellOwner.UseMana(m_manaCostPerSecond);
            m_targeting.GetComponent<SpellInstance>().m_IsAiming = false;
            m_isAiming = false;

            
        }
        else if ((interaction.HasFlag(InteractionType.StopCasting)) && (m_isAiming))
            CancelCasting();
    }
}
