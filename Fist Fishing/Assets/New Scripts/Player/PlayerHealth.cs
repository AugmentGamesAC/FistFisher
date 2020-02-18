using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerHealth
{
    [SerializeField]
    protected PercentageTracker m_percent;
    public IPercentage Percentage => m_percent;
    public PercentageTracker Tracker => m_percent;

    public delegate void MinimumAmountReached();
    public event MinimumAmountReached OnMinimumAmountReached;

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

        //PlayerInstance.Instance.PlayerStatMan[Stats.MaxHealth].OnCurrentAmountChanged += SetMax;
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

    protected void SetMax(float max)
    {
        m_percent.SetMax(max);
    }
}


