using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUpdater : ProgressBarUpdater
{
    public new void Start()
    {
        base.Start();
        UpdateTracker(PlayerInstance.Instance.Health.Tracker);
    }
}
