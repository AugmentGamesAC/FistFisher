using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    [SerializeField]
    protected List<FishGatheringQuest> m_questLine = new List<FishGatheringQuest>();

    [SerializeField]
    protected int m_selectedQuestIndex = 0;
    public FishGatheringQuest CurrentQuest => m_questLine[m_selectedQuestIndex];

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;

        CurrentQuest.Activate();
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
}
