using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FishSpawner : MonoBehaviour
{

    public PondManager m_parentPond;

    public GameObject m_fishPrefab;
    public float m_normalMaxNumberOfFishForThisSpawnerToSpawn = 10.0f;
    public float m_normalNumberOfSecondsToSpawnFish = 5.0f;
    public float m_spawnRadius = 5.0f;
    public float m_spawnCooldown;

    List<GameObject> m_currentFishSpawned = new List<GameObject>();

    public void DespawnFish()
    {
        GameObject lastFish = m_currentFishSpawned.Last();
        lastFish.SetActive(false);
        m_currentFishSpawned.Remove(lastFish);
    }

    public GameObject SpawnFish()
    {
        GameObject newFish = ObjectPoolManager.Get(m_fishPrefab); //this should handle object pool creation, and return a fish of given type already set as active
        if (newFish == null)
            return null;
        BasicFish fishScript = newFish.GetComponent<BasicFish>();
        if (fishScript == null)
            return null;

        LayerMask mask = LayerMask.GetMask("Water");
        //Debug.LogError(mask);

        m_currentFishSpawned.Add(newFish);
        do
        {
            Vector3 pos = (Random.insideUnitSphere * m_spawnRadius) + transform.position;
            newFish.transform.position = pos;
        } while (!Physics.CheckSphere(newFish.transform.position, fishScript.m_personalSpaceRadius, mask));

            float rot = Random.Range(0.0f, 360.0f);
        Quaternion rotq = Quaternion.Euler(0.0f, rot, 0.0f);
        newFish.transform.rotation = rotq;

        return newFish;
    }


    public void ResetSpawningClock()
    {
        m_spawnCooldown = m_normalNumberOfSecondsToSpawnFish;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (m_fishPrefab == null)
            GameObject.Destroy(this);
        m_currentFishSpawned.Clear();
        m_spawnCooldown = m_normalNumberOfSecondsToSpawnFish;
    }

    protected void DefaultSpawning()
    {
        m_spawnCooldown -= Time.deltaTime;
        if(m_spawnCooldown<=0.0f && m_currentFishSpawned.Count <= m_normalMaxNumberOfFishForThisSpawnerToSpawn)
        {
            SpawnFish();
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

        m_spawnCooldown = Mathf.Clamp(m_spawnCooldown, 0.0f, m_normalNumberOfSecondsToSpawnFish);
    }





}
