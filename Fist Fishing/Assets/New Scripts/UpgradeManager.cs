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

    protected static int m_appliedUpgrades;

    PlayerStatManager statManager;

    static Dictionary<UpgradeTypes, int> UpgradeCosts;

    /// <summary>
    /// Creates an Upgrade with RNG.
    /// </summary>
    void GenerateUpgrade()
    {
        //Upgrade upgrade = new Upgrade()
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
    public static void UpdateAppliedUpgrade()
    {
        //this needs to be first.
        m_appliedUpgrades++;
        UpdateCosts?.Invoke(RecalculateCost);
    }

    public delegate void CostChangeListener(System.Func<Dictionary<Stats, float>, int> updatefunction);
    public static CostChangeListener UpdateCosts;

    static int RecalculateCost(Dictionary<Stats, float> statsModifier)
    {
        return 100;
    }
}
