using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestTypes
{
    Gathering,
    Punching
}

[System.Serializable]
public class Quest
{
    [SerializeField]
    protected bool m_IsActive;
    public bool IsActive => m_IsActive;

    [SerializeField]
    protected int m_tasksLeft;
    public int TaskLeft => m_tasksLeft;

    [SerializeField]
    protected QuestDefinition m_def;
    public QuestDefinition QuestDef => m_def;

    public Quest(QuestDefinition def)
    {
        if (def == default)
            throw new System.EntryPointNotFoundException("Quest definition was default");

        m_def = def;
        m_tasksLeft = m_def.TaskAmount;
    }
    public virtual void Activate()
    {
        m_IsActive = true;
    }
    public virtual void Deactivate()
    {
        m_IsActive = false;
        QuestManager.Instance.NextQuest();
    }
    public virtual void ResolveCompletedQuest()
    {
        List<IItem> rewardItems = m_def.LootGrab.Items;
        //add items from reward to players inventory.
        foreach (var item in rewardItems)
        {
            if(item is Upgrade)
            {
                (item as Upgrade).ApplyUpgrade();
                continue;
            }
            PlayerInstance.Instance.ItemInventory.AddItem(item, 1);
        }

        //add clams to player's inventory from the reward.
        PlayerInstance.Instance.Clams.SetValue(PlayerInstance.Instance.Clams + m_def.LootGrab.Clams);

        Deactivate();
    }
}
