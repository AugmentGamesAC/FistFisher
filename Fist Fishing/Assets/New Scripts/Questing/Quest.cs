using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestTypes
{
    Gathering,
    Punching
}

/// <summary>
/// base for all quests
/// tracks if active and what task(s) is/are required for completion
/// </summary>
[System.Serializable]
public class Quest
{
    [SerializeField]
    protected bool m_IsActive;
    public bool IsActive => m_IsActive;

    [SerializeField]
    protected FloatTracker m_tasksLeft;
    public FloatTracker TaskLeft => m_tasksLeft;

    [SerializeField]
    protected QuestDefinition m_def = new QuestDefinition();
    public QuestDefinition QuestDef => m_def;

    public Quest(QuestDefinition def)
    {
        if (def == default)
            throw new System.EntryPointNotFoundException("Quest definition was default");

        m_def = def;
    }
    public Quest() { }
    public virtual void Activate()
    {
        m_IsActive = true;
        m_tasksLeft.SetValue(m_def.TaskAmount);
    }
    public virtual void Deactivate()
    {
        m_IsActive = false;
        PlayerInstance.Instance.QuestManager.NextQuest();
    }
    public virtual void ResolveCompletedQuest()
    {
        //add clams to player's inventory from the reward.
        PlayerInstance.Instance.Clams.SetValue(PlayerInstance.Instance.Clams + m_def.LootGrab.Clams);

        Deactivate();
    }
}
