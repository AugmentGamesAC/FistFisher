using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Clutter Object", menuName = "Clutter/Clutter Definition")]
public class ClutterDefinition : ScriptableObject, ISpawnable
{
    public float WeightedChance => throw new System.NotImplementedException();
    public MeshCollider MeshOverRide => throw new System.NotImplementedException();
    public Type SpawnableType => m_BasicClutter.GetType();
    public String SpawnableName => m_BasicClutter.name;

    #region ModelReferences
    /*[SerializeField]
    protected Mesh m_BaseModelReference;*/
    [SerializeField]
    protected GameObject m_BasicClutter;
    #endregion

    public GameObject Instantiate()
    {
        throw new System.NotImplementedException();
    }

    public GameObject Instantiate(Vector3 position, Quaternion rotation)
    {
        throw new System.NotImplementedException();
    }


    public GameObject Spawn(MeshCollider m)
    {
        if (m == null)
            return null;

        Vector3 pos = BiomeInstance.FindValidPosition(m);
        //Debug.Log(pos);
        Vector3 p = BiomeInstance.GetSeafloorPosition(pos);
        if (p == Vector3.zero)
        {
            pos.y = m.bounds.max.y;
            p = BiomeInstance.GetSeafloorPosition(pos);
        }
        //Debug.Log(p);
        return ObjectPoolManager.Get(m_BasicClutter, p, Quaternion.identity);
    }
}
