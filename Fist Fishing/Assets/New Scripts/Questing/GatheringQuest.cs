using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatheringQuest : Quest
{
    protected int m_tasksLeft;
    protected IItem m_itemType;

    public GatheringQuest(QuestDefinition def, IItem itemType) : base(def)
    {
        throw new System.NotImplementedException();
    }
    protected bool ItemGathered(IItem item)
    {
        throw new System.NotImplementedException();
    }
    //returns true if the item is what we are looking for.
    protected void TaskCompleted()
    {
        throw new System.NotImplementedException();
    }
}
