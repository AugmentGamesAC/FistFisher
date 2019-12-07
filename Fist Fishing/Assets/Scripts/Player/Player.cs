using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Harvester))]
[RequireComponent(typeof(HealthModule))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(CombatModule))]
[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(CraftingModule))]
//eventually require punchadex

public class Player : MonoBehaviour
{
    [SerializeField]
    protected HealthModule m_healthModule;
    public HealthModule HealthModule {get { return m_healthModule; } }
    private OxygenTracker m_oxygenTracker;

    private CharacterController m_characterController;

    public Vector3 m_respawnLocation;

    public GameObject m_InfluenceSphereObject;

    [SerializeField]
    protected FishArchetype m_fishArchetype;
    public FishArchetype FishType { get { return m_fishArchetype; } }

    public void SetNewCheckpoint(Vector3 point)
    {
        m_respawnLocation = point;
    }

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

        //gameObject.transform.position = m_spawnLocation.position;
        //GEt Vector between boat spawn and player.

        Vector3 MoveVector = m_respawnLocation - gameObject.transform.position;

        m_characterController.Move(MoveVector);

        PlayerMovement move = gameObject.GetComponent<PlayerMovement>();
        move.Mount();

    }

    private void Init()
    {
        if (m_InfluenceSphereObject == null)
            m_InfluenceSphereObject = gameObject.transform.parent.gameObject;

        m_healthModule = GetComponent<HealthModule>();

        if (m_characterController == null)
        {
            m_characterController = m_InfluenceSphereObject.GetComponent<CharacterController>();
        }

        m_oxygenTracker = GetComponentInChildren<OxygenTracker>();

        m_healthModule.OnDeath += HandleDeath;

    }

    private void Start()
    {
        Init();
    }

}
