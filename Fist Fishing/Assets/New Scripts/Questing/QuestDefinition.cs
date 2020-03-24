using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDefinition : ScriptableObject
{
    [SerializeField]
    protected Sprite m_icon { get; private set; }

    [SerializeField]
    public string Description { get; private set; }

    [SerializeField]
    public Reward Loot { get; private set; }

    [SerializeField]
    public QuestTypes QuestType { get; private set; }

    [SerializeField]
    public int TaskAmount { get; private set; }
}
