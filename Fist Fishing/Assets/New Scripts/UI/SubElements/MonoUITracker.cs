using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonoUITracker<T> : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField]
    protected T m_value;

    public delegate void UIUpdateListner(T type);
    public UIUpdateListner OnStateChange;

    protected void UpdateState()
    {
        OnStateChange?.Invoke(m_value);
    }

    public void OnBeforeSerialize()
    {
        UpdateState();
    }

    public void OnAfterDeserialize()
    {
    }
}

public class UITracker<T> : ISerializationCallbackReceiver
{
    [SerializeField]
    protected T m_value;

    public delegate void UIUpdateListner(T type);
    public UIUpdateListner OnStateChange;

    protected void UpdateState()
    {
        OnStateChange?.Invoke(m_value);
    }

    public void OnBeforeSerialize()
    {
        UpdateState();
    }

    public void OnAfterDeserialize()
    {
    }
}