using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(HealthModule))]
[RequireComponent(typeof(PlayerMovement))]

public class Player : MonoBehaviour
{

    protected FishArchetype m_fishArchetype { public get; private set; }


    [SerializeField]
    protected FishArchetype m_fishArchetype;
    public FishArchetype CurrentHealth { get { return m_fishArchetype; } }






}
