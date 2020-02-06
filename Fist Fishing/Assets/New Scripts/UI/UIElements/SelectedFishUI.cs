using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedFishUI : MonoBehaviour
{
    [SerializeField]
    protected FloatTextUpdater m_distanceDisplay;
    public FloatTextUpdater DistanceDisplay => m_distanceDisplay;

    [SerializeField]
    protected FloatTextUpdater m_speedDisplay;
    public FloatTextUpdater SpeedDisplay => m_speedDisplay;

    [SerializeField]
    protected FloatTextUpdater m_healthDisplay;
    public FloatTextUpdater HealthDisplay => m_healthDisplay;

    [SerializeField]
    protected ImageUpdater m_imageDisplay;
    public ImageUpdater ImageDisplay => m_imageDisplay;


    [SerializeField]
    protected CombatManager m_combatManager;

    private void Start()
    {
        m_combatManager.OnSelected += UpdateUI;
    }

    /// <summary>
    /// Gets selected fish from combat manager.
    /// </summary>
    /// <param name="newData"></param>
    public void UpdateUI(FishCombatInfo newData)
    {
        if (newData == null)
            return;

        DistanceDisplay.UpdateTracker(newData.CombatDistance);
        SpeedDisplay.UpdateTracker(newData.Speed);
        //HealthDisplay.UpdateTracker(newData.FishData.Health.CurrentAmount);
        //ImageDisplay.UpdateTracker(newData.FishData.Sprite);
    }
}
