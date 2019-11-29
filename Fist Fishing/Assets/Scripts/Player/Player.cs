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
    private HealthModule m_healthModule;
    private OxygenTracker m_oxygenTracker;

    private CharacterController m_characterController;

    public Transform m_respawnLocation;


    [SerializeField]
    protected FishArchetype m_fishArchetype;
    public FishArchetype FishType { get { return m_fishArchetype; } }

    public void SetNewCheckpoint(Transform point)
    {
        m_respawnLocation = point;
    }

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

        //gameObject.transform.position = m_spawnLocation.position;
        //GEt Vector between boat spawn and player.

        Vector3 MoveVector = m_respawnLocation.position - gameObject.transform.position;

        m_characterController.Move(MoveVector);


        /*PlayerMovement move = gameObject.GetComponent<PlayerMovement>();
        move.m_isMounted = true;
        move.m_canMount = false;*/

        //player should be sent to respawn. 
    }

    private void Init()
    {
        m_healthModule = GetComponent<HealthModule>();

        m_characterController = GetComponent<CharacterController>();

        m_oxygenTracker = GetComponentInChildren<OxygenTracker>();

        m_healthModule.OnDeath += HandleDeath;
    }

    private void Start()
    {
        Init();
    }

}
