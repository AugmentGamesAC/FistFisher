using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : ASpellUser
{
    protected ASpellUser m_PlayerRef;

    //void Awake()
    //{
    //    m_playerRef = GameObject.FindGameObjectWithTag("Player");
    //}

    //Call this on Health Change.
    void CheckDeath()
    {
        if(IsDead())
        {
            //Destroy 

            //remove from Spawner's enemy list.

        }
    }

    public override Transform Aiming => throw new System.NotImplementedException();
}
