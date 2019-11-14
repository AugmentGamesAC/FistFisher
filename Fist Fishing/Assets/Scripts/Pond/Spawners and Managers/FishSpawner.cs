using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{

    GameObject m_fihPrefab;
    float m_normalMaxNumberOfFishForThisSpanerToSpawn = 10.0f;
    float m_normalNumberOfSecondsToSpanFish = 5.0f;

    protected FishArchetype m_fishArchetype { public get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        if (m_fihPrefab == null)
            GameObject.Destroy(this);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
