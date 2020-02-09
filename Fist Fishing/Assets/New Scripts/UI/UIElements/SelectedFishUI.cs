﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedFishUI : CoreUIElement<FishCombatInfo>
{
    [SerializeField]
    protected FloatTextUpdater EnemyDistanceDisplay;
    [SerializeField]
    protected TextUpdater EnemyNameDisplay;
    [SerializeField]
    protected ImageUpdater EnemyTypeImageDisplay;
    [SerializeField]
    protected ImageUpdater EnemyIconDisplay;
    [SerializeField]
    protected PercentTextUpdater EnemyHealthNumberDisplay;
    [SerializeField]
    protected FloatTextUpdater EnemySwimSpeedDisplay;
    [SerializeField]
    protected ProgressBarUpdater ProgressBar;

    /// <summary>
    /// Gets selected fish from combat manager.
    /// </summary>
    /// <param name="newData"></param>
    public override void UpdateUI(FishCombatInfo newData)
    {
        if (!ShouldUpdateUI(newData))
            return;

        ProgressBar.UpdateTracker(newData.FishInstance.Health.PercentTracker);
        EnemyDistanceDisplay.UpdateTracker(newData.CombatDistance);
        MemberUpdate(EnemyNameDisplay, newData.FishInstance.FishData.Item.Name);
        MemberUpdate(EnemyIconDisplay, newData.FishInstance.FishData.IconDisplay);


        EnemyHealthNumberDisplay.UpdateTracker(newData.FishInstance.Health.PercentTracker);
        EnemySwimSpeedDisplay.UpdateTracker(newData.Speed);
    }
}

