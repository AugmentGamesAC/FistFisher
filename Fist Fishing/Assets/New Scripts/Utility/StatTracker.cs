using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatTracker
{
    public static implicit operator float(StatTracker reference)
    {
        return reference.m_maxValue;
    }

    public StatTracker(float max = 3.0f)
    {
        m_maxValue = max;
    }

    [SerializeField]
    protected virtual float m_maxValue { get; set; }
    public virtual float MaxValue => m_maxValue;

    public virtual void Change(float changeAmount)
    {
        m_maxValue += changeAmount;
    }

    public virtual void SetValue(float max)
    {
        m_maxValue = max;
    }
}
