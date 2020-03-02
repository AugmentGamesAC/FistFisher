using System;
using UnityEngine;
[Serializable]
public class Percentage : IPercentage
{
    [SerializeField]
    protected StatTracker m_max = new StatTracker();
    public StatTracker Max => m_max;
    [SerializeField]
    protected float m_current;
    public float Current => m_current;

    protected float m_percentage;
    public float Percent => m_percentage;
    protected void updatePercentage()
    {
        m_percentage = Current / Max;
    }

    public void SetCurrent(float current)
    {
        m_current = Mathf.Clamp(current, 0, m_max);
        updatePercentage();
    }

    public void IncrementCurrent(float increment)
    {
        SetCurrent(m_current + increment);
    }

    public void SetMax(float max)
    {
        m_max.SetValue( max);
        updatePercentage();
    }
}
