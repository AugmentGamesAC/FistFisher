using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatTracker
{
    [SerializeField]
    protected float m_maxValue;

    public delegate void ChangeDel();
    public event ChangeDel OnChange;

    public static implicit operator float(StatTracker reference)
    {
        return reference.m_maxValue;
    }

    public StatTracker(float max = 100.0f)
    {
        m_maxValue = max;
        OnChange?.Invoke();
    }

    public virtual float MaxValue => m_maxValue;

    public virtual void Change(float changeAmount)
    {
        m_maxValue += changeAmount;
        OnChange?.Invoke();
    }

    public virtual void SetValue(float max)
    {
        m_maxValue = max;
        OnChange?.Invoke();
    }
}
