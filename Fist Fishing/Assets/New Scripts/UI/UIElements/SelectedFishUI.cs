using System.Collections;
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
    protected IntImageUpdater EnemyTypeDisplay;
    [SerializeField]
    protected ImageUpdater EnemyAction;
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
        EnemyAction.ForceUpdate(newData.FishInstance.FishData.IconDisplay);
        EnemyTypeDisplay.ForceUpdate( (int)Mathf.Log(2,(float) (newData.FishInstance.FishData.FishClassification & (FishBrain.FishClassification.Agressive|FishBrain.FishClassification.Fearful|FishBrain.FishClassification.Passive))) );
        EnemyHealthNumberDisplay.UpdateTracker(newData.FishInstance.Health.PercentTracker);
        EnemySwimSpeedDisplay.UpdateTracker(newData.Speed);
    }
}

