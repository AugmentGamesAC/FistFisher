using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// basic behaviour for fish that picked a random valid position to swim to and did so
/// </summary>
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
        BasicFish2 me = m_me.GetComponent<BasicFish2>();
        if (me == default)
        {
            m_target.transform.position = Vector3.zero;
            return;
        }

        m_target.transform.position = me.Defintion.FindNewSpot(me.Home);
    }
}
