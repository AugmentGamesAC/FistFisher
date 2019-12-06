using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAttack : FishTask
{
    public override NodeResult Execute()
    {
        ReadInfo();

        PlayerInMouthCheck();

        return NodeResult.SUCCESS;
    }

    public void PlayerInMouthCheck()
    {
        BiteManager myMouth = m_me.GetComponentInChildren<BiteManager>();
        if (myMouth == default)
            return;

        foreach(Collider chewable in myMouth.InMouth)
        {
            Player fishVictem = chewable.GetComponentInParent<Player>();
            if (fishVictem == default)
                continue;
            fishVictem.HealthModule.TakeDamage(myMouth.BiteDamage);
        }
    }
}

