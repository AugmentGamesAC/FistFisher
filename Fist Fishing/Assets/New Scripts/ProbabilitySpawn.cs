using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class ProbabilitySpawn : ISpawnable
{
    public abstract GameObject Instatiate(MeshCollider m);
    public float m_weightedChance;
    public MeshCollider m_meshOverRide;
    public abstract void Clone();
}


[Serializable]
public class ProbabilitySpawnClutter : ProbabilitySpawn
{
    public GameObject Instatiate(MeshCollider m) { return m_spawnReference.Instatiate(m); }
    [SerializeField]
    protected FishDefintion m_spawnReference;

}
[Serializable]
public class ProbabilitySpawnCollectable : ProbabilitySpawn
{
    public override GameObject Instatiate(MeshCollider m) { return m_spawnReference.Instatiate(m); }
    [SerializeField]
    protected FishDefintion m_spawnReference;

}
[Serializable]
public class ProbabilitySpawnFish : ProbabilitySpawn
{
    public override GameObject Instatiate(MeshCollider m) { return m_spawnReference.Instatiate(m); }
    [SerializeField]
    protected FishDefintion m_spawnReference;

}
