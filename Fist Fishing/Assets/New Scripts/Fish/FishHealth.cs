using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FishHealth
{
    public FishHealth(float max)
    {
        m_percTracker = new PercentageTracker(new StatTracker(max));
        ResetCurrentAmount();
    }

    public float Max => m_percTracker.Max;
    public float CurrentAmount => m_percTracker.Current;

    protected PercentageTracker m_percTracker;
    public PercentageTracker PercentTracker => m_percTracker;


    public event Death OnMinimumHealthReached;

    /// <summary>
    /// Can consider FishHealth as a float with this.
    /// returns ref to currentAmount.
    /// </summary>
    /// <param name="reference"></param>
    public static implicit operator float(FishHealth reference)
    {
        return reference.m_percTracker.Current;
    }

    public void Change(float changeAmount)
    {
        m_percTracker.IncrementCurrent(changeAmount);

        if (CurrentAmount <= 0)
            OnMinimumHealthReached?.Invoke();
    }

    public void ResetCurrentAmount()
    {
        Change(m_percTracker.Max);
    }
}
