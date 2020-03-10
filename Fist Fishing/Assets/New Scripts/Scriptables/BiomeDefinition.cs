﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

/// <summary>
/// this class stores the data needed to instantiate it as an object from biome manager and then spawn stuff
/// </summary>
[CreateAssetMenu(fileName = "New Biome Object", menuName = "Biome/Biome Definition")]
public class BiomeDefinition : ScriptableObject
{
    #region variables
    [SerializeField]
    protected float m_maximumNumberOfSpawns;
    public float MaxNumberOfSpawns => m_maximumNumberOfSpawns;
    [SerializeField]
    protected float m_amountOfClutterToSpawn;
    public float AmountOfClutterToSpawn => m_amountOfClutterToSpawn;

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
    [SerializeField]
    protected string m_name;
    public string Name => m_name;

    protected Color m_boatMapColour;
    [SerializeField]
    protected float m_timeBetweenSpawns;
    public float TimeBetweenSpawns => m_timeBetweenSpawns;


    #endregion variables
    protected BiomeDefinition CloneSelf(string NewCloneName, BiomeDefinition biome)
    {
        m_clutter = biome.m_clutter.Select(x=>x.MemberwiseClone()).ToList();
        m_collectables = biome.m_collectables.Select(x => x.MemberwiseClone()).ToList();
        m_aggressiveFish = biome.m_aggressiveFish.Select(x => x.MemberwiseClone()).ToList();
        m_mehFish = biome.m_mehFish.Select(x => x.MemberwiseClone()).ToList();
        m_preyFish = biome.m_preyFish.Select(x => x.MemberwiseClone()).ToList();
        m_name = NewCloneName;
        return this;
    }

    public BiomeDefinition CloneSelf(string NewCloneName)
    {
        BiomeDefinition newME = Instantiate(this);
        newME.name = NewCloneName;

        return newME.CloneSelf(NewCloneName,this);
    }


    public void Start()
    {
        if ((m_clutter.Count > 0) && m_clutter.Select(X => X.WeightedChance).Sum() != 1)
            throw new System.InvalidOperationException("Clutters weightedAverage doesn't sum to 1");
        if ((m_collectables.Count > 0) && m_collectables.Select(X => X.WeightedChance).Sum() != 1)
            throw new System.InvalidOperationException("Collectables weightedAverage doesn't sum to 1");
        if ((m_aggressiveFish.Count > 0) && m_aggressiveFish.Select(X => X.WeightedChance).Sum() != 1)
            throw new System.InvalidOperationException("Aggressive Fish weightedAverage doesn't sum to 1");
        if ((m_mehFish.Count > 0) && m_mehFish.Select(X => X.WeightedChance).Sum() != 1)
            throw new System.InvalidOperationException("Meh Fish weightedAverage doesn't sum to 1");
        if ((m_preyFish.Count > 0) && m_preyFish.Select(X => X.WeightedChance).Sum() != 1)
            throw new System.InvalidOperationException("Prey Fish weightedAverage doesn't sum to 1");
    }


}
