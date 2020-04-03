﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Clutter Object", menuName = "Clutter/Clutter Definition")]
public class ClutterDefinition : ScriptableObject, ISpawnable
{
    #region ModelReferences
    /*[SerializeField]
    protected Mesh m_BaseModelReference;*/
    [SerializeField]
    protected GameObject m_BasicClutter;

    public float WeightedChance => throw new System.NotImplementedException();

    public MeshCollider MeshOverRide => throw new System.NotImplementedException();
    #endregion
    public GameObject Instatiate(MeshCollider m)
    {
        if (m == null)
            return null;

        Vector3 pos = BiomeInstance.FindValidPosition(m);
        Vector3 p = BiomeInstance.GetSeafloorPosition(pos);
        if (p == Vector3.zero)
        {
            pos.y = m.bounds.max.y;
            p = BiomeInstance.GetSeafloorPosition(pos);
        }

        return ObjectPoolManager.Get(m_BasicClutter, p, Quaternion.identity);
    }
}