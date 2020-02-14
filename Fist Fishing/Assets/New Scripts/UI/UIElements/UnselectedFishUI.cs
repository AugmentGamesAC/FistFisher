using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnselectedFishUI : CoreUIElement<FishCombatInfo>
{

    [SerializeField]
    protected TextUpdater EnemyNameDisplay;
    [SerializeField]
    protected IntImageUpdater EnemyTypeImageDisplay;
    [SerializeField]
    protected ImageUpdater EnemyIconDisplay;
    [SerializeField]
    protected IntImageUpdater EnemyAction;
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
        EnemyNameDisplay.ForceUpdate(newData.FishInstance.FishData.Item.Name);
        EnemyIconDisplay.ForceUpdate(newData.FishInstance.FishData.IconDisplay);
        EnemyTypeImageDisplay.ForceUpdate((int)Mathf.Log(2, (float)(newData.FishInstance.FishData.FishClassification & (FishBrain.FishClassification.Agressive | FishBrain.FishClassification.Fearful | FishBrain.FishClassification.Passive))));
        EnemyAction.ForceUpdate((int)Mathf.Log(2, (float)(1)));
    }
}


