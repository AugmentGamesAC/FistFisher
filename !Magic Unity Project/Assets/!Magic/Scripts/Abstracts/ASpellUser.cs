using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ASpellUser : MonoBehaviour
{
    protected float m_HealthCurrent;
    public float HealthCurrent { get { return m_HealthCurrent; } }
    protected float m_HealthPredicted;
    public float HealthPredicted { get { return m_HealthPredicted; } }
    protected float m_HealthMax;
    public float HealthMax { get { return m_HealthMax; } }
    protected float m_HealthRegen;
    public float HealthRegen { get { return m_HealthRegen; } }

    protected float m_ManaCurrent;
    public float ManaCurrent { get { return m_ManaCurrent; } }
    protected float m_ManaPredicted;
    public float ManaPredicted { get { return m_ManaPredicted; } }
    protected float m_ManaMax;
    public float ManaMax { get { return m_ManaMax; } }
    protected float m_ManaRegen;
    public float ManaRegen { get { return m_ManaRegen; } }

    public abstract Transform Aiming { get; }
}

