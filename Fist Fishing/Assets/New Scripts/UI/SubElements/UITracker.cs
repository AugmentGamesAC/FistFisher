using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// this contains a delegate that tracks the type of data used in UI updaters. 
/// When values need to change, SetValue is called
/// All data used in UI elements are a child of this class
/// </summary>
/// <typeparam name="T"></typeparam>
[System.Serializable]
public class UITracker<T> : ISerializationCallbackReceiver
{
    [SerializeField]
    protected T m_value;
    [SerializeField]
    protected bool SerializeInvokes;

    public delegate void UIUpdateListener(T type);
    public UIUpdateListener OnStateChange;

    protected void UpdateState()
    {

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