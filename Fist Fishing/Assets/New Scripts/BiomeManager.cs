using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this needs to be removed when it's made
/// </summary>
public class BiomeDefinition
{

}

/// <summary>
/// This class exists to handle all the biomes in scene
/// </summary>
public class BiomeManager : MonoBehaviour
{

    #region singletonification
    private static BiomeManager Instance;
    private static void hasInstance()
    {
        if (Instance == default)
            throw new System.InvalidOperationException("ALInput not Initilized");
    }
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject); //unity is stupid. Needs this to not implode
        Instance = this;
    }
    #endregion

    protected List<BiomeDefinition> Biomes;


    /// <summary>
    /// this calls the spawn clutter in each valid biome provided in the list of biomedefinitions
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// this loops through all valid biomes, checks if it can spawn in them, picks fish or collectable, then spawns 
    /// </summary>
    void Update()
    {
        
    }
    /// <summary>
    /// this takes the list of clutter and amount of clutter, and spawns random clutter of that quantity
    /// </summary>
    /// <param name="bd"></param>
    protected void SpawnClutter(BiomeDefinition bd)
    {
        throw new System.NotImplementedException("Not Implemented");
    }
    /// <summary>
    /// this takes the biome, finds out how many things are in it and what the max number of things it should have is, and says if it is allowed to spawn more
    /// </summary>
    /// <param name="bd"></param>
    /// <returns></returns>
    protected bool CanWeSpawnAnythingInThisBiome(BiomeDefinition bd)
    {
        throw new System.NotImplementedException("Not Implemented");

    }
    /// <summary>
    /// this picks a random fish from the biome definition and spawns it in a random valid location
    /// </summary>
    /// <param name="bd"></param>
    /// <returns></returns>
    protected bool SpawnFish(BiomeDefinition bd)
    {
        throw new System.NotImplementedException("Not Implemented");

    }
    /// <summary>
    /// this picks a random collectable from within the biome definition, picks a random location for it to spawn
    /// </summary>
    /// <param name="bd"></param>
    /// <returns></returns>
    protected bool SpawnCollectable(BiomeDefinition bd)
    {
        throw new System.NotImplementedException("Not Implemented");

    }
    /// <summary>
    /// this picks a random position within a given mesh
    /// </summary>
    /// <param name="bd"></param>
    /// <returns></returns>
    protected Vector3 FindValidPosition(BiomeDefinition bd)
    {
        throw new System.NotImplementedException("Not Implemented");

    }
    /// <summary>
    /// this takes the size of the fist and position it is trying to span in to ensure it has room
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    protected bool SpherecastToEnsureItHasRoom(Vector3 pos, float radius)
    {
        throw new System.NotImplementedException("Not Implemented");

    }

    /// <summary>
    /// this takes in a position in world and raycasts down and returns where it hit the floor
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    protected Vector3 GetSeafloorPosition(Vector3 pos)
    {
        throw new System.NotImplementedException("Not Implemented");

    }
}
