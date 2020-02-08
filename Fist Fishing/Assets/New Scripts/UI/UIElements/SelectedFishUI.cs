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
    protected ImageUpdater EnemyTypeImageDisplay;
    [SerializeField]
    protected ImageUpdater EnemyIconDisplay;
    [SerializeField]
    protected FloatTextUpdater EnemyHealthNumberDisplay;
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

        ProgressBar.UpdateTracker(newData.FishInstance.Health.CurrentAmount);
        EnemyDistanceDisplay.UpdateTracker(newData.CombatDistance);
        MemberUpdate(EnemyNameDisplay, newData.FishInstance.FishData.Item.Name);
        MemberUpdate(EnemyIconDisplay, newData.FishInstance.FishData.IconDisplay);

        //EnemyTypeImageDisplay.UpdateTracker(MyPsudoData.TypeImage);
        MemberUpdate(EnemyHealthNumberDisplay, "{0}/" + newData.FishInstance.Health.Max.ToString(), newData.FishInstance.Health.CurrentAmount);
        EnemyHealthNumberDisplay.UpdateTracker(newData.FishInstance.Health.CurrentAmount);
        EnemySwimSpeedDisplay.UpdateTracker(newData.Speed);
    }
}

