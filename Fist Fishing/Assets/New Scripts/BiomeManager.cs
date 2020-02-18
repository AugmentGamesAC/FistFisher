using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class exists to handle all the biomes in scene
/// </summary>
[System.Serializable]
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

    [SerializeField]
    protected List<BiomeDefinition> m_biomes;


    /// <summary>
    /// this calls the spawn clutter in each valid biome provided in the list of biomedefinitions
    /// </summary>
    void Start()
    {
        if(m_biomes.Count<=0)
        {
            Destroy(this);
        }
        foreach(BiomeDefinition bd in m_biomes)
        {
            bd.m_numberOfThingsCurrentlySpawnedInThisBiome = 0;
            SpawnClutter(bd);
        }
    }

    /// <summary>
    /// this loops through all valid biomes, checks if it can spawn in them, picks fish or collectable, then spawns 
    /// </summary>
    void Update()
    {
        
    }

    private float GetWeghtedSum(List<ProbabilitySpawn> list)
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
    /// this takes the list of clutter and amount of clutter, and spawns random clutter of that quantity
    /// </summary>
    /// <param name="bd"></param>
    protected void SpawnClutter(BiomeDefinition bd)
    {
        float prob = GetWeghtedSum(bd.ClutterList);



        for (int i = 0; i < bd.ClutterCount; i++)
        {
            
            GameObject obj;
            int objIndex = GetIndexFromWeightedList(bd.ClutterList, prob);
            Transform pos = gameObject.transform;
            pos.position = FindValidPosition(bd);
            pos.position = GetSeafloorPosition(pos.position);

            obj = Instantiate(bd.ClutterList[objIndex].m_spawnReference.Model, pos);
        }
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
        if (!CanWeSpawnAnythingInThisBiome(bd))
            return false;
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

        //this is currently using "b" - a bounding box. does not yet account for more complex shapes
        //https://answers.unity.com/questions/163864/test-if-point-is-in-collider.html?_ga=2.115196111.95679741.1581358981-1866372932.1581358981 
        //is something to look into, but is a low priority at this point
        while (!validPos)
        {
            pos.x = Random.Range(b.min.x, b.max.x);
            pos.y = Random.Range(b.min.y, b.max.y);
            pos.z = Random.Range(b.min.z, b.max.z);
            if (b.Contains(pos))
                validPos = true;
        }
        return pos;
    }


    /// <summary>
    /// this takes the size of the fist and position it is trying to span in to ensure it has room
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    protected bool SpherecastToEnsureItHasRoom(Vector3 pos, float radius)
    {
        RaycastHit hit; //unused here
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
