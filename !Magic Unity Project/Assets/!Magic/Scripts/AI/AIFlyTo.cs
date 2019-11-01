using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIFlyTo : ABehaviour
{
    public float m_ObstacleDetectRadius = 5.0f;
    public float m_avoidanceWeight = 50.5f;

   // bool m_isMoving = false;

    Vector3 m_moveDirection = Vector3.zero;

    //Initial function.
    public override void OnBehaviourStart()
    {
        InitPatrolBot();
        m_moveDirection = Vector3.zero;

        m_data.m_agent = null;

        //if distance between the two is smaller than the max distance.
        if (PlayerInLineOfSight())
        {
            //m_data.m_agent.SetDestination(m_data.followObject.position); //this uses navmeshes, so scrap that
            m_updateTimer = 0;
            m_data.state = OnBehaviourUpdate;
        }
        else
        {
            TransitionBehaviour(AIData.Behaviour.Idle);
        }
    }

    void LateUpdate()
    {
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(m_moveDirection), m_data.m_angularSpeed * Time.deltaTime);
        if (Vector3.Distance(gameObject.transform.position, m_data.followObject.transform.position) < m_data.m_movementSpeed * Time.deltaTime)
        {
            gameObject.transform.position = m_data.followObject.transform.position;
        }
        else
        {
            gameObject.transform.Translate(0, 0, m_data.m_movementSpeed * Time.deltaTime);
        }
    }


    public override void OnBehaviourUpdate()
    {
        m_updateTimer += Time.deltaTime;

        //if too far from me, go to idle.
        if (!PlayerInLineOfSight())
        {
            TransitionBehaviour(AIData.Behaviour.Idle);
        }
        else if(PlayerInAttackRange())
        {
            TransitionBehaviour(AIData.Behaviour.Attack);
        }
        else if (m_updateTimer > m_updateDelay && PlayerInLineOfSight())
        {
            m_moveDirection = GetDirectionToMove();



            m_updateTimer = 0;
        }

        //LateUpdate();
    }

    //shoot ray towards movement direction, if it hits, get a reflection
    private Vector3 CollisionAvoidance(Vector3 targetdir)
    {
        Vector3 aviodanceDir = Vector3.zero;
        int Mask = ~LayerMask.GetMask("Ignore Raycast");

        RaycastHit hitInfo = new RaycastHit();
        if(Physics.Raycast(gameObject.transform.position, targetdir, out hitInfo, m_ObstacleDetectRadius, Mask))
        {
            aviodanceDir = Vector3.Reflect(targetdir, hitInfo.normal);
        }


        return aviodanceDir;
    }

    //gets a diretion to move by a durect line towards target, then an avoidance if going to hit something
    Vector3 GetDirectionToMove()
    {
        Vector3 direction = Vector3.zero;


        direction = m_data.followObject.position - transform.position; 
        direction = Vector3.Normalize(direction);

        direction += CollisionAvoidance(direction) * m_avoidanceWeight;
        direction = Vector3.Normalize(direction);

        return direction;
    }

    public override void OnBehaviourEnd()
    {
        m_data.m_agent = GetComponent<NavMeshAgent>();
    }
}
