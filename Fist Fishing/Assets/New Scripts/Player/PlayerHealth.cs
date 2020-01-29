﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
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

    public PlayerStatManager m_PlayerStatMan;

    private void Start()
    {
        ResetCurrentAmount();
        m_PlayerStatMan[Stats.MaxHealth].OnCurrentAmountChanged += MaxIncrease;
    }

    public void MaxIncrease()
    {
        m_max = m_PlayerStatMan[Stats.MaxHealth];
        ResetCurrentAmount();
    }

    /// <summary>
    /// Can consider StatTracker as a float with this.
    /// returns ref to currentAmount.
    /// </summary>
    /// <param name="reference"></param>
    public static implicit operator float(PlayerHealth reference)
    {
        return reference.CurrentAmount;
    }

    public void Change(float changeAmount)
    {
        m_currentAmount = Mathf.Clamp(m_currentAmount + changeAmount, m_min, m_max);

        if (OnCurrentAmountChanged != null)
            OnCurrentAmountChanged.Invoke();

        if (CurrentAmount == m_min && OnMinimumAmountReached != null)
            OnMinimumAmountReached.Invoke();
    }

    public void ResetCurrentAmount()
    {
        Change(m_max);
    }
}


