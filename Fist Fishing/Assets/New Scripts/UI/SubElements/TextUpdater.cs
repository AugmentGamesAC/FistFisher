using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class TextUpdater : CoreUIUpdater<TextTracker,Text,string>
{
    protected override void UpdateState(string value)
    {
        m_UIElement.text = value;
    }
}
