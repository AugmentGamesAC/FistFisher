using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedFishUI : CoreUIElement
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

    
    [ContextMenu("DummyInit")]
    public void newPsudoData()
    {
        UpdateUI(new PsudoCombatFish());
    }

    /// <summary>
    /// Gets selected fish from combat manager.
    /// </summary>
    /// <param name="newData"></param>
    public void UpdateUI(FishCombatInfo newData)
    {
        if (newData == null)
        {
            gameObject.SetActive(false);
            return;
        }

        EnemyDistanceDisplay.UpdateTracker(newData.CombatDistance);
        MemberUpdate(EnemyNameDisplay, newData.FishData.Item.Name);
        MemberUpdate(EnemyIconDisplay, newData.FishData.IconDisplay);

        //EnemyTypeImageDisplay.UpdateTracker(MyPsudoData.TypeImage);
        MemberUpdate(EnemyHealthNumberDisplay, string.Format("\\{0\\}/{0}", newData.FishData.Health.Max), newData.FishData.Health.CurrentAmount);
        EnemyHealthNumberDisplay.UpdateTracker(newData.FishData.Health.CurrentAmount);
        EnemySwimSpeedDisplay.UpdateTracker(newData.Speed);

    }
}


[System.Serializable]
public class PsudoCombatFish : FishCombatInfo
{
    public PsudoCombatFish()
    {
        Speed.SetValue(5);
        CombatDistance.SetValue(14);
    }
}
