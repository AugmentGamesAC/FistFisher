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
    protected FloatTextUpdater DamageUpdater;

    [SerializeField]
    protected TextUpdater SweetSpotUpdater;
    [SerializeField]
    protected TextUpdater NameUpdater;
    [SerializeField]
    protected TextUpdater DescriptionUpdater;

    [SerializeField]
    protected ImageUpdater IconUpdater;

    [SerializeField]
    protected ImageUpdater AttackIconUpdater;

    [SerializeField]
    protected ImageUpdater PinWheelIconUpdater;

    //[SerializeField]
    //protected ImageUpdater ItemIconUpdater;

    public override void UpdateUI(CombatMoveInfo newData)
    {
        if (!ShouldUpdateUI(newData))
            return;

        //SweetSpotUpdater.UpdateTracker(newData.SweetSpot);
        ThrashUpdater.UpdateTracker(newData.Noise);
        OxygenUpdater.UpdateTracker(newData.OxygenConsumption);
        MoveDistanceUpdater.UpdateTracker(newData.MoveDistance);
        DamageUpdater.UpdateTracker(newData.Damage);

        NameUpdater.UpdateTracker(newData.Name);
        DescriptionUpdater.UpdateTracker(newData.Description);

        IconUpdater.UpdateTracker(newData.Icon);
        AttackIconUpdater.UpdateTracker(newData.Icon);
        PinWheelIconUpdater.UpdateTracker(newData.Icon);

    }
}
