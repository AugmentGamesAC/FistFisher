using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollectableDefinition : ScriptableObject, ISpawnable
{
    public Type SpawnableType => m_BasicCollectable.GetType();
    public String SpawnableName => m_BasicCollectable.name;
    #region ModelReferences
    /*[SerializeField]
    protected Mesh m_BaseModelReference;*/
    [SerializeField]
    protected GameObject m_BasicCollectable;

    [SerializeField]
    protected AItem m_collectableItemData;
    public AItem ItemData => m_collectableItemData;

    public float WeightedChance => throw new System.NotImplementedException();

    public MeshCollider MeshOverRide => throw new System.NotImplementedException();

    public GameObject Instantiate()
    {
        throw new System.NotImplementedException();
    }

    public GameObject Instantiate(Vector3 position, Quaternion rotation)
    {
        throw new System.NotImplementedException();
    }
    #endregion
    public virtual GameObject Spawn(MeshCollider m)
    {
        if (m == null)
            return null;

        Vector3 pos = BiomeInstance.FindValidPosition(m);
        Vector3 p = BiomeInstance.GetSeafloorPosition(pos);
        if (p == Vector3.zero)
        {
            pos.y = m.bounds.max.y - m.bounds.extents.y;
            p = BiomeInstance.GetSeafloorPosition(pos);
        }

        return ObjectPoolManager.Get(m_BasicCollectable, p, Quaternion.identity);
    }

}
