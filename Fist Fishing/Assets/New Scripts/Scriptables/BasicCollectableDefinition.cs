using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// base definition for all collectable scriptable objects
/// builds on CollectableDefinition by allowing actual harvesting
/// </summary>
[System.Serializable]
[CreateAssetMenu(fileName = "New Collectables Object", menuName = "Collectables/Collectable Definition")]
public class BasicCollectableDefinition : CollectableDefinition
{
    [SerializeField]
    protected Material m_materialOverride;

    //public float WeightedChance => throw new System.NotImplementedException();

    //public MeshCollider MeshOverRide => throw new System.NotImplementedException();

    public override GameObject Spawn(MeshCollider m)
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
        GameObject o = ObjectPoolManager.Get(m_BasicCollectable, p, Quaternion.identity);
        BasicCollectable c = o.AddComponent<BasicCollectable>();
        c.SetDefinition(this);
        if (m_materialOverride != null)
            o.GetComponent<Renderer>().material = m_materialOverride;
        return o;
    }
}
