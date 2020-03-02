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
            bd.Start();
        }
    }

    /// <summary>
    /// this loops through all valid biomes, checks if it can spawn in them, picks fish or collectable, then spawns 
    /// </summary>
    void Update()
    {
        foreach (BiomeDefinition bd in m_biomes)
        {
            bd.SpawnStuff(Time.deltaTime);
        }
    }

}
