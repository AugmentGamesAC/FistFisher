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
    public Dictionary<FishSpawner, FishArchetype> Spawners { get { return m_spawners; } }


    public int SpawnerCount = 0;
    public float m_radius;


    [SerializeField]
    protected bool m_isBaited;
    public bool IsBaited { get { return m_isBaited; } }

    [SerializeField]
    protected List<GameObject> m_activeBait = new List<GameObject>();
    public List<GameObject> ActiveBait { get { return m_activeBait; } }

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
                m_spawners.Add(f, f.m_fishPrefab.GetComponent<BasicFish>().FishArcheType);
                f.m_parentPond = this;
            }
            i++;
        }
    }

    /// <summary>
    /// informed by bait if it exists in this pond
    /// updates list of active bait and sets bool of if baited
    /// baited does nothing at this point, but will change behaviour later
    /// </summary>
    public void ChangeBaitPresenceInThisPond(GameObject bait, bool alive)
    {
        Bait bobj = bait.GetComponent<Bait>();
        if (bobj == null)
            return;

        if(alive)
        {
            m_activeBait.Add(bait);
        }
        else
        {
            m_activeBait.Remove(bait);
        }

        if (m_activeBait.Count > 0)
            m_isBaited = true;
        else
            m_isBaited = false;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnerCount = m_spawners.Count;


    }
}
