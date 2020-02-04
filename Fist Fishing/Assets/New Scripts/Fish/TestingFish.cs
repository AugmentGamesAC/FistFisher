using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestingFish : IFishData
{


    public float Damage => 20.0f;

    public float CombatSpeed => 4.0f;

    public float AttackRange => 6.0f;

    [SerializeField]
    protected FishHealth _Health = new FishHealth();
    public FishHealth Health => _Health;


    [SerializeField]
    protected Sprite _FishSprite;
    public Sprite Sprite => _FishSprite;

    [SerializeField]
    protected FishBrain.FishClassification _FishClassification;

    public TestingFish(FishHealth Health, Sprite FishSprite, FishBrain.FishClassification FishClassification)
    {
        _Health = Health;
        _FishSprite = FishSprite;
        _FishClassification = FishClassification;
    }

    public TestingFish() { }

    public FishBrain.FishClassification FishClassification => _FishClassification;

    
}
