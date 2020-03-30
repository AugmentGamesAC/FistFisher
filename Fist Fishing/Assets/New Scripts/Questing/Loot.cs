using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    [SerializeField]
    protected int m_fishAmount;
    public int FishAmount => m_fishAmount;

    [SerializeField]
    protected int m_upgradeAmount;
    public int UpgradeAmount => m_upgradeAmount;

    [SerializeField]
    protected int m_baitAmount;
    public int BaitAmount => m_baitAmount;

    [SerializeField]
    protected int m_clams;
    public int Clams => m_clams;

    Reward m_reward = new Reward();

    public Loot()
    {
        for (int i = 0; i < m_fishAmount; i++)
        {
            m_reward.AddItem(new FishDefintion());
        }

        for (int i = 0; i < m_upgradeAmount; i++)
        {
            m_reward.AddItem(new Upgrade());
        }

        for (int i = 0; i < m_baitAmount; i++)
        {
            m_reward.AddItem(new Bait_IItem());
        }

        m_reward.SetClams(m_clams);
    }
}
