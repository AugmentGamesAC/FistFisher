﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// script for all fishes
/// contains references to all fishy things
/// </summary>
[System.Serializable]
public class BasicFish : MonoBehaviour
{
    #region working inspector dictionary
    /// <summary>
    /// this is the mess required to make dictionaries with  list as a value work in inspector
    /// used in this case to pair enum of menu enum with a list of menu objects
    /// </summary>
    [System.Serializable]
    public class ListOfFishHitboxes : InspectorDictionary<Collider, float> { }
    [SerializeField]
    protected ListOfFishHitboxes m_fishHitboxList = new ListOfFishHitboxes();
    public ListOfFishHitboxes FishHitboxList { get { return m_fishHitboxList; } }


    #endregion working inspector dictionary

    [SerializeField]
    protected FishDefintion m_fishDef;

    protected FishInstance m_fish;
    public FishInstance FishInstance => m_fish;

    public float m_personalSpaceRadius = 1.0f;

    public float Speed;
    public float TurnSpeed;

    [SerializeField]
    protected FishBrain.FishClassification m_fishClass = FishBrain.FishClassification.Fearful;
    public FishBrain.FishClassification FishClass { get { return m_fishClass; } }

    public Transform LookFrom;
    public FishSpawner Spawner;

    public bool m_inBaitZone = false;

    [SerializeField]
    protected FishArchetype m_fishArchetype;
    public FishArchetype FishArcheType { get { return m_fishArchetype; } }

    //behaviour
    protected BehaviorTree m_behaviour;

    [SerializeField]
    protected CombatPrompt promptPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Prompt prompt = gameObject.AddComponent<CombatPrompt>();
        prompt.Init(m_fishDef.IconDisplay, "Alt Action Button to Fight {0} Fish!", 1);

        m_fish = new FishInstance(m_fishDef, prompt);
        m_fish.Health.OnMinimumHealthReached += HandleDeath;

        //TODO: clean
        m_behaviour = GetComponent<BehaviorTree>();
    }
    private void HandleDeath()
    {
        //ObjectPool should Handle fish.
        gameObject.SetActive(false);
        //TODO: Fix prompt disapearing during combat.
    }
}
