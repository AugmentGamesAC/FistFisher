using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITurretAttack : ABehaviour
{
    public Gauntlet m_gauntlet;
    public Transform from;
    public Transform to;

    private float timeCount = 0.0f;

    private void Awake()
    {
    }

    public override void OnBehaviourStart()
    {
        InitTurret();

        //get Component that will shoot turret projectile.
        //m_gauntlet = GetComponentInChildren<Gauntlet>();

        m_updateTimer = 0.0f;

        //fire initial shot. use core instead.
        //if (PlayerInLineOfSight())
        //      m_gauntlet.Fire();


        m_data.state = OnBehaviourUpdate;
    }

    public override void OnBehaviourUpdate()
    {
        m_updateTimer += Time.deltaTime;

        //lock on and fire.
        if (PlayerInLineOfSight())
        {
            transform.LookAt(m_data.followObject);

            //find angle between two vectors:
            //Quaternion.FromToRotation(Vector3, Vector3);
            //Vector3 direction = m_data.followObject.position - transform.position;

            //transform.rotation = Quaternion.Slerp(transform.rotation, to.rotation, timeCount);
            //timeCount = timeCount + Time.deltaTime;


            //rotatehead towards player.
            //should Slerp towards in future and not snap.

            if (m_updateTimer > m_updateDelay)
            {
                //fire spell projectile from "gauntlet".


                m_updateTimer = 0;
            }
        }
        else
        {
            //float yRot = -55.0f;
            //rotate back to default position.
            //transform.eulerAngles = new Vector3(m_localTransform.rotation.x, m_localTransform.rotation.y + yRot, m_localTransform.rotation.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, m_localTransform.rotation, timeCount);
            timeCount = timeCount + Time.deltaTime;

            if (Mathf.Approximately(transform.rotation.y, m_localTransform.rotation.y))
            {
                TransitionBehaviour(AIData.Behaviour.Idle);
                timeCount = 0.0f;
            }
        }
    }

    public override void OnBehaviourEnd()
    {

    }
}
