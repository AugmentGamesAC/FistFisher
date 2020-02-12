using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this class stores the data needed to instantiate it as an object from biome manager and then spawn stuff
/// </summary>
[CreateAssetMenu(fileName = "New Biome Object", menuName = "Biome/Biome")]
public class BiomeDefinition : ScriptableObject
{
    protected float m_maximumNumberOfSpawns;
    protected float m_clutterCount;

    protected List<ProbabilitySpawn> m_clutter;
    protected List<ProbabilitySpawn> m_collectables;
    protected List<ProbabilitySpawn> m_aggressiveFish;
    protected List<ProbabilitySpawn> m_mehFish;
    protected List<ProbabilitySpawn> m_preyFish;

    protected GameObject m_biomeMesh;
    public GameObject Mesh => m_biomeMesh;
    protected Vector3 m_worldPosition;
    protected string m_name;
    protected Color m_boatMapColour;

}
