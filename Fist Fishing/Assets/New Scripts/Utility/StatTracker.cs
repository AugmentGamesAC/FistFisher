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

    public delegate void CurrentAmountChanged();
    public CurrentAmountChanged OnCurrentAmountChanged;

    /// <summary>
    /// Can consider StatTracker as a float with this.
    /// returns ref to currentAmount.
    /// </summary>
    /// <param name="reference"></param>
    public static implicit operator float(StatTracker reference)
    {
        return reference.CurrentAmount;
    }

    /// <summary>
    /// Adds changeAmount to current amount.
    /// Invokes OnChanged delegates.
    /// </summary>
    /// <param name="changeAmount"></param>
    public void Change(float changeAmount)
    {
        m_currentAmount += changeAmount;

        OnCurrentAmountChanged?.Invoke();
    }
}
