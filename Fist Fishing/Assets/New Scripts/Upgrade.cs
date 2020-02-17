using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : IItem
{
    /*
     Upgrade 
 --
 PlayerStatManager statManager;
 float cost;
 delegate wasUpdated;
 Dictionary<Stats, float> statsModifier;
 --
 UpdateCost(Func<Dictionary<Stats, float>, float>);
 ApplyUpgrade();
 */
    protected PlayerStatManager statManager;

    protected Dictionary<Stats, float> statsModifier;


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
        m_worth = calculateNewCost(statsModifier);
    }

    public Upgrade(string name, Sprite icon, string description, int worth, ItemType itemType = ItemType.Upgrade, int id = (int)ItemType.Upgrade, int stackSize = 1 )
    {
        m_name = name;
        m_display = icon;
        m_description = description;
        m_worth = worth;
        m_type = itemType;
        m_id = id;
        m_stackSize = stackSize;

        UpgradeManager.UpdateCosts += UpdateCost;
    }
    ~Upgrade()
    {
        UpgradeManager.UpdateCosts -= UpdateCost;
    }

    public void ApplyUpgrade()
    {
        if (statsModifier.Count <= 0 && statManager != default)
            return;

        foreach (var item in statsModifier)
        {
            statManager.UpdateStat(item.Key, item.Value);
        }

        UpgradeManager.UpdateAppliedUpgrade();
    }

    public bool CanMerge(IItem newItem)
    {
        return false;
    }
}
