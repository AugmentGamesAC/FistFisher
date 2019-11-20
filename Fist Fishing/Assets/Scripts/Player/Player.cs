using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Harvester))]
[RequireComponent(typeof(HealthModule))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(CombatModule))]
[RequireComponent(typeof(Inventory))]
//eventually require punchadex

public class Player : MonoBehaviour
{
    [SerializeField]
    protected FishArchetype m_fishArchetype;
    public FishArchetype FishType { get { return m_fishArchetype; } }

    protected void HandleDeath()
    {
        //stick stuff here to handle player death
    }

    private void Init()
    {

    }

    private void Start()
    {
        Init();
    }

}
