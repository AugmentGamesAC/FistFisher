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
    public string Name => m_name;

    protected Color m_boatMapColour;
    [SerializeField]
    protected float m_timeBetweenSpawns;
    public float TimeBetweenSpawns => m_timeBetweenSpawns;


    #endregion variables
    protected BiomeDefinition CloneSelf(BiomeDefinition biome)
    {
        m_clutterCount = biome.m_clutterCount;
        m_maximumNumberOfSpawns = biome.m_maximumNumberOfSpawns;
        m_clutter = biome.m_clutter.Select(x=>x).ToList();
        m_collectables = biome.m_collectables.ToList();
        m_aggressiveFish = biome.m_aggressiveFish.ToList();
        m_mehFish = biome.m_mehFish.ToList();
        m_preyFish = biome.m_preyFish.ToList();
        m_name = biome.m_name;
        m_boatMapColour = biome.m_boatMapColour;
        m_timeBetweenSpawns = biome.m_timeBetweenSpawns; 
        return this;
    }

    public BiomeDefinition CloneSelf(string NewCloneName)
    {
        BiomeDefinition newME = ScriptableObject.CreateInstance<BiomeDefinition>();
        newME.name = NewCloneName;

        return newME.CloneSelf(this);
    }


    public void Start()
    {
        if ((m_clutter.Count > 0) && m_clutter.Select(X => X.m_weightedChance).Sum() != 1)
            throw new System.InvalidOperationException("Clutters weightedAverage doesn't sum to 1");
        if ((m_collectables.Count > 0) && m_collectables.Select(X => X.m_weightedChance).Sum() != 1)
            throw new System.InvalidOperationException("Collectables weightedAverage doesn't sum to 1");
        if ((m_aggressiveFish.Count > 0) && m_aggressiveFish.Select(X => X.m_weightedChance).Sum() != 1)
            throw new System.InvalidOperationException("Aggressive Fish weightedAverage doesn't sum to 1");
        if ((m_mehFish.Count > 0) && m_mehFish.Select(X => X.m_weightedChance).Sum() != 1)
            throw new System.InvalidOperationException("Meh Fish weightedAverage doesn't sum to 1");
        if ((m_preyFish.Count > 0) && m_preyFish.Select(X => X.m_weightedChance).Sum() != 1)
            throw new System.InvalidOperationException("Prey Fish weightedAverage doesn't sum to 1");
    }


}
