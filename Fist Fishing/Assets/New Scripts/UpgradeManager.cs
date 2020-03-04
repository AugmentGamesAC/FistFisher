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

    [SerializeField]
    protected int BaseUpgradesCost;

    [SerializeField]
    protected static int m_currentUpgradesCost;
    public int CurrentUpgradeCost => m_currentUpgradesCost;

    static Dictionary<Stats, float> StatMultiplierPerLevel;

    static Dictionary<Stats, int> CostMultiplierPerLevel;

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

    [SerializeField]
    protected string ArmUpgradeDescription;
    [SerializeField]
    protected string LegUpgradeDescription;
    [SerializeField]
    protected string TorsoUpgradeDescription;

    public void Awake()
    {
        m_currentUpgradesCost = BaseUpgradesCost;

        actionList = new List<Func<Upgrade>>()
        {
            GenerateArmUpgrade,
            GenerateChestUpgrade,
            GenerateLegUpgrade
        };

        StatMultiplierPerLevel = new Dictionary<Stats, float>() {
            { Stats.AirConsumption, 0.05f},
            { Stats.AirRestoration, 0.5f},
            { Stats.MaxAir, 0.2f},
            { Stats.MaxHealth , 0.25f},
            { Stats.MovementSpeed, 0.1f},
            { Stats.Power, 0.25f},
            { Stats.Stealth, 0.5f},
            { Stats.TurnSpeed, 1}
        };

        CostMultiplierPerLevel = new Dictionary<Stats, int>()
        {
            { Stats.AirConsumption, 3},
            { Stats.AirRestoration, 5},
            { Stats.MaxAir, 2},
            { Stats.MaxHealth , 2},
            { Stats.MovementSpeed, 3},
            { Stats.Power, 3},
            { Stats.Stealth, 3},
            { Stats.TurnSpeed, 1}
        };
    }

    /*
 (AR) (50%) (5*) Air Regeneration - base value needs to be low 
 (AC) (5%)  (3*) Air Consumption - The amount of air that is used up as the player performs actions. Base 
 (AV) (20%) (2*) Air Capacity - amount of air 
 (AM) (50%) (1*)  Air Metabolize rate - how much oxygen per pound it cost,
 (M) (50%) (1*)  how much heal per pound
 (SF) (10%) (3*) Forward 
 (ST) (100%) (1*) Swim turning
 (SN) (50%) (3*)  Swim Noise
 (CD) (25%) (3*)  Combat Damage
 (CN) (50%) (3*)  Combat Noise
 (CA) (10%) (4*)  Combat Air Use
 (H) (25%) (2*) Health
*/

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

    protected string GetRarity(int upgradelevel)
    {
        var selection = Enum.GetValues(typeof(Rarity));
        Rarity rarity = (Rarity)selection.GetValue(upgradelevel);
        return rarity.ToString(); 
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
        int level = GetRandomUpgradeLevel();

        float PowerMod = StatMultiplierPerLevel[Stats.Power] * PlayerInstance.Instance.PlayerStatMan.BasePower * level;
        float AirConsumptionMod = StatMultiplierPerLevel[Stats.AirConsumption] * PlayerInstance.Instance.PlayerStatMan.BaseAirConsumption * level;

        Dictionary<Stats, float> modifiers = new Dictionary<Stats, float>()
        {
            { Stats.Power , PowerMod },
            { Stats.AirConsumption, AirConsumptionMod }
        };

        return new Upgrade(string.Format("{0} Strong Arm", GetRarity(level)), ArmIcon, ArmUpgradeDescription, BaseArmWorth, modifiers);
    }

    protected Upgrade GenerateLegUpgrade()
    {
        int level = GetRandomUpgradeLevel();

        float MoveSpeedMod = StatMultiplierPerLevel[Stats.MovementSpeed] * PlayerInstance.Instance.PlayerStatMan.BaseMoveSpeed * level;
        float StealthMod = StatMultiplierPerLevel[Stats.Stealth] * PlayerInstance.Instance.PlayerStatMan.BaseStealth * level;
        float TurnSpeedMod = StatMultiplierPerLevel[Stats.TurnSpeed] * PlayerInstance.Instance.PlayerStatMan.BaseTurnSpeed * level;

        Dictionary<Stats, float> modifiers = new Dictionary<Stats, float>()
        {
            { Stats.MovementSpeed , MoveSpeedMod },
            { Stats.Stealth, StealthMod },
            { Stats.TurnSpeed, TurnSpeedMod }
        };//MovementSpeed, stealth, turnSpeed

        return new Upgrade(string.Format("{0} Leg Muscles", GetRarity(level)), LegIcon, LegUpgradeDescription, BaseLegWorth, modifiers);
    }

    protected int GetRandomUpgradeLevel()
    {
        var selection = Enum.GetValues(typeof(Rarity));
        return RandRange(0, selection.Length);
    }

    protected Upgrade GenerateChestUpgrade()
    {
        int level = GetRandomUpgradeLevel();

        float MaxAirMod = StatMultiplierPerLevel[Stats.MaxAir] * PlayerInstance.Instance.PlayerStatMan.BaseMaxAir * level;
        float AirRestoreMod = StatMultiplierPerLevel[Stats.AirRestoration] * PlayerInstance.Instance.PlayerStatMan.BaseAirRestoration * level;
        float MaxHealthMod = StatMultiplierPerLevel[Stats.MaxHealth] * PlayerInstance.Instance.PlayerStatMan.BaseMaxAir * level;

        Dictionary<Stats, float> modifiers = new Dictionary<Stats, float>()
        {
            { Stats.MaxAir , MaxAirMod },
            { Stats.AirRestoration, AirRestoreMod },
            { Stats.MaxHealth, MaxHealthMod }
        };

        return new Upgrade(string.Format("{0} Iron Lungs", GetRarity(level)), TorsoIcon, TorsoUpgradeDescription, BaseTorsoWorth, modifiers);
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
        float totalCost = 0;

        foreach (var modifier in statsModifier)
        {
            totalCost += CostMultiplierPerLevel[modifier.Key]  * modifier.Value / StatMultiplierPerLevel[modifier.Key];
        }

        return (int)(m_appliedUpgrades * totalCost * m_currentUpgradesCost);
    }
}
