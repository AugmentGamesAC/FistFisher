using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

/// <summary>
/// the death event exists to despawn items and remove them from biome list
/// </summary>
#region death event
public delegate void Death();
public interface IDyingThing
{
    event Death Death;
}
#endregion



/// <summary>
/// This class exists to handle a biomes in scene
/// Deals with what it contains and what it can spawn
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
    protected float currentCooldown;

    [SerializeField]
    protected bool m_showDebugSpawnCountMessages = false;

    /// <summary>
    /// sets up a dictionary of all the spawnables the scene contains, 
    /// sets up whe map visibility and label stuff,
    /// then spawns in all the clutter
    /// </summary>
    public void Start()
    {
        m_MeshCollider = GetComponent<MeshCollider>();
        //m_MeshCollider.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        LayerMask l = LayerMask.NameToLayer("BoatMapOnly");
        m_MeshCollider.gameObject.layer = l;

        currentCooldown = UnityEngine.Random.Range(0, 0.25f);

        m_memberCount = new Dictionary<IEnumerable<ISpawnable>, int>()
        {
            {m_aggressiveProbSpawn   = m_myInstructions.AggressiveFishList.Cast<ISpawnable>() , 0},
            {m_mehProbSpawn          = m_myInstructions.MehFishList.Cast<ISpawnable>()        , 0},
            {m_preyProbSpawn         = m_myInstructions.PreyFishList.Cast<ISpawnable>()       , 0},
            {m_collectablesProbSpawn = m_myInstructions.CollectablesList.Cast<ISpawnable>()   , 0},
            {m_clutterProbSpawn      = m_myInstructions.ClutterList.Cast<ISpawnable>()        , 0}
        };
        //Debug.Log(m_myInstructions.AggressiveFishList.Count);
        if ((m_myInstructions.ClutterList.Count > 0))
           StartCoroutine(SpawnClutter());
        SpawnText();
        ResolveBiomeMeshRendering();

        if (m_myInstructions.CollectablesList.Count > 0)
            m_areThereAnyCollectables = true;
        if (m_myInstructions.AggressiveFishList.Count > 0 ||
            m_myInstructions.MehFishList.Count > 0 ||
            m_myInstructions.PreyFishList.Count > 0  )
            m_areThereAnyFish = true;
        if (m_areThereAnyFish)
            m_totalFishCount = m_myInstructions.AggressiveFishList.Count + m_myInstructions.MehFishList.Count + m_myInstructions.PreyFishList.Count;
    }

    /// <summary>
    /// gets the mesh of the biome (or adds it if it was removed)
    /// changes the mesh to be seethrough and of the specified biome colour
    /// </summary>
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
        r.enabled = true;
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

    /// <summary>
    /// gets the text prefab, sets up the transform and text
    /// </summary>
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

    /// <summary>
    /// countdown to spawn, then attempts to spawn
    /// </summary>
    public void Update()
    {

        if (NewMenuManager.PausedActiveState)
            return;

        if (Configurations.Instance.TurnOffBiomeSpawningEntirely)
            return;
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


    /// <summary>
    /// used locally for determining fish type to spawn. 
    /// could prolly repurpose one of several other fish type enums, but this is specific and controlled here
    /// </summary>
    private enum FishTypeToSpawn
    {
        prey,
        meh,
        pred,
        error
    }

    /// <summary>
    /// logic for determining which type of fish to spawn if it is to spawn a fish
    /// a lot lengthier now that we are allowing there to be empty lists of fish types
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// turns the above enum into a spawn and add to biome member list
    /// </summary>
    /// <param name="ft"></param>
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

    /// <summary>
    /// if we are allowed to spawn something in biome based on timer,
    /// figure out what to spawn and do so
    /// </summary>
    protected void ResolveSpawning()
    {
        if (m_memberCount == default) //nothing in any list to spawn
            return;
        if (!m_areThereAnyCollectables && !m_areThereAnyFish) //the above list includes clutter. this handles clutter-only biome
            return;
        if ((m_memberCount.Values.Sum() - m_memberCount[m_clutterProbSpawn]) >= m_myInstructions.MaxNumberOfSpawns) //it was counting clutter in spawns... not originally intended
            return;

        Debug.Log(m_areThereAnyCollectables + " - " + m_areThereAnyFish);

        if (m_areThereAnyCollectables && !m_areThereAnyFish) //only collectables
        {
            if (Configurations.Instance.TurnOffBiomeCollectablesEntirely)
                return;
            m_memberCount[m_collectablesProbSpawn] += (SpawnFromWeightedList(m_collectablesProbSpawn)) ? 1 : 0;
            return;
        }

        FishTypeToSpawn ft = DetermineFishTypeToSpawn();
        if (!m_areThereAnyCollectables && m_areThereAnyFish) //only fish
        {

            if (Configurations.Instance.TurnOffBiomeFishEntirely)
                return;
            SpawnFishFromType(ft);
            return;
        }

        if (m_areThereAnyCollectables && m_areThereAnyFish) //both fish and collectables
        {
            if (m_memberCount[m_collectablesProbSpawn] < (m_memberCount[m_mehProbSpawn] + m_memberCount[m_preyProbSpawn]))
            {
                if (Configurations.Instance.TurnOffBiomeCollectablesEntirely)
                    return;
                m_memberCount[m_collectablesProbSpawn] += (SpawnFromWeightedList(m_collectablesProbSpawn)) ? 1 : 0;
            }
            else
            {
                if (Configurations.Instance.TurnOffBiomeFishEntirely)
                    return;
                SpawnFishFromType(ft);
            }
            return;
        }
    }


    /// <summary>
    /// given the list of a type, use the associated weights to randomly pick one of the options
    /// then tell it to set up itself, apply a death event so this biome can track it, rotate 
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    protected bool SpawnFromWeightedList(IEnumerable<ISpawnable> list)
    {
        float rand = UnityEngine.Random.Range(0, 1.0f);
        foreach (ISpawnable possibbleSpawn in list)
            if ((rand -= possibbleSpawn.WeightedChance) < 0)
            {
                GameObject g = possibbleSpawn.Spawn((possibbleSpawn.MeshOverRide == default) ? m_MeshCollider : possibbleSpawn.MeshOverRide);
                Debug.Log(g);
                g.transform.Rotate(Vector3.up, UnityEngine.Random.Range(0, 360.0f));
                BottomAdjust(g, possibbleSpawn);
                IDyingThing d = g.GetComponent<IDyingThing>();
                if(d!=null)
                    d.Death += () => { m_memberCount[list]--; };
                return true;
            }
        return false;
    }

    /// <summary>
    /// tries to make the spawnable a probabilityspawn, then check if the object is set to span from bottom
    /// if so, adjust the gameobject up
    /// this is a prime example of something that should be redesigned
    /// </summary>
    private void BottomAdjust(GameObject g, ISpawnable possibbleSpawn)
    {
        ProbabilitySpawnFish f = possibbleSpawn as ProbabilitySpawnFish;
        ProbabilitySpawnClutter c = possibbleSpawn as ProbabilitySpawnClutter;
        ProbabilitySpawnCollectable e = possibbleSpawn as ProbabilitySpawnCollectable;
        bool bottom = false;

        if (f != null)
            bottom = f.SpawnFromBottom;
        else if (c != null)
            bottom = c.SpawnFromBottom;
        else if (e != null)
            bottom = e.SpawnFromBottom;

        if(bottom)
            SpawningTweaks.AdjustForBottom(g);
    }

    /// <summary>
    /// this takes the list of clutter and amount of clutter, and spawns random clutter of that quantity
    /// </summary>
    /// <param name="bd"></param>
    protected IEnumerator SpawnClutter()
    {
        if (Configurations.Instance.TurnOffBiomeClutterEntirely)
            yield return default;

        if (!(m_myInstructions.ClutterList.Count() > 0) || m_myInstructions.AmountOfClutterToSpawn == 0)
            yield return default;
        /*Debug.Log(m_myInstructions);
        Debug.Log(m_myInstructions.ClutterList);
        Debug.Log(m_memberCount[m_clutterProbSpawn]);*/

        m_memberCount[m_clutterProbSpawn] = (SpawnFromWeightedList(m_myInstructions.ClutterList)) ? 1 : 0;
        //Debug.Log(m_memberCount[m_clutterProbSpawn]);

        while (m_memberCount[m_clutterProbSpawn] < m_myInstructions.AmountOfClutterToSpawn)
        {
            m_memberCount[m_clutterProbSpawn] += (SpawnFromWeightedList(m_myInstructions.ClutterList)) ? 1 : 0;
            yield return new WaitForEndOfFrame();
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

    /// <summary>
    /// figures out if the object is inside the biome. 
    /// Unity hates trying to do this for concave mesh colliders, 
    /// so it is currently using bounding boxes if concave until much more lengthy complex maths can be added
    /// </summary>
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
    public static bool SpherecastToEnsureItHasRoom(Vector3 pos, float radius, out RaycastHit hit)
    {
        return Physics.SphereCast(pos, radius, Vector3.down, out hit, Mathf.Infinity, ~LayerMask.GetMask("Player", "Ignore Raycast", "Water", "BoatMapOnly"));
    }

    /// <summary>
    /// this takes in a position in world and raycasts down and returns where it hit the floor
    /// </summary>
    public static Vector3 GetSeafloorPosition(Vector3 pos)
    {
        RaycastHit hit;
        Physics.Raycast(pos, Vector3.down, out hit, Mathf.Infinity, ~LayerMask.GetMask("Player", "Ignore Raycast", "Water", "BoatMapOnly"));
        return hit.point;
    }

}
