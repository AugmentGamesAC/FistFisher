using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestManager : UITracker<QuestManager>
{
    [SerializeField]
    protected List<FishGatheringQuest> m_questLine;

    [SerializeField]
    protected int m_selectedQuestIndex = 0;
    public FishGatheringQuest CurrentQuest => m_questLine[m_selectedQuestIndex];

    //public QuestManager()
    //{
    //    CurrentQuest.Activate();
    //    UpdateState();
    //}

    public void NextQuest()
    {
        m_selectedQuestIndex++;

        if (m_selectedQuestIndex == m_questLine.Count)
        {
            ResolveQuestlineCompleted();
        }

        CurrentQuest.Activate();
        UpdateState();
    }

    public bool CheckGatheringProgress(IItem item, int count)
    {
        if (!(CurrentQuest is FishGatheringQuest))
        {
            Debug.Log("Not Gathering Quest!");
            return false;
        }

        if (!(item is FishDefintion))
        {
            Debug.Log("Not Fish!");
            return false;
        }

        if (count > 1)
            Debug.Log("more than one item gathered.");

        for (int i = 0; i < count; i++)
        {
            if (!(CurrentQuest.ItemGathered(item as FishDefintion)))
                return false;
        }
        UpdateState();

        return true;
    }

    /// <summary>
    /// currently loops back to first quest.
    /// </summary>
    protected void ResolveQuestlineCompleted()
    {
        m_selectedQuestIndex = 0;
        CurrentQuest.Activate();
    }

    protected new void UpdateState()
    {
        OnStateChange?.Invoke(this);
    }
}
