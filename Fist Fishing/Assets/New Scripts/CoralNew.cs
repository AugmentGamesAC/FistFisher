using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class CoralNew : MonoBehaviour, IDyingThing 
{
    public event CleanupCall Death;

    [SerializeField]
    protected CoralDefinition m_definitionForThis;
    [SerializeField]
    protected HashSet<int> m_hashForTrackingWhichOfTheseHasBeenUsed = new HashSet<int>();
    [SerializeField]
    protected int m_numberOfBudsDied = 0;
    [SerializeField]
    protected float m_timeBetweenSpawns;
    protected float m_currentTimeToSpawn;
    protected bool m_isThisStillAbleToSpawnbuds = true;

    public void SetTimeBetweenSpawns(float t)
    {
        m_timeBetweenSpawns = Mathf.Clamp(t, 0, Mathf.Infinity);
    }

    public bool SetDefinition(CoralDefinition def)
    {
        if (def == null)
            return false;
        m_definitionForThis = def;
        return true;
    }

    protected int SelectSpawnNode()
    {
        /*Debug.Log(m_hashForTrackingWhichOfTheseHasBeenUsed);
        Debug.Log(m_hashForTrackingWhichOfTheseHasBeenUsed.Count);
        Debug.Log(m_definitionForThis);
        Debug.Log(m_definitionForThis.NamedModelComponentsForLocationsToSpawnCoralBudsAt);
        Debug.Log(m_definitionForThis.NamedModelComponentsForLocationsToSpawnCoralBudsAt.Count);*/
        if (m_hashForTrackingWhichOfTheseHasBeenUsed.Count >= m_definitionForThis.NamedModelComponentsForLocationsToSpawnCoralBudsAt.Count)
            return -1;
        int returnval;
        do
        {
            returnval = UnityEngine.Random.Range(0, m_definitionForThis.NamedModelComponentsForLocationsToSpawnCoralBudsAt.Count);
        } while (m_hashForTrackingWhichOfTheseHasBeenUsed.Contains(returnval));

        m_hashForTrackingWhichOfTheseHasBeenUsed.Add(returnval);

        return returnval;
    }

    protected GameObject SpawnBud()
    {
        int i = SelectSpawnNode();
        if (i == -1)
            return null;

        GameObject g = gameObject.transform.Find(m_definitionForThis.NamedModelComponentsForLocationsToSpawnCoralBudsAt[i]).gameObject;
        //Debug.Log(g);
        Vector3 pos = g.transform.position;
        /*Debug.Log(m_definitionForThis);
        Debug.Log(m_definitionForThis.CoralBudDefinition);
        Debug.Log(m_definitionForThis.CoralBudDefinition.ItemData);*/
        GameObject o = m_definitionForThis.CoralBudDefinition.Instantiate(pos);

        Harvestable h; 
        if (o.gameObject.TryGetComponent<Harvestable>(out h))
            Destroy(h);
        h = o.AddComponent<Harvestable>();
        h.TransferProperties(m_definitionForThis.CoralBudDefinition.ItemData);

        o.GetComponent<IDyingThing>().Death += () => { m_numberOfBudsDied++; };

        return o;
    }

    public void Update()
    {
        if(m_numberOfBudsDied >= m_definitionForThis.NamedModelComponentsForLocationsToSpawnCoralBudsAt.Count)
        {
            Die();
            return;//just for safety
        }

        if (!m_isThisStillAbleToSpawnbuds)
            return;

        if(m_currentTimeToSpawn <= 0)
        {
            m_currentTimeToSpawn = m_timeBetweenSpawns;
            GameObject b = SpawnBud();
            if (b == null)
                m_isThisStillAbleToSpawnbuds = false;
        }
        m_currentTimeToSpawn -= Time.deltaTime;
    }
    public void Start()
    {
        m_currentTimeToSpawn = m_timeBetweenSpawns;
    }

    public void Die()
    {
        this.gameObject.SetActive(false);
        Death?.Invoke();
    }
}
