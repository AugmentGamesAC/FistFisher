using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// new script to apply to coral
/// this handles bud spawning and coral death when all coral it can spawn is gone
/// </summary>
[System.Serializable]
public class CoralNew : MonoBehaviour, IDyingThing 
{
    public event Death Death;

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

    /// <summary>
    /// increments the number of buds that have been taken from this coral
    /// </summary>
    public void BudDied()
    {
        m_numberOfBudsDied++;
    }



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

    /// <summary>
    /// gets list of positions on coral model to spawn coral at, figures out if it's used
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// spawns a coral bud and sets up its components
    /// </summary>
    /// <returns></returns>
    protected GameObject SpawnBud()
    {
        int i = SelectSpawnNode();
        if (i == -1)
            return null;

        GameObject g = gameObject.transform.Find(m_definitionForThis.NamedModelComponentsForLocationsToSpawnCoralBudsAt[i]).gameObject;
        Vector3 pos = g.transform.position;
        GameObject o = m_definitionForThis.CoralBudDefinition.Instantiate(pos);

        CoralBudNew c = o.GetComponent<CoralBudNew>();
        c.SetParent(this);

        Harvestable h; 
        if (o.gameObject.TryGetComponent<Harvestable>(out h))
            Destroy(h);
        h = o.AddComponent<Harvestable>();
        h.TransferProperties(m_definitionForThis.CoralBudDefinition.ItemData);

        o.GetComponent<IDyingThing>().Death += () => { m_numberOfBudsDied++; };

        return o;
    }

    /// <summary>
    /// countdown for bud spawning, and kills self if all out of buds
    /// </summary>
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
