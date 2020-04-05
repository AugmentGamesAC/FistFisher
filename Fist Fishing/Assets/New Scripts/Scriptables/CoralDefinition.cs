using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Coral Object", menuName = "Coral/Coral Definition")]
public class CoralDefinition : CollectableDefinition//ScriptableObject, ISpawnable
{
    [SerializeField]
    protected CoralBudDefinition m_coralBudDefinition;
    public CoralBudDefinition CoralBudDefinition => m_coralBudDefinition;

    [SerializeField]
    protected GameObject m_modelOfCoral;
    [SerializeField]
    protected float m_timeBetweenBudSpawns;
    [SerializeField]
    protected Material m_materialOverride;
    [SerializeField]
    protected List<string> m_namedModelComponentsForLocationsToSpawnCoralBudsAt;
    public List<string> NamedModelComponentsForLocationsToSpawnCoralBudsAt => m_namedModelComponentsForLocationsToSpawnCoralBudsAt;

    //public float WeightedChance => throw new System.NotImplementedException();

    //public MeshCollider MeshOverRide => throw new System.NotImplementedException();

    public override GameObject Instatiate(MeshCollider m) 
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
        GameObject o = ObjectPoolManager.Get(m_modelOfCoral, p, Quaternion.identity);
        CoralNew c = o.AddComponent<CoralNew>();
        c.SetDefinition(this);
        c.SetTimeBetweenSpawns(m_timeBetweenBudSpawns);
        if (m_materialOverride != null)
            o.GetComponent<Renderer>().material = m_materialOverride;
        return o;
    }
}
