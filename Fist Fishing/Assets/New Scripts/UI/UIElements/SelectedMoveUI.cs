using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedMoveUI : CoreUIElement<CombatMoveInfo>
{

    [SerializeField]
    protected FloatTextUpdater ThrashUpdater;
    [SerializeField]
    protected FloatTextUpdater OxygenUpdater;
    [SerializeField]
    protected FloatTextUpdater MoveDistanceUpdater;

    [SerializeField]
    protected TextUpdater SweetSpotUpdater;
    [SerializeField]
    protected TextUpdater NameUpdater;
    [SerializeField]
    protected TextUpdater DescriptionUpdater;

    [SerializeField]
    protected ImageUpdater IconUpdater;

    public override void UpdateUI(CombatMoveInfo newData)
    {
        if (!ShouldUpdateUI(newData))
            return;

        //SweetSpotUpdater.UpdateTracker(newData.SweetSpot);
        ThrashUpdater.UpdateTracker(newData.Noise);
        OxygenUpdater.UpdateTracker(newData.OxygenConsumption);
        MoveDistanceUpdater.UpdateTracker(newData.MoveDistance);

        NameUpdater.UpdateTracker(newData.Name);
        DescriptionUpdater.UpdateTracker(newData.Description);

        IconUpdater.UpdateTracker(newData.Icon);
    }
}
