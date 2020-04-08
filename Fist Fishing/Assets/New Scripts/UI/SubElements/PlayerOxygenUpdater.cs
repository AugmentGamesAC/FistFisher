using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// updater for specifically player oxygen percentage-based UI bar
/// </summary>
public class PlayerOxygenUpdater : ProgressBarUpdater
{
    public void Start()
    {
        UpdateTracker(PlayerInstance.Instance.Oxygen.Tracker);
    }
}
