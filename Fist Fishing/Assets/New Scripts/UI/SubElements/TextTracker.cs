using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TextTracker : MonoUITracker<string>
{
    private void Start()
    {
        if (m_value == null)
        {
            m_value = " ";
        }
    }
}
