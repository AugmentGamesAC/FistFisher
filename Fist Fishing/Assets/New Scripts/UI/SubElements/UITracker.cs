﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// generic tracker for all UI updating types
/// </summary>
/// <typeparam name="T"></typeparam>
[System.Serializable]
public class UITracker<T> : ISerializationCallbackReceiver
{
    [SerializeField]
    protected T m_value;
    [SerializeField]
    protected bool SerializeInvokes;

    public delegate void UIUpdateListner(T type);
    public UIUpdateListner OnStateChange;

    protected void UpdateState()
    {
        if (m_value == null)
            return;
        OnStateChange?.Invoke(m_value);
    }

    public void OnBeforeSerialize()
    {
        if (!SerializeInvokes)
            return;
        UpdateState();
    }

    public void OnAfterDeserialize()
    {
    }

    protected virtual T ImplicitOverRide(UITracker<T> reference)
    {
        return reference.m_value;
    }

    /// <summary>
    /// Can consider StatTracker as a float with this.
    /// returns ref to currentAmount.
    /// </summary>
    /// <param name="reference"></param>
    public static implicit operator T(UITracker<T> reference)
    {
        return reference.ImplicitOverRide(reference);
    }

    public void SetValue(T newValue)
    {
        m_value = newValue;
        UpdateState();
    }
}