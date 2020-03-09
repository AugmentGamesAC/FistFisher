using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class CoralNew : MonoBehaviour, IDyingThing /*ProbabilitySpawnCollectable*/
{
    public event CleanupCall Death;

    //coral needs to have variables for 
    //range(#) of coral buds to spawn before it kills itself
    [SerializeField]
    protected int m_MinimumBudsToSpawn;
    [SerializeField]
    protected int m_MaximumBudsToSpawn;
    protected int m_totalBudsSpawnedInLife;
    protected int m_actualNumberOfBudsThisWillSpawn;

    //places to put buds
    //string list for model component positions?
    [SerializeField]
    protected List<string> m_NamesOfModelComponentsWithTransformsToSpawnAt;

    //models for buds and base
    [SerializeField]
    protected GameObject m_budPrefab;
    [SerializeField]
    protected GameObject m_coralBaseModel;

    //time between spawns
    [SerializeField]
    protected float m_timeBetweenBudSpawns;
    private float m_currentTimeBeforeNextSpawn;

    public void Update()
    {
        if(m_currentTimeBeforeNextSpawn<=0)
        {
            Vector3 pos = GetLocationToSpawnAtGivenString(PickRandomSpawnPositionFromListOfStrings());
            Instantiate(m_budPrefab, pos, transform.rotation, transform);

            m_currentTimeBeforeNextSpawn = m_timeBetweenBudSpawns;
            return;
        }
        m_currentTimeBeforeNextSpawn--;
    }
    public void Start()
    {
        m_currentTimeBeforeNextSpawn = m_timeBetweenBudSpawns;
        m_totalBudsSpawnedInLife = 0;
        if (((m_actualNumberOfBudsThisWillSpawn = NumberOfBudsToSpawnInCoralLifetimeFromRange(m_MinimumBudsToSpawn, m_MaximumBudsToSpawn)) < 1) ||
            (m_NamesOfModelComponentsWithTransformsToSpawnAt==default))
            this.Die();

    }

    //ideally, I'll change this so it doesn't spawn buds on top of each other
    protected string PickRandomSpawnPositionFromListOfStrings()
    {
        return m_NamesOfModelComponentsWithTransformsToSpawnAt[UnityEngine.Random.Range(0, m_NamesOfModelComponentsWithTransformsToSpawnAt.Count)];
    }

    protected Vector3 GetLocationToSpawnAtGivenString(string name)
    {
        return gameObject.transform.Find(name).position;
    }

    protected int NumberOfBudsToSpawnInCoralLifetimeFromRange(int min, int max)
    {
        if (min < 1 || min > max + 1)
            return 0;
        return UnityEngine.Random.Range(min, max+1);
    }
    public void Die()
    {
        this.gameObject.SetActive(false);
        Death?.Invoke();
    }
}
