using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using TMPro;

public delegate void CleanupCall();
public interface IDyingThing
{
    event CleanupCall Death;
}
/// <summary>
/// This class exists to handle all the biomes in scene
/// </summary>
[System.Serializable, RequireComponent(typeof(MeshCollider))]
public class BiomeInstance : MonoBehaviour
{
    protected MeshCollider m_MeshCollider;

    [SerializeField]
    protected BiomeDefinition m_myInstructions;
    public BiomeDefinition Definiton { get => m_myInstructions; set => m_myInstructions = value; }
    
    protected TextMeshPro m_biomeText;

    protected Dictionary<IEnumerable<ISpawnable>, int> m_memberCount;
    protected IEnumerable<ISpawnable> m_aggressiveProbSpawn;
    protected IEnumerable<ISpawnable> m_mehProbSpawn;
    protected IEnumerable<ISpawnable> m_preyProbSpawn;
    protected IEnumerable<ISpawnable> m_collectablesProbSpawn;
    protected IEnumerable<ISpawnable> m_clutterProbSpawn;

    protected bool m_areThereAnyFish = false;
    protected bool m_areThereAnyCollectables = false;
    protected int m_totalFishCount;

    [SerializeField]
    protected bool m_showDebugSpawnCountMessages = false;

    public void Start()
    {
        m_MeshCollider = GetComponent<MeshCollider>();
        //m_MeshCollider.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        LayerMask l = LayerMask.NameToLayer("BoatMapOnly");
        m_MeshCollider.gameObject.layer = l;

        currentCooldown = UnityEngine.Random.Range(0, 0.25f);

        m_memberCount = new Dictionary<IEnumerable<ISpawnable>, int>()
        {
            {m_aggressiveProbSpawn  = m_myInstructions.AggressiveFishList.Cast<ISpawnable>() , 0},
            {m_mehProbSpawn         = m_myInstructions.MehFishList.Cast<ISpawnable>()        , 0},
            {m_preyProbSpawn        = m_myInstructions.PreyFishList.Cast<ISpawnable>()       , 0},
            {m_collectablesProbSpawn= m_myInstructions.CollectablesList.Cast<ISpawnable>()   , 0},
            {m_clutterProbSpawn      = m_myInstructions.ClutterList.Cast<ISpawnable>()        , 0}
        };

        if ((m_myInstructions.ClutterList.Count > 0))
            SpawnClutter();
        SpawnText();
        ResolveBiomeMeshRendering();

        if (m_myInstructions.CollectablesList.Count > 0)
            m_areThereAnyCollectables = true;
        if (m_myInstructions.AggressiveFishList.Count > 0 &&
            m_myInstructions.MehFishList.Count > 0 &&
            m_myInstructions.PreyFishList.Count > 0  )
            m_areThereAnyFish = true;
        if (m_areThereAnyFish)
            m_totalFishCount = m_myInstructions.AggressiveFishList.Count + m_myInstructions.MehFishList.Count + m_myInstructions.PreyFishList.Count;
    }

