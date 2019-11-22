using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HarvestableSpawner : MonoBehaviour
{
    public TargetController m_targetController;
    public PondManager m_parentPond;

    public GameObject m_harvestablePrefab;
    public int m_maxSpawnedHavestable = 3;
    public float m_normalNumberOfSecondsToSpawnHarvestable = 5.0f;
    public float m_spawnRadius = 5.0f; // this will be set locations instead.
    public float m_spawnCooldown;

    //buds are null until spawned in a harvestable. use this so we can get a transform from it.
    public GameObject bud1;
    public GameObject bud2;
    public GameObject bud3;

    Dictionary<GameObject, GameObject> m_budGameObjectsOnCoral;

    List<GameObject> m_currentHarvestableSpawned = new List<GameObject>();

    public void DespawnHarvestable()
    {
        //GameObject lastHarvestable = m_currentHarvestableSpawned/*.Last()*/;
        //lastHarvestable.SetActive(false);
        //m_currentHarvestableSpawned.Remove(lastHarvestable);
    }

    public GameObject SpawnHavestable()
    {
        if (!m_budGameObjectsOnCoral.ContainsValue(null))
            return null;

        GameObject newHarvestable = ObjectPoolManager.Get(m_harvestablePrefab); //this should handle object pool creation, and return a fish of given type already set as active
        if (newHarvestable == null)
            return null;
        Harvestable HarvestableScript = newHarvestable.GetComponent<Harvestable>();
        if (HarvestableScript == null)
            return null;
        HarvestableScript.m_spawner = this;
        HarvestableScript.m_targetController = m_targetController;

        LayerMask mask = LayerMask.GetMask("Water");
        //Debug.LogError(mask);

        //check array of GameObject, if one of them is null, add and place harvestable at that location.

        //get first null object in our bud list, give it the spawned object reference.
        foreach (GameObject spawns in m_budGameObjectsOnCoral.Keys)
        {
            //check where null value is, place a newHarvestable there.
            GameObject value;
            m_budGameObjectsOnCoral.TryGetValue(spawns, out value);

            if (value == null)
            {
                //fill slot
                m_budGameObjectsOnCoral[spawns] = newHarvestable;
                m_currentHarvestableSpawned.Add(newHarvestable);

                var myKey = m_budGameObjectsOnCoral.FirstOrDefault(x => x.Value == newHarvestable).Key;
                newHarvestable.gameObject.transform.position = myKey.transform.position;
                break;
            }
        }

        float rot = Random.Range(0.0f, 360.0f);
        Quaternion rotq = Quaternion.Euler(0.0f, rot, 0.0f);
        newHarvestable.transform.rotation = rotq;

        return newHarvestable;
    }


    public void ResetSpawningClock()
    {
        m_spawnCooldown = m_normalNumberOfSecondsToSpawnHarvestable;
    }

    // Start is called before the first frame update
    void Start()
    {
        //all null at the start.
        m_budGameObjectsOnCoral = new Dictionary<GameObject, GameObject> { { bud1, null }, { bud2, null }, { bud3, null } };

        if (m_harvestablePrefab == null)
            GameObject.Destroy(this);

        m_currentHarvestableSpawned.Clear();
        m_spawnCooldown = m_normalNumberOfSecondsToSpawnHarvestable;
    }

    protected void DefaultSpawning()
    {
        m_spawnCooldown -= Time.deltaTime;
        if (m_spawnCooldown <= 0.0f && m_currentHarvestableSpawned.Count <= m_maxSpawnedHavestable)
        {
            SpawnHavestable();
            ResetSpawningClock();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_parentPond == null)
            return;

        switch (m_parentPond.m_behaviour)
        {
            case PondBehaviour.Default:
                DefaultSpawning();
                break;
            default:
                break;
        }

        m_spawnCooldown = Mathf.Clamp(m_spawnCooldown, 0.0f, m_normalNumberOfSecondsToSpawnHarvestable);
    }
}
