using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IFishData 
{
    float Damage { get; }
    float CombatSpeed { get;  }
    float AttackRange { get; }

    float MaxHealth { get;}

    IItem Item { get; }

    Sprite IconDisplay { get; }

    FishBrain.FishClassification FishClassification { get; }
}

public interface IPlayerData
{
    float AttackRange { get; }

    PlayerHealth Health { get; }

    OxygenTracker Oxygen { get; }

    ImageTracker IconDisplay { get; }

    FloatTracker Clams { get; }

    CombatManager CM { get; }
}