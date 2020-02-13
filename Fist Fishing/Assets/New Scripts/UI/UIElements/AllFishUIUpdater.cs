using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CoreUIElement<FishCombatInfo>))]
public class AllFishUIUpdater : CoreUIUpdater<SingleSelectionListTracker<FishCombatInfo>, CoreUIElement<FishCombatInfo>, ISingleSelectionList<FishCombatInfo>>
{
    [SerializeField]
    protected List<UnselectedFishUI> m_unselectedFish = new List<UnselectedFishUI>();

    protected override void UpdateState(ISingleSelectionList<FishCombatInfo> value)
    {
        if (value == default || value.Count < 1)
        {
            ClearUI();
            return;
        }

        m_UIElement.UpdateUI(value.SelectedItem);

        int targetIndex = 0;
        foreach (var unselected in m_unselectedFish)
        {
            if (targetIndex == value.Selection)
                targetIndex++;

            unselected.UpdateUI(targetIndex >= value.Count ? default : value[targetIndex]);
            targetIndex++;
        }
    }

    protected void ClearUI()
    {
        m_UIElement.UpdateUI(default);
        foreach (var unselected in m_unselectedFish)
            unselected.UpdateUI(default);
    }
}

