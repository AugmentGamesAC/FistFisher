using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedFishUI : MonoBehaviour
{
    [SerializeField]
    protected FishCombatInfo Info;

    [SerializeField]
    protected CombatManager m_combatManager;


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

    
    [ContextMenu("SwapData")]
    public void newPsudoData()
    {
        UpdateUI(new PsudoCombatFish());
    }

    public void UpdateUI(FishCombatInfo newData)
    {
        Info = newData;

        EnemyDistanceDisplay.UpdateTracker(Info.CombatDistance);
        //EnemyNameDisplay.UpdateTracker(MyPsudoData.Name);
        //EnemyTypeImageDisplay.UpdateTracker(MyPsudoData.TypeImage);
        //EnemyIconDisplay.UpdateTracker(MyPsudoData.IconImage);
        EnemyHealthNumberDisplay.UpdateTracker(Info.FishData.Health.CurrentAmount);
        EnemySwimSpeedDisplay.UpdateTracker(Info.Speed);
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
