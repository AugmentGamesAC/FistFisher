using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIData : MonoBehaviour
{
    public enum Behaviour
    {
        Follow,
        Idle,
        Look,
        FlyTo
    };
    public Behaviour m_currentBehaviour;
    public Behaviour m_lastBehaviour;

    public delegate void State();
    public State state;

    public NavMeshAgent m_agent;
    public Transform followObject;//All enemies get a Ref to Player transform.

    //follow script vars for now.
    public float minDistToPlayer;
    public float maxDistToPlayer;

    /***********************************NAVMESH DATA RAGARDING MOVEMENT STATS***********************************/
    [SerializeField]
    public float m_movementSpeed = 3.5f;
    [SerializeField]
    public float m_angularSpeed = 120.0f;
    [SerializeField]
    public float m_acceleration = 8.0f;
    [SerializeField]
    public float m_stoppingDistance = 2.0f;

    /***********************************************************************************************************/

    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        //Default starting behaviour, could be idle or patrol.
        m_currentBehaviour = Behaviour.Idle;
        m_lastBehaviour = Behaviour.Idle;
    }
}
