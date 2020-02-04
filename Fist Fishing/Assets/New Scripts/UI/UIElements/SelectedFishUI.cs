using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedFishUI : MonoBehaviour
{
    [SerializeField]
    protected FloatTextUpdater DistanceDisplay;

    [SerializeField]
    protected CombatManager m_combatManager;

    private void Start()
    {
        m_combatManager.OnSelected += UpdateUI;
    }

    /// <summary>
    /// GEts selected fish from combat manager.
    /// </summary>
    /// <param name="newData"></param>
    public void UpdateUI(FishCombatInfo newData)
    {
        DistanceDisplay.UpdateTracker(newData.CombatDistance);       
    }
}
