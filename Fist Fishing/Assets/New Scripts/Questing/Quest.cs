using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestTypes
{
    Gathering,
    Punching
}

public class Quest
{
    protected bool m_IsActive;

    protected QuestDefinition m_def;

    public delegate void QuestFinished();
    public event QuestFinished OnQuestSatisfied;

    public Quest(QuestDefinition def)
    {
        throw new System.NotImplementedException();
    }
    public virtual void Activate()
    {
        throw new System.NotImplementedException();
    }
    public virtual void Deactivate()
    {
        throw new System.NotImplementedException();
    }
    public virtual void ApplyReward()
    {
        throw new System.NotImplementedException();
    }
}
