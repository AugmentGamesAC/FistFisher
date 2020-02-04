using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedFishUI : MonoBehaviour
{
    [SerializeField]
    protected FishCombatInfo newInfo

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

    }

    public void UpdateUI(FishCombatInfo newData)
    {
        //out with old
        MyPsudoData = newData;
        EnemyDistanceDisplay.UpdateTracker(MyPsudoData.Distance);
        EnemyNameDisplay.UpdateTracker(MyPsudoData.Name);
        EnemyTypeImageDisplay.UpdateTracker(MyPsudoData.TypeImage);
        EnemyIconDisplay.UpdateTracker(MyPsudoData.IconImage);
        EnemyHealthNumberDisplay.UpdateTracker(MyPsudoData.HealthText);
        EnemySwimSpeedDisplay.UpdateTracker(MyPsudoData.SwimSpeed);
    }
}

