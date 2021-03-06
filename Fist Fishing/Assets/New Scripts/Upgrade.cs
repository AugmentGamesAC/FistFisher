﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// items that apply stats to the player
/// </summary>
public class Upgrade : IItem
{
    protected Dictionary<Stats, float> m_statsModifier;

    [SerializeField]
    protected int m_stackSize;
    public int StackSize => m_stackSize;

    [SerializeField]
    protected int m_id;
    public int ID => m_id;

    [SerializeField]
    protected int m_worth;
    public int WorthInCurrency => m_worth;

    [SerializeField]
    protected ItemType m_type;
    public ItemType Type => m_type;

    [SerializeField]
    protected string m_description;
    public string Description => m_description;

    [SerializeField]
    protected Sprite m_display;
    public Sprite IconDisplay => m_display;

    [SerializeField]
    protected string m_name;
    public string Name => m_name;

    /// <summary>
    /// takes function as argument that returns a float.
    /// </summary>
    /// <param name="func"></param>
    public void UpdateCost(System.Func<Dictionary<Stats, float>, int> calculateNewCost)
    {
        m_worth = calculateNewCost(m_statsModifier);
    }

    /// <summary>
    /// takes all the data to create a new upgrade
    /// </summary>
    public Upgrade(string name, Sprite icon, string description, int worth, Dictionary<Stats, float> statsModifier, ItemType itemType = ItemType.Upgrade, int id = (int)ItemType.Upgrade, int stackSize = 1)
    {
        m_name = name;
        m_display = icon;
        m_description = description;
        m_worth = worth;
        m_type = itemType;
        m_id = id;
        m_stackSize = stackSize;
        m_statsModifier = statsModifier;

        UpgradeManager.UpdateCosts += UpdateCost;
    }

    public Upgrade() { }
    ~Upgrade()
    {
        UpgradeManager.UpdateCosts -= UpdateCost;
    }

    /// <summary>
    /// Called when you buy
    /// applies the stats of the upgrade
    /// </summary>
    public void ApplyUpgrade()
    {
        PlayerStatManager statMan = PlayerInstance.Instance.PlayerStatMan;

        if (m_statsModifier.Count <= 0 && statMan != default)
            return;

        foreach (var item in m_statsModifier)
        {
            statMan.UpdateStat(item.Key, item.Value);
        }

        UpgradeManager.UpdateAppliedUpgrade();
    }

    public bool CanMerge(IItem newItem)
    {
        return false;
    }

    /// <summary>
    /// handles being dropped after being picked up from a slot
    /// </summary>
    public bool ResolveDropCase(ISlotData newSlot, ISlotData oldSlot)
    {
        if (!(newSlot.Manager is PlayerSlotManager))
            return false;

        ApplyUpgrade();
        oldSlot.RemoveItem();

        return true ;
    }
}
