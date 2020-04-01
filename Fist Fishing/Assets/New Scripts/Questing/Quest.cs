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
    protected QuestDefinition m_def = new QuestDefinition();
    public QuestDefinition QuestDef => m_def;

    public Quest(QuestDefinition def)
    {
        if (def == default)
            throw new System.EntryPointNotFoundException("Quest definition was default");

        m_def = def;
    }
    public Quest()
    {
    }
    public virtual void Activate()
    {
        m_IsActive = true;
        m_tasksLeft = m_def.TaskAmount;
    }
    public virtual void Deactivate()
    {
        m_IsActive = false;
        QuestManager.Instance.NextQuest();
    }
    public virtual void ResolveCompletedQuest()
    {
        //List<FishDefintion> FishItems = m_def.LootGrab.FishReward;
        //List<Bait_IItem> BaitItems = m_def.LootGrab.BaitReward;
        //add items from reward to players inventory.
        //foreach (var item in FishItems)
        //{
        //    PlayerInstance.Instance.ItemInventory.AddItem(item, 1);
        //}
        //foreach (var item in BaitItems)
        //{
        //    PlayerInstance.Instance.ItemInventory.AddItem(item, 1);
        //}

        //for (int i = 0; i < m_def.LootGrab.UpgradeAmount; i++)
        //{
        //    UpgradeManager.Instance.GenerateUpgrade().ApplyUpgrade();
        //}

        //add clams to player's inventory from the reward.
        PlayerInstance.Instance.Clams.SetValue(PlayerInstance.Instance.Clams + m_def.LootGrab.Clams);

        Deactivate();
    }
}
