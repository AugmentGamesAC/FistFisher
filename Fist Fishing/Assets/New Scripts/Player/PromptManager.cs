using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// manager for all prompts
/// figuring out the active prompts and their priority and display
/// </summary>
public class PromptManager : UITracker<PromptManager>
{
    [SerializeField]
    protected ImageTracker m_display = new ImageTracker();
    public ImageTracker Display => m_display;

    [SerializeField]
    protected TextTracker m_description = new TextTracker();
    public TextTracker Description => m_description;

    Dictionary<int, HashSet<Prompt>> priorityPromptGroups = new Dictionary<int, HashSet<Prompt>>();

    protected int m_currentPriority;
    public int CurrentPriority => m_currentPriority;

    public int CurrentPriorityCount => priorityPromptGroups[CurrentPriority].Count;

    public void AddPriority(Prompt prompt)
    {
        HashSet<Prompt> priorityGroup;

        if (!priorityPromptGroups.TryGetValue(prompt.Priority, out priorityGroup))
            priorityPromptGroups.Add(prompt.Priority, priorityGroup = new HashSet<Prompt>());

        if (priorityGroup.Contains(prompt))
            return;
        priorityGroup.Add(prompt);
    }

    /// <summary>
    /// Call this when an object that holds a prompt dies like a fish or Harvestable.
    /// </summary>
    /// <param name="prompt"></param>
    public void RemovePriority(Prompt prompt)
    {
        HashSet<Prompt> priorityGroup;

        if (!priorityPromptGroups.TryGetValue(prompt.Priority, out priorityGroup))
            return;

        priorityGroup.Remove(prompt);
        if (priorityGroup.Count == 0)
            priorityPromptGroups.Remove(prompt.Priority);
    }


    public void RegisterPrompt(Prompt prompt)
    {
        AddPriority(prompt);

        if (CurrentPriority <= prompt.Priority && CurrentPriority != 0)
            return;

        m_currentPriority = prompt.Priority;
        DisplayPrompt(prompt);
    }

    public void DeregisterPrompt(Prompt prompt)
    {
        RemovePriority(prompt);

        if (priorityPromptGroups.Count() == 0)
        {
            m_currentPriority = 0;
            UpdateState();
            return;
        }

        m_currentPriority = priorityPromptGroups.Keys.Min();

        DisplayPrompt(priorityPromptGroups[m_currentPriority].First());
    }

    public void DisplayPrompt(Prompt newPrompt)
    {
        m_display.SetValue(newPrompt.Display);
        m_description.SetValue(newPrompt.Description);
        UpdateState();
    }

    public void HideCurrentPrompt()
    {
        m_currentPriority = 0;
        UpdateState();
    }

    protected new void UpdateState()
    {
        OnStateChange?.Invoke(this);
    }
}

