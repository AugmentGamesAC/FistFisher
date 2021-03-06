﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// base definition for all fish scriptable objects
/// </summary>
[CreateAssetMenu(fileName = "New Fish Object", menuName = "Fish/Fish Definition")]
public class FishDefintion : ScriptableObject, IFishData, IItem, ISpawnable
{
    public Type SpawnableType => typeof(FishDefintion);
    public String SpawnableName => m_name;
    #region IFishData
    public IItem Item => this;
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
    protected float m_maxHealth;
    public float MaxHealth => m_maxHealth;

    [SerializeField]
    protected FishBrain.FishClassification m_fishClassification;
    public FishBrain.FishClassification FishClassification => m_fishClassification;
    #endregion
    #region IFishData IItem Overlap
    [SerializeField]
    protected Sprite m_IconDisplay;
    public Sprite IconDisplay => m_IconDisplay;
    #endregion
    #region IItem

    [SerializeField]
    protected int m_stackSize;
    public int StackSize => m_stackSize;
    [SerializeField]
    protected int m_iD;
    public int ID => m_iD;
    [SerializeField]
    protected int m_worthInCurrency;
    public int WorthInCurrency => m_worthInCurrency;
    [SerializeField]
    protected ItemType m_Type;
    public ItemType Type => m_Type;
    [SerializeField]
    protected string m_Description;
    public string Description => m_Description;
    [SerializeField]
    protected string m_name;
    public string Name => m_name;

    public float WeightedChance => throw new System.NotImplementedException();

    public MeshCollider MeshOverRide => throw new System.NotImplementedException();

    public bool CanMerge(IItem newItem)
    {
        return false;
    }
    #endregion

    #region ModelReferences
    /*[SerializeField]
    protected Mesh m_BaseModelReference;*/
    [SerializeField]
    protected GameObject m_BasicFish;
    [SerializeField]
    protected Material m_Skin;
    public Material Skin => m_Skin;

    //private GameObject m_thisObject = null;
    #endregion


    public Vector3 FindNewSpot(MeshCollider m)
    {
        Vector3 pos = m_BasicFish.transform.position;
        var myRef = m_BasicFish.GetComponentInChildren<SkinnedMeshRenderer>();
        float rad = myRef.bounds.size.x / 2.0f;
        RaycastHit hit; //ignored as FindValidPosition doesn't allow for overrides yet

        do
        {
            pos = BiomeInstance.FindValidPosition(m);
        } while (!BiomeInstance.SpherecastToEnsureItHasRoom(pos, rad, out hit));
        return pos;
    }


    public GameObject Spawn(MeshCollider m)
    {
        if (m == null)
            return null;

        Vector3 pos = FindNewSpot(m);
        BasicFish2 coreFish = ObjectPoolManager.Get(m_BasicFish, pos, Quaternion.identity).GetComponent<BasicFish2>();
        if (coreFish == null)
            Debug.LogError(pos + " " + m + " " + m_BasicFish);
        coreFish.Init(this, m);

        return coreFish.gameObject;
    }

    public void ConfigFish(FishBrain.FishClassification classification)
    {
        m_fishClassification = classification;
        m_Type = ItemType.Fish;
        m_Description = "Funny not Found";
        m_worthInCurrency = 100;
        m_combatSpeed = 4;
    }

    public bool ResolveDropCase(ISlotData slot, ISlotData oldSlot)
    {
        if (!(slot.Manager is ShopSlotManager))
            return false;
        oldSlot.RemoveItem();
        slot.RemoveItem();

        return true;
    }

    public GameObject Instantiate(MeshCollider m)
    {
        GameObject returnVal;

        returnVal = Instantiate(m_BasicFish);

        AttachParts(returnVal,m);
        return returnVal;
    }

    public GameObject Instantiate(MeshCollider m,Vector3 position, Quaternion rotation)
    {
        GameObject returnVal;

        returnVal = Instantiate(m_BasicFish, position, rotation);

        AttachParts(returnVal,m);
        return returnVal;
    }

    protected void AttachParts(GameObject newInstance, MeshCollider m)
    {

    }
}
