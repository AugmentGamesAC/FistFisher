using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class ProbabilitySpawn : ISpawnable
{
    public virtual GameObject Instatiate(MeshCollider m) { return null; }
    public virtual bool Despawn() { return false; }
    public float m_weightedChance;
    public MeshCollider m_meshOverRide;
}


[Serializable]
public class ProbabilitySpawnClutter : ProbabilitySpawn
{
    public override GameObject Instatiate(MeshCollider m) { return m_spawnReference.Instatiate(m); }
    public override bool Despawn() { return m_spawnReference.Despawn(); }
    [SerializeField]
    protected ClutterDefinition m_spawnReference;

}
[Serializable]
public class ProbabilitySpawnCollectable : ProbabilitySpawn
{
    public override GameObject Instatiate(MeshCollider m) { return m_spawnReference.Instatiate(m); }
    public override bool Despawn() { return m_spawnReference.Despawn(); }
    [SerializeField]
    protected CollectableDefinition m_spawnReference;
}
[Serializable]
public class ProbabilitySpawnFish : ProbabilitySpawn
{
    public override GameObject Instatiate(MeshCollider m) { return m_spawnReference.Instatiate(m); }
    public override bool Despawn() { return m_spawnReference.Despawn(); }
    [SerializeField]
    protected FishDefintion m_spawnReference;
}
