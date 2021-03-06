﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI updaters for the selected fish in combat
/// </summary>
public class SelectedFishUI : CoreUIElement<FishCombatInfo>
{
    [SerializeField]
    protected FloatTextUpdater EnemyDistanceDisplay;
    [SerializeField]
    protected TextUpdater EnemyNameDisplay;
    [SerializeField]
    protected IntImageUpdater EnemyTypeDisplay;
    [SerializeField]
    protected IntImageUpdater EnemyAction;
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
        EnemyNameDisplay.ForceUpdate(newData.FishInstance.FishData.Item.Name);
        EnemyIconDisplay.ForceUpdate(newData.FishInstance.FishData.IconDisplay);
        //EnemyAction.ForceUpdate(newData.FishInstance.FishData.IconDisplay);
        EnemyAction.ForceUpdate(newData.FishInstance.FishData.FishClassification.HasFlag(FishBrain.FishClassification.Aggressive)?1:0 );
        EnemyTypeDisplay.ForceUpdate(
            newData.FishInstance.FishData.FishClassification.HasFlag(FishBrain.FishClassification.Passive) ? 0 :
                        newData.FishInstance.FishData.FishClassification.HasFlag(FishBrain.FishClassification.Aggressive) ? 1 : 2);

        EnemyHealthNumberDisplay.UpdateTracker(newData.FishInstance.Health.PercentTracker);
        EnemySwimSpeedDisplay.UpdateTracker(newData.Speed);
    }
}

