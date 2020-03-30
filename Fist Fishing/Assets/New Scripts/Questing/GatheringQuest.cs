using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatheringQuest : Quest
{
    protected IItem m_itemType;

    public GatheringQuest(QuestDefinition def, IItem itemType) : base(def)
    {
        m_itemType = itemType;
    }
    //returns true if the item is what we are looking for.
    public bool ItemGathered(IItem item)
    {
        if (item.Type != m_itemType.Type)
            return false;

        m_tasksLeft--;

        CheckTaskCompleted();

        return true;
    }
    
    //check if we have met the requirements, if so, call resolve completed.
    protected bool CheckTaskCompleted()
    {
        if (m_tasksLeft >= 0)
            return false;

        ResolveCompletedQuest();

        return true;
    }
}
