using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

/// <summary>
/// this class stores the data needed to instantiate it as an object from biome manager and then spawn stuff
/// </summary>
[CreateAssetMenu(fileName = "New Biome Object", menuName = "Biome/Biome")]
public class BiomeDefinition : ScriptableObject
{
    #region variables
    protected float m_maximumNumberOfSpawns;
    public float MaxNumberOfSpawns => m_maximumNumberOfSpawns;
    protected float m_clutterCount;
    public float ClutterCount => m_clutterCount;

    [SerializeField]
    protected List<ProbabilitySpawnClutter> m_clutter;
    [SerializeField]
    protected List<ProbabilitySpawnCollectable> m_collectables;
    [SerializeField]
    protected List<ProbabilitySpawnFish> m_aggressiveFish;
    [SerializeField]
    protected List<ProbabilitySpawnFish> m_mehFish;
    [SerializeField]
    protected List<ProbabilitySpawnFish> m_preyFish;


    public List<ProbabilitySpawnClutter> ClutterList => m_clutter;
    public List<ProbabilitySpawnCollectable> CollectablesList => m_collectables;
    public List<ProbabilitySpawnFish> AggressiveFishList => m_aggressiveFish;
    public List<ProbabilitySpawnFish> MehFishList => m_mehFish;
    public List<ProbabilitySpawnFish> PreyFishList => m_preyFish;

    protected string m_name;

    protected Color m_boatMapColour;
    [SerializeField]
    protected float m_timeBetweenSpawns;
    public float TimeBetweenSpawns => m_timeBetweenSpawns;

    protected int m_numberOfThingsCurrentlySpawnedInThisBiome;
    public int NumberOfThingsCurrentlySpawnedInThisBiome => m_numberOfThingsCurrentlySpawnedInThisBiome;
    #endregion variables

    public void Start()
    {
        if (m_clutter.Select(X => X.m_weightedChance).Sum() != 1)
            throw new System.InvalidOperationException("Clutters weightedAverage doesn't sum to 1");
        if (m_collectables.Select(X => X.m_weightedChance).Sum() != 1)
            throw new System.InvalidOperationException("Collectables weightedAverage doesn't sum to 1");
        if (m_aggressiveFish.Select(X => X.m_weightedChance).Sum() != 1)
            throw new System.InvalidOperationException("Aggressive Fish weightedAverage doesn't sum to 1");
        if (m_mehFish.Select(X => X.m_weightedChance).Sum() != 1)
            throw new System.InvalidOperationException("Meh Fish weightedAverage doesn't sum to 1");
        if (m_preyFish.Select(X => X.m_weightedChance).Sum() != 1)
            throw new System.InvalidOperationException("Prey Fish weightedAverage doesn't sum to 1");
    }


}
