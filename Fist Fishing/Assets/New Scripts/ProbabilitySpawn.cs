using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class ProbabilitySpawn : ISpawnable
{
    public virtual GameObject Instatiate(MeshCollider m) { return null; }
    public float m_weightedChance;
    public MeshCollider m_meshOverRide;
}


[Serializable]
public class ProbabilitySpawnClutter : ProbabilitySpawn
{
    public override GameObject Instatiate(MeshCollider m) { return m_spawnReference.Instatiate(m); }
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
