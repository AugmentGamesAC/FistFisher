using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITurretAttack : ABehaviour
{
    public Gauntlet m_gauntlet;

    public override void OnBehaviourStart()
    {
        InitTurret();

        //get Component that will shoot turret projectile.
        //m_gauntlet = GetComponentInChildren<Gauntlet>();

        m_updateTimer = 0.0f;

        //fire initial shot.
        //m_gauntlet.Fire();

        m_data.state = OnBehaviourUpdate;
    }

    public override void OnBehaviourUpdate()
    {
        m_updateTimer += Time.deltaTime;

        //lock on and fire.
        if (PlayerInLineOfSight())
        {
            //rotatehead towards player.
            //should Slerp towards in future and not snap.
            transform.LookAt(m_data.followObject);

            if (m_updateTimer > m_updateDelay)
            {

                //fire spell projectile from "gauntlet".


                m_updateTimer = 0;
            }
        }
        else
        {
            //rotate back to default position.

        }
    }

    public override void OnBehaviourEnd()
    {

    }
}
