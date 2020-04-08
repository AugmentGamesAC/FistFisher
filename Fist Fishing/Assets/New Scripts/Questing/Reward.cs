using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// data and processing for quest rewards
/// </summary>
[System.Serializable]
public class Reward
{
    [SerializeField]
    protected List<IItem> m_Items = new List<IItem>();
    public List<IItem> Items => m_Items;

    [SerializeField]
    protected List<FishDefintion> m_fish = new List<FishDefintion>();
    public List<FishDefintion> FishReward => m_fish;

    [SerializeField]
    protected List<Bait_IItem> m_baits = new List<Bait_IItem>();
    public List<Bait_IItem> BaitReward => m_baits;

    [SerializeField]
    protected List<Upgrade> m_upgradeReward = new List<Upgrade>();
    public List<Upgrade> UpgradeRewards => m_upgradeReward;

    [SerializeField]
    protected int m_upgradeAmount;
    public int UpgradeAmount => m_upgradeAmount;

    [SerializeField]
    protected IntTracker m_clams;
    public IntTracker Clams => m_clams;

    public Reward()
    {
        for (int i = 0; i < m_upgradeAmount; i++)
        {
            UpgradeRewards.Add(UpgradeManager.Instance.GenerateUpgrade());
        }
    }

    public void AddItem(IItem item)
    {
        if (item == default)
            return;
        m_Items.Add(item);
    }

    public void SetClams(int clams)
    {
        m_clams.SetValue(clams);
    }
}
