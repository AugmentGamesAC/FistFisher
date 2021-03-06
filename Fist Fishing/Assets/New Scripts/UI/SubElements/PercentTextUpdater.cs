﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// updater for text based on percentages
/// </summary>
[System.Serializable]
public class PercentTextUpdater : CoreUIUpdater<PercentageTracker,Text,IPercentage>
{
    [SerializeField]
    protected string m_textInput;

    public void SetFormatter(string format) { m_textInput = format; }
    protected override void UpdateState(IPercentage value)
    {
        m_UIElement.text = string.Format(m_textInput, (float)value.Current, (float)value.Max, (float)value.Percent);
    }
}
