using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class FloatTracker : UITracker<float>
{
    private void Start()
    {
        if (m_value == null)
        {
            m_value = 0;
        }
    }
}
