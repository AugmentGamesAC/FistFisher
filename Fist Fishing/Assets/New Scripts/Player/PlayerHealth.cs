using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth
{
    protected PercentageTracker m_percent;
    public IPercentage Percentage => m_percent;

    public delegate void MinimumAmountReached();
    public event MinimumAmountReached OnMinimumAmountReached;

    public PlayerStatManager m_PlayerStatMan;

    /// <summary>
    /// Can consider StatTracker as a float with this.
    /// returns ref to currentAmount.
    /// </summary>
    /// <param name="reference"></param>
    public static implicit operator float(PlayerHealth reference)
    {
        return reference.m_percent.Current;
    }

    public PlayerHealth(float max)
    {
        m_percent = new PercentageTracker(max);
        ResetCurrentAmount();
    }

    public void Change(float changeAmount)
    {
        m_percent.IncrementCurrent(changeAmount);

        if (this == 0 && OnMinimumAmountReached != null)
            OnMinimumAmountReached.Invoke();
    }

    public void ResetCurrentAmount()
    {
        Change(m_percent.Max);
    }
}


