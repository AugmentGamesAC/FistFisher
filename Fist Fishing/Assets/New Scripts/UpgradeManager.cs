using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

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
        Arms,// power, airConsumption
        Legs,//MovementSpeed, stealth, turnSpeed
        Torso//AirRestoration, MaxAir, Max Health.
    }

    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }

    protected static int m_appliedUpgrades;

    static Dictionary<UpgradeTypes, int> UpgradeCosts;

    [SerializeField]
    protected Sprite ArmIcon;
    [SerializeField]
    protected Sprite LegIcon;
    [SerializeField]
    protected Sprite TorsoIcon;

    [SerializeField]
    protected int BaseArmWorth;
    [SerializeField]
    protected int BaseLegWorth;
    [SerializeField]
    protected int BaseTorsoWorth;

    public void Awake()
    {
        actionList = new List<Func<Upgrade>>()
        {
            GenerateArmUpgrade,
            GenerateChestUpgrade,
            GenerateLegUpgrade
        };
    }

    protected List<Func<Upgrade>> actionList;

    /// <summary>
    /// called when shop boots up.
    /// Creates an Upgrade with RNG.
    /// </summary>
    public Upgrade GenerateUpgrade()
    {
        return RandomListEntry<Func<Upgrade>>(actionList)();
    }

    protected string GetRandomRarity()
    {
        return RandomEnum<Rarity>().ToString();
    }


    protected T RandomListEntry<T>(List<T> toRandomize)
    {
        return toRandomize[Random.Range(0, toRandomize.Count)];
    }

    protected T RandomEnum<T>()
    {
        var selection = Enum.GetValues(typeof(T));
        return (T)selection.GetValue(Random.Range(0, selection.Length));
    }

    protected int RandRange(int min, int max)
    {
        return Random.Range(min, max);
    }

    protected Upgrade GenerateArmUpgrade()
    {
        float PowerMod = RandRange(10, 30);
        float AirConsumptionMod = RandRange(25, 50);

        Dictionary<Stats, float> modifiers = new Dictionary<Stats, float>()
        {
            { Stats.Power , PowerMod },
            { Stats.AirConsumption, AirConsumptionMod }
        };

        return new Upgrade(string.Format("{0} Strong Arm", GetRandomRarity()), ArmIcon, "strong frogman RISE!!", BaseArmWorth, modifiers);
    }

    protected Upgrade GenerateLegUpgrade()
    {
        float MoveSpeedMod = RandRange(5, 15);
        float StealthMod = RandRange(50, 80);
        float TurnSpeedMod = RandRange(10, 20);

        Dictionary<Stats, float> modifiers = new Dictionary<Stats, float>()
        {
            { Stats.MovementSpeed , MoveSpeedMod },
            { Stats.Stealth, StealthMod },
            { Stats.TurnSpeed, TurnSpeedMod }
        };//MovementSpeed, stealth, turnSpeed

        return new Upgrade(string.Format("{0} Leg Muscles", GetRandomRarity()), LegIcon, "Strong legs lead happy families!!", BaseLegWorth, modifiers);
    }

    protected Upgrade GenerateChestUpgrade()
    {
        float MaxAirMod = RandRange(30, 60);
        float AirRestoreMod = RandRange(25, 50);
        float MaxHealthMod = RandRange(40, 80);

        Dictionary<Stats, float> modifiers = new Dictionary<Stats, float>()
        {
            { Stats.MaxAir , MaxAirMod },
            { Stats.AirRestoration, AirRestoreMod },
            { Stats.MaxHealth, MaxHealthMod }
        };

        return new Upgrade(string.Format("{0} Iron Lungs", GetRandomRarity()), TorsoIcon, "Cardiovasculature is very important kids!!", BaseTorsoWorth, modifiers);
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
        return m_appliedUpgrades * 100;
    }
}
