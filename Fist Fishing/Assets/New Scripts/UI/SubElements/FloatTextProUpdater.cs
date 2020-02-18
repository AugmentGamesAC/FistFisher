using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class FloatTextProUpdater : CoreUIUpdater<FloatTracker, TextMeshProUGUI, float>
{
    [SerializeField]
    protected string m_textInput;

    public void SetFormatter(string format) { m_textInput = format; }
    protected override void UpdateState(float value)
    {
        m_UIElement.text = string.Format(m_textInput, value);
    }
}
