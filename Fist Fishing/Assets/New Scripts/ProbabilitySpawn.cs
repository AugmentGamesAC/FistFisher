using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProbabilitySpawn<T,V>: ISpawnable where T:ISpawnable where V: UnityEngine.Object
{
    public GameObject Instatiate(MeshCollider m) { return m_spawnRefence.Instatiate(m); }
    public float m_weightedChance;
    public MeshCollider m_meshOverRide;
    [SerializeField]
    protected T m_spawnRefence;
    //public V Clone( (V) this.meb )
}


[Serializable]
public class ProbabilitySpawnClutter : ProbabilitySpawn<FishDefintion,ProbabilitySpawnClutter>
{
    public override GameObject Instatiate(MeshCollider m)
    {
        return m_spawnReference.Instatiate(m);
    }
    public override void Clone()
    {
        
    }


}
[Serializable]
public class ProbabilitySpawnCollectable : ProbabilitySpawn<FishDefintion, ProbabilitySpawnClutter> { }


[Serializable]
public class ProbabilitySpawnFish : ProbabilitySpawn<FishDefintion, ProbabilitySpawnFish> { }