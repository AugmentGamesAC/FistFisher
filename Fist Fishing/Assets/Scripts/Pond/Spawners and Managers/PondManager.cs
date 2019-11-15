using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this will eventually tell us how spawners should behave or if the pond has special behaviours
/// </summary>
public enum PondBehaviour
{
    Default,
}

/// <summary>
/// mamanges all contained spawners
/// </summary>
public class PondManager : MonoBehaviour
{
    public PondBehaviour m_behaviour = PondBehaviour.Default;

    private Dictionary<FishSpawner, FishArchetype> m_spawners = new Dictionary<FishSpawner, FishArchetype>();
    public int SpawnerCount = 0;
    public float m_radius;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    /// <summary>
    /// gets all relevent managable components
    /// </summary>
    private void Init()
    {
        m_radius = gameObject.GetComponent<SphereCollider>().radius; //hacky way to get radius for sphere overlap as opposed to collisions
        
        Collider[] hits = Physics.OverlapSphere(gameObject.transform.position, m_radius);
        int i = 0;
        while (i < hits.Length)
        {
            FishSpawner f = hits[i].GetComponent<FishSpawner>();
            if (f!=null)
            {
                m_spawners.Add(f, f.m_fishPrefab.GetComponent<BasicFish>().FishType);
                f.m_parentPond = this;
            }
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SpawnerCount = m_spawners.Count;

        if (Input.GetKeyDown(KeyCode.G)) //force-spawn fish. remove this.
        {
            foreach(var g in m_spawners)
            {
                g.Key.SpawnFish();
            }
        }
    }
}
