using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOxygenUpdater : ProgressBarUpdater
{
    public new void Start()
    {
        base.Start();
        UpdateTracker(PlayerInstance.Instance.Oxygen.Tracker);
    }
}
