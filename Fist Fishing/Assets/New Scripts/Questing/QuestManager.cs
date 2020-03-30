using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    [SerializeField]
    protected List<Quest> m_questLine;

    [SerializeField]
    protected int m_selectedQuestIndex = 0;
    public Quest CurrentQuest => m_questLine[m_selectedQuestIndex];

    public delegate void QuestTrigger(IItem item);
    public event QuestTrigger OnGatheringProgress;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;

        OnGatheringProgress += CheckGatheringProgress;
    }

    public void NextQuest()
    {
        m_selectedQuestIndex++;

        if (m_selectedQuestIndex == m_questLine.Count)
        {
            ResolveQuestlineCompleted();
            return;
        }
        
        CurrentQuest.Activate();
    }

    protected void CheckGatheringProgress(IItem item)
    {
        if (!(CurrentQuest is GatheringQuest))
        {
            Debug.Log("Not Gathering Quest!");
            return;
        }

        GatheringQuest gatheringQuest = CurrentQuest as GatheringQuest;

        gatheringQuest.ItemGathered(item);
    }

    /// <summary>
    /// currently loops back to first quest.
    /// </summary>
    protected void ResolveQuestlineCompleted()
    {
        m_selectedQuestIndex = 0;
        CurrentQuest.Activate();
    }
}
