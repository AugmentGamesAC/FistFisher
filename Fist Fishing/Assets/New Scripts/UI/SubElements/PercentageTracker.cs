﻿using UnityEngine;

[System.Serializable]
public class PercentageTracker : UITracker<IPercentage>, IPercentage
{
    public PercentageTracker(float max)
    {
        displayPercentage = new Percentage();
        m_value = displayPercentage;
        m_value.SetMax(max);
    }

    [SerializeField]
    protected Percentage displayPercentage;
    public float Current => m_value.Current;

    public StatTracker Max => m_value.Max;

    public float Percent => m_value.Percent;

    public void IncrementCurrent(float increment)
    {
        m_value.IncrementCurrent(increment);
        UpdateState();
    }

    public void SetCurrent(float current)
    {
        m_value.SetCurrent(current);
        UpdateState();
    }

    public void SetMax(float max)
    {
        m_value.SetMax(max);
        UpdateState();
    }

    protected override IPercentage ImplicitOverRide(UITracker<IPercentage> reference)
    {
        return this;
    }
}

