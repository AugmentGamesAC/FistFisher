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
    public GameObject Spawn(MeshCollider m) { return m_spawnReference.Spawn(m); }
    public GameObject Instantiate(MeshCollider m) { return m_spawnReference.Instantiate(m); }
    public GameObject Instantiate(MeshCollider m, Vector3 position, Quaternion rotation) { return m_spawnReference.Instantiate(m,position,rotation); }

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