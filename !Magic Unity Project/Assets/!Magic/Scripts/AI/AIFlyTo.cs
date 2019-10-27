using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIFlyTo : ABehaviour
{
    public float m_ObstacleDetectRadius = 5.0f;
    public float m_avoidanceWeight = 50.5f;

    bool m_isMoving = false;

    Vector3 m_moveDirection = Vector3.zero;

    //Initial function.
    public override void OnBehaviourStart()
    {
        Init();
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

    //since we're not doing navmeshes for this, we need to use lateupdate to actually move and the other update to get direction
    //(Sam is using Update for all AI behaviour, so this should be fine)
    void LateUpdate()
    {
        if(m_isMoving)
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
            m_isMoving = true;
            m_moveDirection = GetDirectionToMove();
            m_updateTimer = 0;
        }

        //LateUpdate();
    }


    //spherecast to see if it collides with anything
    private GameObject[] SpherecastForCollidables()
    {
        int Mask = ~LayerMask.GetMask("Ignore Raycast");
        RaycastHit[] hitInfos = Physics.SphereCastAll(gameObject.transform.position, m_ObstacleDetectRadius, Vector3.down, 0.1f, Mask);
        GameObject[] NearbyObjects = new GameObject[hitInfos.Length];

        for (int i=0;i< hitInfos.Length;i++)
        {
            Collider c = hitInfos[i].transform.gameObject.GetComponent<Collider>();
            if (c != null && c.isTrigger==false) //this will be null if there is no gameobject, is no collider, or collider is only a trigger
            {
                NearbyObjects[i] = hitInfos[i].transform.gameObject;
            }
        }
        return NearbyObjects;
    }

    //gets a spherecast of all nearby objects, tries to avoid them
    Vector3 GetDirectionToMove()
    {

        GameObject[] NearbyObjects = SpherecastForCollidables(); //get list of all nearby objects that can be collided with
        Vector3 avoidance = Vector3.zero;
        Vector3 direction = Vector3.zero;

        if (NearbyObjects != null)
        {
            foreach (GameObject g in NearbyObjects)
            {
                //if (Vector3.Distance(g.transform.position, transform.position) < m_ObstacleDetectRadius) //we'll just assume detection radius is avoidance radius as well to save some calcs
                {
                    avoidance += (transform.position - g.transform.position);
                }
            }
        }

        Vector3 TargetDir = m_data.followObject.position - transform.position;
        direction = TargetDir + avoidance * m_avoidanceWeight;
        direction = Vector3.Normalize(direction);

        return direction;
    }

    public override void OnBehaviourEnd()
    {
        m_data.m_agent = GetComponent<NavMeshAgent>();
    }
}
