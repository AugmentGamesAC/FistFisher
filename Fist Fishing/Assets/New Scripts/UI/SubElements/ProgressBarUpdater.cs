using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// updater for percentage-based UI bars
/// </summary>
public class ProgressBarUpdater : CoreUIUpdater<PercentageTracker,Image,IPercentage>
{
    protected override void UpdateState(IPercentage fillValue)
    {
        //Set the fill amount based on what is changed in the tracker
        m_UIElement.fillAmount = fillValue.Percent;
    }
}
