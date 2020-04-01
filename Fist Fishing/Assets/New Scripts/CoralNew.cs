using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class CoralNew : MonoBehaviour, IDyingThing 
{
    public event Death Death;

    [SerializeField]
    protected CoralDefinition m_definitionForThis;
    [SerializeField]
    protected HashSet<int> m_hashForTrackingWhichOfTheseHasBeenUsed;
    [SerializeField]
    protected int m_numberOfbudsDied = 0;
    [SerializeField]
    protected float m_timeBetweenSpawns;
    protected float m_currentTimeToSpawn;
    protected bool m_isThisStillAbleToSpawnbuds = true;


    public bool SetDefinition(CoralDefinition def)
    {
        if (def == null)
            return false;
        m_definitionForThis = def;
        return true;
    }

    protected int SelectSpawnNode()
    {
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

        Vector3 pos = g.transform.position;
        GameObject o = m_definitionForThis.CoralBudDefinition.Instantiate(pos);
        o.GetComponent<IDyingThing>().Death += () => { m_numberOfbudsDied++; };
        return o;
    }

    public void Update()
    {
        if(m_numberOfbudsDied >= m_definitionForThis.NamedModelComponentsForLocationsToSpawnCoralBudsAt.Count)
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
