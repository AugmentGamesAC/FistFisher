using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(HealthModule))]
[RequireComponent(typeof(PlayerMovement))]


[RequireComponent(typeof(CombatModule))]
[RequireComponent(typeof(Inventory))]

//Punchadex
public class Player : MonoBehaviour
{
    public HealthModule m_healthModule;
    public OxygenTracker m_oxygenTracker;

    public Vector3 m_spawnLocation;


    [SerializeField]
    protected FishArchetype m_fishArchetype;
    public FishArchetype FishType { get { return m_fishArchetype; } }

    protected void HandleDeath()
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

        gameObject.transform.position = m_spawnLocation;

        //player should be sent to respawn. 
    }

    private void Init()
    {
        m_healthModule = GetComponent<HealthModule>();

        m_healthModule.OnDeath += HandleDeath;
    }

    private void Start()
    {
        Init();
    }

}
