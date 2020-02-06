using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IFishData 
{
    float Damage { get; }
    float CombatSpeed { get;  }
    float AttackRange { get; }

    FishHealth Health { get;}

    IItem Item { get; }

    Sprite IconDisplay { get; }

    FishBrain.FishClassification FishClassification { get; }
}
