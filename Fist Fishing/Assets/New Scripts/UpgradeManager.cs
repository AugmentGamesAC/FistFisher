﻿using System.Collections;
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

    PlayerStatManager statManager;

    Dictionary<UpgradeTypes, int> UpgradeCosts;

    public delegate void AppliedUpgradeChange();

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
        UpdateCosts?.Invoke(RecalculateCost);
    }

    public delegate void CostChangeListener(System.Func<Dictionary<PlayerStatManager.Stats, float>, float> updatefunction);
    public static CostChangeListener UpdateCosts;

    float RecalculateCost(Dictionary<PlayerStatManager.Stats, float> statsModifier)
    {
        return 100.0f;
    }
}