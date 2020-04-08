using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// updater for specifically player health percentage-based UI bar
/// </summary>
public class PlayerHealthUpdater : ProgressBarUpdater
{
    public void Start()
    {
        UpdateTracker(PlayerInstance.Instance.Health.Tracker);
    }
}
