using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCombatFish : FishCombatInfo
{
    [SerializeField]
    public static FishHealth health;
    public static Sprite sprite;
    public static FishBrain.FishClassification fishType;
   
    public new IFishData m_fishData = new TestingFish(health,sprite,fishType);


}
