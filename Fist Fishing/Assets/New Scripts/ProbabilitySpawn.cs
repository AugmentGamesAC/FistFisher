using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IObject<T>
{
    T MemberwiseClone();
}

public static class SpawningTweaks
{
    public static void AdjustForBottom(GameObject g)
    {
        if (g == default)
            return;
        Collider c = g.gameObject.GetComponent<Collider>();
        if (c == default)
            return;
        Bounds b = c.bounds;
        if (b == default)
            return;
        Vector3 o = g.transform.position;
            Vector3 offset = b.extents;
            o.y += offset.y;
            g.transform.position = o;
    }
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


[Serializable]
[CreateAssetMenu(fileName = "New Prob Spawn Clutter Object", menuName = "Clutter/Clutter ProbabilitySpawn")]
public class ProbabilitySpawnClutter : ProbabilitySpawn<ClutterDefinition,ProbabilitySpawnClutter> { }

[Serializable]
[CreateAssetMenu(fileName = "New Prob Spawn Collectable Object", menuName = "Collectables/Collectable ProbabilitySpawn")]
public class ProbabilitySpawnCollectable : ProbabilitySpawn<CollectableDefinition, ProbabilitySpawnCollectable> { }

[Serializable]
[CreateAssetMenu(fileName = "New Prob Spawn Fish Object", menuName = "Fish/Fish ProbabilitySpawn")]
public class ProbabilitySpawnFish : ProbabilitySpawn<FishDefintion, ProbabilitySpawnFish> { }



