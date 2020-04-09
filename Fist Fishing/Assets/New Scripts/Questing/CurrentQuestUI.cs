using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentQuestUI : CoreUIElement<QuestManager>
{
    [SerializeField]
    protected ImageUpdater IconUpdater;

    [SerializeField]
    protected TextUpdater DescriptionUpdater;

    [SerializeField]
    protected FloatTextUpdater TasksUpdater;

    public override void UpdateUI(QuestManager newData)
    {
        if (!ShouldUpdateUI(newData))
            return;

        IconUpdater.UpdateTracker(newData.CurrentQuest.QuestDef.Icon);
        DescriptionUpdater.UpdateTracker(newData.CurrentQuest.QuestDef.Description);
        TasksUpdater.UpdateTracker(newData.CurrentQuest.TaskLeft);
    }
}
