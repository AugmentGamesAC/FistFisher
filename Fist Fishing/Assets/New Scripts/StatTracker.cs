using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTracker 
{
    //    Responsibilities
    //- keeps track of an amount
    //- Invoke delegates when values reach certain amounts
    //- helper functions

    [SerializeField]
    protected float m_currentAmount;
    public float CurrentAmount { get { return m_currentAmount; } }

    [SerializeField]
    protected float m_max = 100.0f;
    public float Max { get { return m_max; } }

    [SerializeField]
    protected float m_min = 0.0f;
    public float Min { get { return m_min; } }

    public float Percentage { get { return m_currentAmount / m_max; } }

    public delegate void CurrentAmountChanged();
    public event CurrentAmountChanged OnCurrentAmountChanged;

    public delegate void MinimumAmountReached();
    public event MinimumAmountReached OnMinimumAmountReached;

    /// <summary>
    /// Can consider StatTracker as a float with this.
    /// returns ref to currentAmount.
    /// </summary>
    /// <param name="reference"></param>
    public static implicit operator float(StatTracker reference)
    {
        return reference.CurrentAmount;
    }

    public void Change(float changeAmount)
    {
        m_currentAmount += Mathf.Clamp(changeAmount, -m_max, m_max);
        m_currentAmount = Mathf.Clamp(m_currentAmount, -1.0f, m_max);
    }
}
