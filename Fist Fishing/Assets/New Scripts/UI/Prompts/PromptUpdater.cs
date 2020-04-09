using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// updater for all prompts
/// </summary>
[RequireComponent(typeof(PromptElement))]
public class PromptUpdater : CoreUIUpdater<UITracker<PromptManager>, PromptElement, PromptManager>
{
    public void Start()
    {
        UpdateTracker(PlayerInstance.Instance.PromptManager);
    }


    protected override void UpdateState(PromptManager value)
    {
        m_UIElement.UpdateUI(value);
    }
}


