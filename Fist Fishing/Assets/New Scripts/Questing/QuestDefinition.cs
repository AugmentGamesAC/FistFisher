using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestDefinition
{
    [SerializeField]
    protected Sprite m_icon;
    public Sprite Icon => m_icon;

    [SerializeField]
    protected string m_description;
    public string Description => m_description;

    [SerializeField]
    protected Reward m_loot;
    public Reward LootGrab => m_loot;

    [SerializeField]
    protected int m_taskAmount;
    public int TaskAmount => m_taskAmount;
}
