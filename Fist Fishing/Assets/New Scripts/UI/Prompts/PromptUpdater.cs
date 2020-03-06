using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptUpdater : CoreUIElement<Prompt>
{
    [SerializeField]
    protected ImageUpdater m_promptDisplayUpdater;

    [SerializeField]
    protected TextUpdater m_textUpdater;
    //Possible description updater 

    public override void UpdateUI(Prompt newData)
    {
        if (!ShouldUpdateUI(newData))
            return;

        m_promptDisplayUpdater.UpdateTracker(newData.Display);
        m_textUpdater.UpdateTracker(newData.Description);
    }
}
