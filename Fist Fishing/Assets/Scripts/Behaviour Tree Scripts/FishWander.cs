﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishWander : FishTask
{
    public void Start()
    {
        ReadInfo();
        ChooseRandomLocation();
    }

    public override NodeResult Execute()
    {
        ReadInfo();

        if (Vector3.Distance(m_me.transform.position, m_target.transform.position) < m_accuracy)
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
