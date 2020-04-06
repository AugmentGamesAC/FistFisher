using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GatheringQuest<T> : Quest where T: IItem
{
    [SerializeField]
    protected T m_itemType;

    public GatheringQuest(QuestDefinition def, T itemType) : base(def)
    {
        m_itemType = itemType;
    }
    public GatheringQuest(T itemType) : base()
    {
        m_itemType = itemType;
    }
    public GatheringQuest() : base() { }

    //returns true if the item is what we are looking for.
    public bool ItemGathered(T item)
    {
        if (item.Name != m_itemType.Name)
            return false;

        m_tasksLeft.SetValue(m_tasksLeft - 1);

        if (CheckTaskCompleted())
            return false;

        return true;
    }
    
    //check if we have met the requirements, if so, call resolve completed.
    protected bool CheckTaskCompleted()
    {
        if (m_tasksLeft > 0)
            return false;

        ResolveCompletedQuest();

        return true;
    }
}
