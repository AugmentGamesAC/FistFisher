using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDefintion : ScriptableObject, IFishData, IItem
{
    [SerializeField]
    protected float m_damage;
    public float Damage => m_damage;

    [SerializeField]
    protected float m_combatSpeed;
    public float CombatSpeed => m_combatSpeed;
    [SerializeField]
    protected float m_attackRange;
    public float AttackRange => m_attackRange;

    [SerializeField]
    protected FishHealth m_Health;
    public FishHealth Health => m_Health;


    public FishBrain.FishClassification FishClassification => throw new System.NotImplementedException();

    public int StackSize => throw new System.NotImplementedException();

    public int ID => throw new System.NotImplementedException();

    public int WorthInCurrency => throw new System.NotImplementedException();

    public ItemType Type => throw new System.NotImplementedException();

    public string Description => throw new System.NotImplementedException();



    public Sprite Sprite => throw new System.NotImplementedException();
    public Sprite IconDisplay => throw new System.NotImplementedException();
}
