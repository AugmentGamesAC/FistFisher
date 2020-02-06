using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHealth
{

    [SerializeField]
    protected FloatTracker m_currentAmount = new FloatTracker();
    public FloatTracker CurrentAmount { get { return m_currentAmount; } }

    [SerializeField]
    protected float m_max = 100.0f;
    public float Max { get { return m_max; } }

    [SerializeField]
    protected float m_min = 0.0f;
    public float Min { get { return m_min; } }

    public float Percentage { get { return m_currentAmount / m_max; } }

    public FishHealth()
    {
        ResetCurrentAmount();
    }

    /// <summary>
    /// Can consider StatTracker as a float with this.
    /// returns ref to currentAmount.
    /// </summary>
    /// <param name="reference"></param>
    public static implicit operator float(FishHealth reference)
    {
        return reference.CurrentAmount;
    }

    public void Change(float changeAmount)
    {
        m_currentAmount.SetValue( Mathf.Clamp(m_currentAmount + changeAmount, m_min, m_max));
    }

    public void ResetCurrentAmount()
    {
        Change(m_max);
    }
}
