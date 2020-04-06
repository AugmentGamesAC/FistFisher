using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CurrentQuestUI))]
public class QuestUpdater : CoreUIUpdater<UITracker<QuestManager>, CurrentQuestUI, QuestManager>
{
    public void Start()
    {
        UpdateTracker(PlayerInstance.Instance.QuestManager);
    }

    protected override void UpdateState(QuestManager value)
    {
        m_UIElement.UpdateUI(value);
    }
}
