using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


/// <summary>
/// This class exists to handle all the biomes in scene
/// </summary>
[System.Serializable, RequireComponent(typeof(MeshCollider))]
public class BiomeInstance : MonoBehaviour
{
    protected MeshCollider m_MeshCollider;

    [SerializeField]
    protected BiomeDefinition m_myInstructions;

    protected Dictionary<IEnumerable<ProbabilitySpawn>, int> m_memberCount;

    protected IEnumerable<ProbabilitySpawn> m_aggresiveProbSpawn;
    protected IEnumerable<ProbabilitySpawn> m_mehProbSpawn;
    protected IEnumerable<ProbabilitySpawn> m_preyProbSpawn;
    protected IEnumerable<ProbabilitySpawn> m_collectablesProbSpawn;

    public void Start()
    {
        m_MeshCollider = GetComponent<MeshCollider>();
        currentCooldown = UnityEngine.Random.Range(0, 0.25f);

        SpawnClutter();

        m_memberCount = new Dictionary<IEnumerable<ProbabilitySpawn>, int>()
        {
            {m_aggresiveProbSpawn   = m_myInstructions.AggressiveFishList.Cast<ProbabilitySpawn>() , 0},
            {m_mehProbSpawn         = m_myInstructions.MehFishList.Cast<ProbabilitySpawn>()        , 0},
            {m_preyProbSpawn        = m_myInstructions.PreyFishList.Cast<ProbabilitySpawn>()       , 0},
            {m_collectablesProbSpawn= m_myInstructions.CollectablesList.Cast<ProbabilitySpawn>()   , 0}
        };
        
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
        if (m_memberCount == default)
            return;
        if (m_memberCount.Values.Sum() >= m_myInstructions.MaxNumberOfSpawns)
            return;
        if (m_memberCount[m_collectablesProbSpawn] <= m_memberCount[m_preyProbSpawn])
        {
            m_memberCount[m_collectablesProbSpawn] += (SpawnFromWeightedList(m_collectablesProbSpawn)) ? 1 : 0;
            return;
        }
        if (m_memberCount[m_preyProbSpawn] < m_memberCount[m_mehProbSpawn])
        {
            m_memberCount[m_preyProbSpawn] += (SpawnFromWeightedList(m_collectablesProbSpawn)) ? 1 : 0;
            return;
        }
        if (m_memberCount[m_mehProbSpawn] < m_memberCount[m_aggresiveProbSpawn])
        {
            m_memberCount[m_mehProbSpawn] += (SpawnFromWeightedList(m_collectablesProbSpawn)) ? 1 : 0;
            return;
        }
        if (m_memberCount[m_aggresiveProbSpawn] < m_memberCount[m_mehProbSpawn] )
            m_memberCount[m_aggresiveProbSpawn] += (SpawnFromWeightedList(m_collectablesProbSpawn)) ? 1 : 0;
    }

    protected bool SpawnFromWeightedList(IEnumerable<ProbabilitySpawn> list)
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
    /// this takes the list of clutter and amount of clutter, and spawns random clutter of that quantity
    /// </summary>
    /// <param name="bd"></param>
    protected void SpawnClutter()
    {
        if (m_collectablesProbSpawn.Count() == 0)
            return;
        
        int cluttercount = (SpawnFromWeightedList(m_collectablesProbSpawn)) ? 1 : 0;

        while (cluttercount < m_myInstructions.ClutterCount)
            cluttercount += (SpawnFromWeightedList(m_collectablesProbSpawn)) ? 1 : 0;
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
            if (IsInside(m_MeshCollider, pos))
                validPos = true;
        }
        return pos;
    }

    public static bool IsInside(Collider c, Vector3 point)
    {
        // Because ClosestPoint(point)=point if point is inside - not clear from docs I feel
        return c.ClosestPoint(point) == point;
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

}
