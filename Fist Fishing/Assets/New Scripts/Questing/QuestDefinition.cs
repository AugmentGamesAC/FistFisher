using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestDefinition
{
    [SerializeField]
    protected ImageTracker m_icon;
    public ImageTracker Icon => m_icon;

    [SerializeField]
    protected TextTracker m_description;
    public TextTracker Description => m_description;

    [SerializeField]
    protected Reward m_loot;
    public Reward LootGrab => m_loot;

    public QuestDefinition()
    {
        m_loot = new Reward();
    }

    [SerializeField]
    protected int m_taskAmount;
    public int TaskAmount => m_taskAmount;
}
