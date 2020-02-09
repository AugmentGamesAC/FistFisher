using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Harvester))]
[RequireComponent(typeof(HealthModule))]
[RequireComponent(typeof(Inventory))]
//eventually require punchadex

public class Player : MonoBehaviour
{
    [SerializeField]
    protected HealthModule m_healthModule;
    public HealthModule HealthModule {get { return m_healthModule; } }
    private OxygenTracker m_oxygenTracker;

    [SerializeField]
    protected FishArchetype m_fishArchetype;
    public FishArchetype FishType { get { return m_fishArchetype; } }

    public void HandleDeath()
    {
        //stick stuff here to handle player death

        //if you have lives, send player to the boat spawn.
        //no lives = gameover screen.

        //Disable Player Input until respawn.
        if (m_healthModule != null)
            m_healthModule.ResetHealth();

        //reset Oxygen.
        if (m_oxygenTracker != null)
            m_oxygenTracker.ResetOxygen();
    }

    private void Init()
    {
        m_healthModule = GetComponent<HealthModule>();

        m_oxygenTracker = GetComponentInChildren<OxygenTracker>();

        m_healthModule.OnDeath += HandleDeath;

        HandleDeath();
    }

    private void Start()
    {
        Init();
    }
}
