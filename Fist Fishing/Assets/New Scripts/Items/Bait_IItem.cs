using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    protected FishBrain.FishClassification m_currentBaitType = FishBrain.FishClassification.BaitSensitive1;
    [SerializeField]
    public FishBrain.FishClassification BaitType { get { return m_currentBaitType; } set { m_currentBaitType = value; } }
    [SerializeField]
    protected float m_direction;
    /// <summary>
    /// Set to either positive 1 or negative 1. FOR TESTING
    /// </summary>
    public float Direction { get { return m_direction; } set { m_direction = value; } }

    [SerializeField]
    protected int m_activeTurnCount;
    public int MaxTurnCount { get { return m_activeTurnCount; } set { m_activeTurnCount = value; } }
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
}
