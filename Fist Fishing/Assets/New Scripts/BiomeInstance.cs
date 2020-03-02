using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
/// This class exists to handle all the biomes in scene
/// </summary>
[System.Serializable, RequireComponent(typeof(MeshCollider))]
public class BiomeInstance : MonoBehaviour
{
    protected MeshCollider m_MeshCollider;

    [SerializeField]
    protected BiomeDefinition m_myInstructions;

    public void Start()
    {
        m_MeshCollider = GetComponent<MeshCollider>();
        currentCooldown = UnityEngine.Random.Range(0, 0.25f);




    }

    protected float currentCooldown;

    public void Update()
    {
        if (currentCooldown < 0)
        {
            ResolveSpawning();
            currentCooldown = m_myInstructions.TimeBetweenSpawns;
        }
        currentCooldown -= Time.deltaTime;
    }

    protected void ResolveSpawning()
    {
        bool spawnsuccess = false;
        do
        {
            if (!CanWeSpawnAnythingInThisBiome())
                return;

            Func<BiomeDefinition, bool> action = (CoinFlip() == true) ? (Func<BiomeDefinition, bool>)SpawnFish : (Func<BiomeDefinition, bool>)SpawnCollectable;

            action(this);

        } while (spawnsuccess == false);
    }



    /// <summary>
    /// for 50/50 things
    /// </summary>
    /// <returns></returns>
    public static bool CoinFlip()
    {
        return Convert.ToBoolean(UnityEngine.Random.Range(0, 2));
    }


    protected bool SpawnFromWeightedList(List<ProbabilitySpawn> list)
    {
        float rand = UnityEngine.Random.Range(0, 1.0f);
        foreach (ProbabilitySpawn possibbleSpawn in list)
            if ((rand -= possibbleSpawn.m_weightedChance) < 0)
            {
                possibbleSpawn.Instatiate((possibbleSpawn.m_meshOverRide==default)?m_MeshCollider:possibbleSpawn.m_meshOverRide);
                return true;
            }
        return false;
    }


    /// <summary>
    /// this takes the biome, finds out how many things are in it and what the max number of things it should have is, and says if it is allowed to spawn more
    /// </summary>
    /// <param name="bd"></param>
    /// <returns></returns>
    protected bool CanWeSpawnAnythingInThisBiome()
    {
        return (m_myInstructions.NumberOfThingsCurrentlySpawnedInThisBiome < m_myInstructions.MaxNumberOfSpawns);
    }

    /// <summary>
    /// this picks a random position within a given mesh
    /// </summary>
    /// <param name="bd"></param>
    /// <returns></returns>
    protected Vector3 FindValidPosition()
    {
        //Collider c = bd.Mesh.GetComponent<Collider>();
        Bounds b = m_MeshCollider.bounds;

        bool validPos = false;
        Vector3 pos = Vector3.zero;

        while (!validPos)
        {
            pos.x = UnityEngine.Random.Range(b.min.x, b.max.x);
            pos.y = UnityEngine.Random.Range(b.min.y, b.max.y);
            pos.z = UnityEngine.Random.Range(b.min.z, b.max.z);
            if (/*b.Contains(pos)*/IsInside(/*c*/m_MeshCollider, pos))
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










    /// <summary>
    /// this takes the list of clutter and amount of clutter, and spawns random clutter of that quantity
    /// </summary>
    /// <param name="bd"></param>
    protected void SpawnClutter()
    {
        float prob = UnityEngine.Random.Range(0.0f, 1.0f);



        for (int i = 0; i < m_myInstructions.ClutterCount; i++)
        {
            int objIndex = GetIndexFromWeightedList(m_myInstructions.ClutterList, prob);
            Transform pos = m_MeshCollider.gameObject.transform;
            pos.position = FindValidPosition();
            pos.position = GetSeafloorPosition(pos.position);

            Instantiate(m_myInstructions.ClutterList[objIndex].m_spawnReference.Model, pos);

            m_myInstructions.ClutterList[objIndex].Instatiate(m_myInstructions.ClutterList);

        }
    }
    /// <summary>
    /// this picks a random fish from the biome definition and spawns it in a random valid location
    /// </summary>
    /// <param name="bd"></param>
    /// <returns></returns>
    protected bool SpawnFish()
    {
        if (!CanWeSpawnAnythingInThisBiome())
            return false;

        float prob = UnityEngine.Random.Range(0.0f, 1.0f);
        int objIndex = GetIndexFromWeightedList(bd.CollectablesList, prob);
        Transform pos = m_biomeMesh.gameObject.transform;
        float rad = bd.ClutterList[objIndex].m_spawnReference.Model.GetComponent<Collider>().bounds.size.x / 2.0f;

        RaycastHit hit; //unused

        do
        {
            pos.position = FindValidPosition(bd);

        } while (SpherecastToEnsureItHasRoom(pos.position, rad, out hit));

        Instantiate(m_myInstructions.ClutterList[objIndex].m_spawnReference.Model, pos);
        m_myInstructions.NumberOfThingsCurrentlySpawnedInThisBiome++;
        return true;
    }


    /// <summary>
    /// this picks a random collectable from within the biome definition, picks a random location for it to spawn
    /// </summary>
    /// <param name="bd"></param>
    /// <returns></returns>
    protected bool SpawnCollectable()
    {
        if (!CanWeSpawnAnythingInThisBiome())
            return false;

        float prob = UnityEngine.Random.Range(0.0f, 1.0f);
        int objIndex = GetIndexFromWeightedList(m_myInstructions.CollectablesList, prob);
        Transform pos = m_MeshCollider.gameObject.transform;
        pos.position = FindValidPosition();
        pos.position = GetSeafloorPosition(pos.position);

        Instantiate(m_myInstructions.ClutterList[objIndex].m_spawnReference.Model, pos);
        m_myInstructions.m_numberOfThingsCurrentlySpawnedInThisBiome++;
        return true;
    }
}
