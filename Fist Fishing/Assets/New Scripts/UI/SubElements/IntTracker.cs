using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class IntTracker : UITracker<int>
{
    public T ImplicitOverRide<T>(IntTracker reference) where T : System.Enum
    {
        return (T)System.Enum.ToObject(typeof(T), reference.m_value);
    }
}

