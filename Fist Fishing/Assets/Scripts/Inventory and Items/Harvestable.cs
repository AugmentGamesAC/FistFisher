using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum HarvestableType
{
    NotSet, 
    DeadFish, 
    Coral1,
    Coral2,
}
/// <summary>
/// attached to any collectable/coral/etc
/// handles item data to attach to the GameObject
/// </summary>
public class Harvestable : MonoBehaviour, IItem
{

    public bool TransferProperties(AItem from)
    {
        if(from == null)
            return false;
        m_stackSize = from.StackSize;
        m_iD  = from.ID;
        m_worthInCurrency = from.m_worthInCurrency;
        m_Type = from.type;
        m_Description = from.description;
        m_iconDisplay = from.Display;
        //m_name = from.Name;

        return true;
    }

    //public HarvestableSpawner m_spawner;

    public TargetController m_targetController;

    public HarvestableType m_harvestableType;


    [SerializeField]
    protected int m_stackSize;
    public int StackSize => m_stackSize;

    [SerializeField]
    protected int m_iD;
    public int ID => m_iD;

    [SerializeField]
    protected int m_worthInCurrency;
    public int WorthInCurrency => m_worthInCurrency;

    [SerializeField]
    protected ItemType m_Type;
    public ItemType Type => m_Type;

    [SerializeField]
    protected string m_Description;
    public string Description => m_Description;

    [SerializeField]
    protected string m_name;
    public string Name => m_name;

    [SerializeField]
    protected Sprite m_iconDisplay;
    public Sprite IconDisplay => m_iconDisplay;

    void Start()
    {
        /*if (m_harvestableType == HarvestableType.NotSet)
            gameObject.SetActive(false);*/
    }

    public bool CanMerge(IItem newItem)
    {
        Harvestable harvestable = newItem as Harvestable;
        if (newItem == default)
            return false;
        return newItem.Type == m_Type;
    }

    public bool ResolveDropCase(ISlotData slot, ISlotData oldSlot)
    {
        return false;
    }
}
