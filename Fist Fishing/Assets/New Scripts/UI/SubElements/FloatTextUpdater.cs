using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// takes a tracker with float values, 
/// turns value into string when updating a text UI element
/// </summary>
[System.Serializable]
public class FloatTextUpdater : CoreUIUpdater<FloatTracker,Text,float>
{
    [SerializeField]
    protected string m_textInput = "{0}";

    public void SetFormatter(string format) { m_textInput = format; }
    protected override void UpdateState(float value)
    {
        m_UIElement.text = string.Format(m_textInput, value);
    }
}
