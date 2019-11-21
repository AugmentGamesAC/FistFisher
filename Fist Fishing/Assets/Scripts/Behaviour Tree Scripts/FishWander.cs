using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishWander : FishTask
{
    public override NodeResult Execute()
    {
        ReadInfo();
        ChooseRandomLocation();

        return NodeResult.SUCCESS;
    }

    public void ChooseRandomLocation()
    {
        BasicFish me = m_me.GetComponent<BasicFish>();
        if (me == default)
        {
            m_target.transform.position = Vector3.zero;
            return;
        }


        m_target.transform.position = me.Spawner.transform.position + Random.insideUnitSphere * me.Spawner.m_spawnRadius;
    }
}
