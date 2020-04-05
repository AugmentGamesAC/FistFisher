using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IObject<T>
{
    T MemberwiseClone();
}

public class ProbabilitySpawn<T,V>: ScriptableObject, /*UnityEngine.Object,*/ ISpawnable, IObject<V> where T:ISpawnable where V: IObject<V>,ISpawnable
{
    public GameObject Instatiate(MeshCollider m) { return m_spawnReference.Instatiate(m); }

    public new V MemberwiseClone() => (V)base.MemberwiseClone();
    [SerializeField]
    protected float m_weightedChance;
    public float WeightedChance => m_weightedChance;
    [SerializeField]
    protected MeshCollider m_meshOverRide;
    public MeshCollider MeshOverRide => m_meshOverRide;
    [SerializeField]
    protected T m_spawnReference;

    [SerializeField]
    protected bool m_spawnFromBottom;
}