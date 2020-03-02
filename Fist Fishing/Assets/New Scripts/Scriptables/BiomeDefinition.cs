using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    protected List<ProbabilitySpawn> m_clutter;
    protected List<ProbabilitySpawn> m_collectables;
    protected List<ProbabilitySpawn> m_aggressiveFish;
    protected List<ProbabilitySpawn> m_mehFish;
    protected List<ProbabilitySpawn> m_preyFish;


    public List<ProbabilitySpawn> ClutterList => m_clutter;
    public List<ProbabilitySpawn> CollectablesList => m_collectables;
    public List<ProbabilitySpawn> AggressiveFishList => m_aggressiveFish;
    public List<ProbabilitySpawn> MehFishList => m_mehFish;
    public List<ProbabilitySpawn> PreyFishList => m_preyFish;

    protected GameObject m_biomeMesh;
    public GameObject Mesh => m_biomeMesh;

    protected Vector3 m_worldPosition;
    public Vector3 Position => m_worldPosition;

    protected string m_name;

    protected Color m_boatMapColour;

    protected float m_timeBetweenSpawns;
    private float m_currentSpawnCooldownTimer = 0;


    protected int m_numberOfThingsCurrentlySpawnedInThisBiome;
    public int NumberOfThingsCurrentlySpawnedInThisBiome => m_numberOfThingsCurrentlySpawnedInThisBiome;
    #endregion variables

    public void Start()
    {
        m_numberOfThingsCurrentlySpawnedInThisBiome = 0;
        SpawnClutter(this);
    }

    #region specific spawns

    /// <summary>
    /// on biome manager update, try to spawn stuff
    /// </summary>
    public void SpawnStuff(float dt)
    {
        m_currentSpawnCooldownTimer += dt;

        if (m_currentSpawnCooldownTimer <= m_timeBetweenSpawns)
            return;


        m_currentSpawnCooldownTimer = 0;
        bool spawnsuccess = false;
        do
        {
            if (!CanWeSpawnAnythingInThisBiome(this))
                return;

            if (CoinFlip() == true)
                spawnsuccess=SpawnFish(this);
            else
                spawnsuccess=SpawnCollectable(this);
        } while (spawnsuccess == false);
    }



    /// <summary>
    /// this takes the list of clutter and amount of clutter, and spawns random clutter of that quantity
    /// </summary>
    /// <param name="bd"></param>
    protected void SpawnClutter(BiomeDefinition bd)
    {
        float prob = GetWeightedSum(bd.ClutterList);



        for (int i = 0; i < bd.ClutterCount; i++)
        {
            int objIndex = GetIndexFromWeightedList(bd.ClutterList, prob);
            Transform pos = m_biomeMesh.gameObject.transform;
            pos.position = FindValidPosition(bd);
            pos.position = GetSeafloorPosition(pos.position);

            Instantiate(bd.ClutterList[objIndex].m_spawnReference.Model, pos);
        }
    }
    /// <summary>
    /// this picks a random fish from the biome definition and spawns it in a random valid location
    /// </summary>
    /// <param name="bd"></param>
    /// <returns></returns>
    protected bool SpawnFish(BiomeDefinition bd)
    {
        if (!CanWeSpawnAnythingInThisBiome(bd))
            return false;

        float prob = GetWeightedSum(bd.CollectablesList);
        int objIndex = GetIndexFromWeightedList(bd.CollectablesList, prob);
        Transform pos = m_biomeMesh.gameObject.transform;
        float rad = bd.ClutterList[objIndex].m_spawnReference.Model.GetComponent<Collider>().bounds.size.x / 2.0f;

        RaycastHit hit; //unused

        do
        {
            pos.position = FindValidPosition(bd);

        } while (SpherecastToEnsureItHasRoom(pos.position, rad, out hit));

        Instantiate(bd.ClutterList[objIndex].m_spawnReference.Model, pos);
        bd.m_numberOfThingsCurrentlySpawnedInThisBiome++;
        return true;
    }


    /// <summary>
    /// this picks a random collectable from within the biome definition, picks a random location for it to spawn
    /// </summary>
    /// <param name="bd"></param>
    /// <returns></returns>
    protected bool SpawnCollectable(BiomeDefinition bd)
    {
        if (!CanWeSpawnAnythingInThisBiome(bd))
            return false;

        float prob = GetWeightedSum(bd.CollectablesList);
        int objIndex = GetIndexFromWeightedList(bd.CollectablesList, prob);
        Transform pos = m_biomeMesh.gameObject.transform;
        pos.position = FindValidPosition(bd);
        pos.position = GetSeafloorPosition(pos.position);

        Instantiate(bd.ClutterList[objIndex].m_spawnReference.Model, pos);
        bd.m_numberOfThingsCurrentlySpawnedInThisBiome++;
        return true;
    }


    #endregion


    #region general functionality

    /// <summary>
    /// for 50/50 things
    /// </summary>
    /// <returns></returns>
    public static bool CoinFlip()
    {
        return Convert.ToBoolean(UnityEngine.Random.Range(0, 2));
    }

    private float GetWeightedSum(List<ProbabilitySpawn> list)
    {
        float prob = 0; //total weight
        foreach (ProbabilitySpawn p in list)
        {
            prob += p.m_weightedChance;
        }
        return prob;
    }

    private int GetIndexFromWeightedList(List<ProbabilitySpawn> list, float weightSum)
    {
        float rand = UnityEngine.Random.Range(0, weightSum);
        float objweightcount = 0;
        int objIndex = 0;
        while (objweightcount < rand)
        {
            objweightcount += list[objIndex].m_weightedChance;
            objIndex++;
            if (objIndex >= list.Count)
                return 0;
        }
        return objIndex;
    }


    /// <summary>
    /// this takes the biome, finds out how many things are in it and what the max number of things it should have is, and says if it is allowed to spawn more
    /// </summary>
    /// <param name="bd"></param>
    /// <returns></returns>
    protected bool CanWeSpawnAnythingInThisBiome(BiomeDefinition bd)
    {
        return (bd.m_numberOfThingsCurrentlySpawnedInThisBiome < bd.MaxNumberOfSpawns);
    }

    /// <summary>
    /// this picks a random position within a given mesh
    /// </summary>
    /// <param name="bd"></param>
    /// <returns></returns>
    protected Vector3 FindValidPosition(BiomeDefinition bd)
    {
        Collider c = bd.Mesh.GetComponent<Collider>();
        Bounds b = c.bounds;

        bool validPos = false;
        Vector3 pos = Vector3.zero;

        while (!validPos)
        {
            pos.x = UnityEngine.Random.Range(b.min.x, b.max.x);
            pos.y = UnityEngine.Random.Range(b.min.y, b.max.y);
            pos.z = UnityEngine.Random.Range(b.min.z, b.max.z);
            if (/*b.Contains(pos)*/IsInside(c, pos))
                validPos = true;
        }
        return pos;
    }

    public static bool IsInside(Collider c, Vector3 point)
    {
        Vector3 closest = c.ClosestPoint(point);
        // Because closest=point if point is inside - not clear from docs I feel
        return closest == point;
    }

    /// <summary>
    /// this takes the size of the fish and position it is trying to span in to ensure it has room
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    protected bool SpherecastToEnsureItHasRoom(Vector3 pos, float radius, out RaycastHit hit)
    {
        return Physics.SphereCast(pos, radius, Vector3.down, out hit, Mathf.Infinity, ~LayerMask.GetMask("Player", "Ignore Raycast", "Water"));
    }



    /// <summary>
    /// this takes in a position in world and raycasts down and returns where it hit the floor
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    protected Vector3 GetSeafloorPosition(Vector3 pos)
    {
        RaycastHit hit;
        Physics.Raycast(pos, Vector3.down, out hit, Mathf.Infinity, ~LayerMask.GetMask("Player", "Ignore Raycast", "Water"));
        return hit.point;
    }


    #endregion
}
