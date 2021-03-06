﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// scriptable object for bait items
/// </summary>
[CreateAssetMenu(fileName = "BaitData", menuName = "ScriptableObjects/Bait", order = 1)]
public class Bait_IItem : ScriptableObject, IItem
{
    [SerializeField]
    protected int m_stackSize;
    public int StackSize => m_stackSize;
    [SerializeField]
    protected int m_id;
    [SerializeField]
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
    [SerializeField]
    protected FishBrain.FishClassification m_currentBaitType;
    public FishBrain.FishClassification BaitType { get { return m_currentBaitType; } set { m_currentBaitType = value; } }
    [SerializeField]
    protected FishBrain.FishClassification m_currentRepelType;
    public FishBrain.FishClassification RepelType { get { return m_currentRepelType; } set { m_currentRepelType = value; } }

    [SerializeField]
    protected int m_activeTurnCount = 3;
    public int TurnCount { get { return m_activeTurnCount; } set { m_activeTurnCount = value; } }
    public bool CanMerge(IItem newItem)
    {
        Bait_IItem item = newItem as Bait_IItem;
        if (item == default)
            return false;
        return m_currentBaitType == item.m_currentBaitType;
    }

    public bool ResolveDropCase(ISlotData slot, ISlotData oldSlot)
    {
        return false;
    }
 
    public bool IsStillActive()
    {
        TurnCount--;
        return TurnCount > 0;
    } 
}