    private void ResolveBiomeMeshRendering()
    {
        //m_MeshCollider
        MeshFilter m = gameObject.GetComponent<MeshFilter>();
        MeshRenderer r = gameObject.GetComponent<MeshRenderer>();
        if (m == null)
        {
            m = gameObject.AddComponent<MeshFilter>();
            m.sharedMesh = m_MeshCollider.sharedMesh;
        }
        if (r == null)
        {
            r = gameObject.AddComponent<MeshRenderer>();

            GameObject p = GameObject.CreatePrimitive(PrimitiveType.Plane);
            p.SetActive(false);
            Material mat = p.GetComponent<MeshRenderer>().sharedMaterial;
            DestroyImmediate(p);
            r.material = mat;
        }
        //r.material.color = Definiton.BoatMapColour;
        r.material.SetColor("_BaseColor", Definiton.BoatMapColour); //please note for later. this was frustrating.

        //snippet taken from https://answers.unity.com/questions/1608815/change-surface-type-with-lwrp.html
        //as setting transparency at runtime is a nightmare it seems
        r.material.SetFloat("_Surface", 1.0f);
        r.material.SetOverrideTag("RenderType", "Transparent");
        r.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.DstColor);
        r.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        r.material.SetInt("_ZWrite", 0);
        r.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        r.material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        r.material.SetShaderPassEnabled("ShadowCaster", false);

    }

    private void SpawnText()
    {
        Vector3 v = gameObject.transform.position;
        v.y = Definiton.TextHeight;

        GameObject o = GameObject.Instantiate(Definiton.BaseTextTemplate, gameObject.transform);
        m_biomeText = o.GetComponent<TextMeshPro>();
        m_biomeText.text = Definiton.Name;
        m_biomeText.gameObject.transform.position = v;
        m_biomeText.gameObject.transform.localScale = Vector3.one * Definiton.TextScale;
    }

    protected float currentCooldown;

    public void Update()
    {
        //Debug.Log("Biome update: " + currentCooldown);
        if (currentCooldown < 0)
        {
            ResolveSpawning();
            currentCooldown = m_myInstructions.TimeBetweenSpawns;
        }
        currentCooldown -= Time.deltaTime;



        if(m_showDebugSpawnCountMessages)
        Debug.Log(  "Biome: "           + Definiton.Name    +
                    "   Clutter: "      + m_memberCount[m_clutterProbSpawn] +
                    "   Collectables: " + m_memberCount[m_collectablesProbSpawn] +
                    "   Aggressive: "   + m_memberCount[m_aggressiveProbSpawn] +
                    "   Meh: "          + m_memberCount[m_mehProbSpawn] +
                    "   Prey: "         + m_memberCount[m_preyProbSpawn]
            );
    }



    private enum FishTypeToSpawn
    {
        prey,
        meh,
        pred,
        error
    }
    private FishTypeToSpawn DetermineFishTypeToSpawn()
    {
        //prey
        if (m_myInstructions.AggressiveFishList.Count == 0 &&
            m_myInstructions.MehFishList.Count == 0 &&
            m_myInstructions.PreyFishList.Count > 0)
        {
            return FishTypeToSpawn.prey;
        }
        //pred
        if (m_myInstructions.AggressiveFishList.Count > 0 &&
            m_myInstructions.MehFishList.Count == 0 &&
            m_myInstructions.PreyFishList.Count == 0)
        {
            return FishTypeToSpawn.pred;
        }
        //meh
        if (m_myInstructions.AggressiveFishList.Count == 0 &&
            m_myInstructions.MehFishList.Count > 0 &&
            m_myInstructions.PreyFishList.Count == 0)
        {
            return FishTypeToSpawn.meh;
        }
        //prey+meh
        if (m_myInstructions.AggressiveFishList.Count == 0 &&
            m_myInstructions.MehFishList.Count > 0 &&
            m_myInstructions.PreyFishList.Count > 0)
        {
            if (m_memberCount[m_preyProbSpawn] < m_memberCount[m_mehProbSpawn])
                return FishTypeToSpawn.prey;
            return FishTypeToSpawn.meh;
        }
        //prey+pred
        if (m_myInstructions.AggressiveFishList.Count > 0 &&
            m_myInstructions.MehFishList.Count == 0 &&
            m_myInstructions.PreyFishList.Count > 0)
        {
            if (m_memberCount[m_preyProbSpawn] < m_memberCount[m_aggressiveProbSpawn])
                return FishTypeToSpawn.prey;
            return FishTypeToSpawn.pred;
        }
        //pred+meh
        if (m_myInstructions.AggressiveFishList.Count > 0 &&
            m_myInstructions.MehFishList.Count > 0 &&
            m_myInstructions.PreyFishList.Count == 0)
        {
            if (m_memberCount[m_mehProbSpawn] < m_memberCount[m_aggressiveProbSpawn])
                return FishTypeToSpawn.meh;
            return FishTypeToSpawn.pred;
        }
        //all3
        if (m_myInstructions.AggressiveFishList.Count > 0 &&
            m_myInstructions.MehFishList.Count > 0 &&
            m_myInstructions.PreyFishList.Count > 0)
        {
            /*if (m_memberCount[m_collectablesProbSpawn] < m_memberCount[m_preyProbSpawn])
                return FishTypeToSpawn.error;*/
            if (m_memberCount[m_preyProbSpawn] < m_memberCount[m_mehProbSpawn])
                return FishTypeToSpawn.prey;
            if (m_memberCount[m_mehProbSpawn] < m_memberCount[m_aggressiveProbSpawn])
                return FishTypeToSpawn.meh;
            return FishTypeToSpawn.pred;
        }


        return FishTypeToSpawn.error;
    }

    private void SpawnFishFromType(FishTypeToSpawn ft)
    {
        switch (ft)
        {
            case FishTypeToSpawn.pred:
                m_memberCount[m_aggressiveProbSpawn] += (SpawnFromWeightedList(m_aggressiveProbSpawn)) ? 1 : 0;
                break;
            case FishTypeToSpawn.meh:
                m_memberCount[m_mehProbSpawn] += (SpawnFromWeightedList(m_mehProbSpawn)) ? 1 : 0;
                break;
            case FishTypeToSpawn.prey:
                m_memberCount[m_preyProbSpawn] += (SpawnFromWeightedList(m_preyProbSpawn)) ? 1 : 0;
                break;
        }
    }


    protected void ResolveSpawning()
    {
        if (m_memberCount == default) //nothing in any list to spawn
            return;
        if (!m_areThereAnyCollectables && !m_areThereAnyFish) //the above list includes clutter. this handles clutter-only biome
            return;
        if ((m_memberCount.Values.Sum() - m_memberCount[m_clutterProbSpawn]) >= m_myInstructions.MaxNumberOfSpawns) //it was counting clutter in spawns... not originally intended
            return;


        if (m_areThereAnyCollectables && !m_areThereAnyFish) //only collectables
        {
            m_memberCount[m_collectablesProbSpawn] += (SpawnFromWeightedList(m_collectablesProbSpawn)) ? 1 : 0;
            return;
        }

        FishTypeToSpawn ft = DetermineFishTypeToSpawn();
        if (!m_areThereAnyCollectables && m_areThereAnyFish) //only fish
        {
            SpawnFishFromType(ft);
            return;
        }

        if (m_areThereAnyCollectables && m_areThereAnyFish) //both fish and collectables
        {
            if(m_memberCount[m_collectablesProbSpawn] < (m_memberCount[m_mehProbSpawn] + m_memberCount[m_preyProbSpawn]))
                m_memberCount[m_collectablesProbSpawn] += (SpawnFromWeightedList(m_collectablesProbSpawn)) ? 1 : 0;
            else
                SpawnFishFromType(ft);
            return;
        }
    }



    protected bool SpawnFromWeightedList(IEnumerable<ISpawnable> list)
    {
        float rand = UnityEngine.Random.Range(0, 1.0f);
        foreach (ISpawnable possibbleSpawn in list)
            if ((rand -= possibbleSpawn.WeightedChance) < 0)
            {
                GameObject g = possibbleSpawn.Instatiate((possibbleSpawn.MeshOverRide == default) ? m_MeshCollider : possibbleSpawn.MeshOverRide);
                g.GetComponent<IDyingThing>().Death += () => { m_memberCount[list]--; };
                g.transform.Rotate(Vector3.up, UnityEngine.Random.Range(0, 360.0f));
                SpawningTweaks.AdjustForBottom(g);
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
        if (!(m_myInstructions.ClutterList.Count() > 0))
            return;

        m_memberCount[m_clutterProbSpawn] = (SpawnFromWeightedList(m_myInstructions.ClutterList)) ? 1 : 0;

        while (m_memberCount[m_clutterProbSpawn] < m_myInstructions.AmountOfClutterToSpawn)
        {
            m_memberCount[m_clutterProbSpawn] += (SpawnFromWeightedList(m_myInstructions.ClutterList)) ? 1 : 0;
        }
    }


    /// <summary>
    /// this picks a random position within a given mesh
    /// </summary>
    /// <param name="bd"></param>
    /// <returns></returns>
    public static Vector3 FindValidPosition(MeshCollider biome)
    {
        Bounds b = biome.bounds;
        bool validPos = false;

        Vector3 pos = Vector3.zero;
        while (!validPos)
        {
            pos.x = UnityEngine.Random.Range(b.min.x, b.max.x);
            pos.y = UnityEngine.Random.Range(b.min.y, b.max.y);
            pos.z = UnityEngine.Random.Range(b.min.z, b.max.z);
            if (IsInside(biome, pos, biome.convex))
                validPos = true;
        }
        return pos;
    }

    public static bool IsInside(Collider c, Vector3 point, bool convex)
    {
        // Because ClosestPoint(point)=point if point is inside - not clear from docs I feel
        if(!convex)
        {
            if (c.bounds.ClosestPoint(point) == point)
                return true;
        }
        else
        {
            if (c.ClosestPoint(point) == point)
                return true;
        }
        
        return false;
        //return c.ClosestPoint(point) == point;
    }

    /// <summary>
    /// this takes the size of the fish and position it is trying to span in to ensure it has room
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    public static bool SpherecastToEnsureItHasRoom(Vector3 pos, float radius, out RaycastHit hit)
    {
        return Physics.SphereCast(pos, radius, Vector3.down, out hit, Mathf.Infinity, ~LayerMask.GetMask("Player", "Ignore Raycast", "Water", "BoatMapOnly"));
    }

    /// <summary>
    /// this takes in a position in world and raycasts down and returns where it hit the floor
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static Vector3 GetSeafloorPosition(Vector3 pos)
    {
        RaycastHit hit;
        Physics.Raycast(pos, Vector3.down, out hit, Mathf.Infinity, ~LayerMask.GetMask("Player", "Ignore Raycast", "Water", "BoatMapOnly"));
        return hit.point;
    }

}
