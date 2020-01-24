using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    //int appliedUpgrades;
    //    enum UpgradeTypes;
    //    PlayerStatManager statManager;
    //    Dictionary<UpgradeTypes, int> UpgradeCosts;
    //    delegate AppliedUpgradeChange;
    //--
    //void GenerateUpgrade();
    //    void GenerateUpgrade(UpgradeTypes type);
    //    void UpdateAppliedUpgrade();
    //    float RecalculateCost(Dictionary<Stats, float> statsModifier);
    //--
    //Responsibilities
    //- Generate an upgrade
    //- AppliedUpgradeChange passes in a float based on number of appliedUpgrades.
    //this calls updateCost on Upgrades.



    public enum UpgradeTypes
    {
        Arms,
        Legs,
        Torso
    }

    protected int m_appliedUpgrades;

    PlayerStatManager statManager;

    Dictionary<UpgradeTypes, int> UpgradeCosts;

    /// <summary>
    /// Creates an Upgrade with RNG.
    /// </summary>
    void GenerateUpgrade()
    {
        
    }

    /// <summary>
    /// Creates an Upgrade with RNG. within the "UpgradeType".
    /// </summary>
    /// <param name="type"></param>
    void GenerateUpgrade(UpgradeTypes type)
    {
        
    }

    /// <summary>
    /// Up the price of all upgrades after purchase.
    /// </summary>
    void UpdateAppliedUpgrade()
    {
        m_appliedUpgrades++;
        //if (OnRecaluclateCosts != default)
        //    OnRecaluclateCosts.Invoke(RecalculateCost);
    }


    public delegate void RecaluclateCosts(Dictionary<PlayerStatManager.Stats, float> statsModifier);
    public event RecaluclateCosts OnRecaluclateCosts;


    public float RecalculateCost(Dictionary<PlayerStatManager.Stats, float> statsModifier)
    {
        return 100.0f;
    }
}
