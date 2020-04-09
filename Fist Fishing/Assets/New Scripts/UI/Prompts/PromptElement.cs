using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI elements for prompts
/// </summary>
public class PromptElement : CoreUIElement<PromptManager>
{
    [SerializeField]
    protected ImageUpdater m_promptDisplayUpdater;
    [SerializeField]
    protected TextUpdater m_textUpdater;
    //Possible description updater 


    public override void UpdateUI(PromptManager newData)
    {
        if (!ShouldUpdateUI(newData,x=>x.CurrentPriority != 0))
            return;

        m_promptDisplayUpdater.UpdateTracker(newData.Display);
        m_textUpdater.ForceUpdate(string.Format( newData.Description, newData.CurrentPriorityCount));
    }
}
