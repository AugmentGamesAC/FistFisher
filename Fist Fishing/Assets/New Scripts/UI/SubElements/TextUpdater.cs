﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// updater for text-based UI elements
/// </summary>
[System.Serializable]
public class TextUpdater : CoreUIUpdater<TextTracker,Text,string>
{
    protected override void UpdateState(string value)
    {
        m_UIElement.text = value;
    }
}
