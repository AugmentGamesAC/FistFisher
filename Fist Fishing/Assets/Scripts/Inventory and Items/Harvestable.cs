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
public class Harvestable : MonoBehaviour, IItem
{
    public HarvestableSpawner m_spawner;

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
        if (m_harvestableType == HarvestableType.NotSet)
            gameObject.SetActive(false);
    }

    public bool CanMerge(IItem newItem)
    {
        Harvestable harvestable = newItem as Harvestable;
        if (newItem == default)
            return false;
        return newItem.Type == m_Type;
    }
}
