using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestingFish : IFishData
{
    [SerializeField]
    protected float damage = 20.0f;
    public float Damage => damage;

    public float CombatSpeed => 4.0f;

    public float AttackRange => 6.0f;


    


    [SerializeField]
    protected Sprite _FishSprite;
    public Sprite IconDisplay => _FishSprite;

    [SerializeField]
    protected FishBrain.FishClassification _FishClassification;

    public TestingFish() { }

    public FishBrain.FishClassification FishClassification => _FishClassification;

    public IItem Item => throw new System.NotImplementedException();

    public float MaxHealth => 10;
}
