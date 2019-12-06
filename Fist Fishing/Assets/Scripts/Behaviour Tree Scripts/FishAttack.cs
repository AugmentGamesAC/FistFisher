using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAttack : FishTask
{
    public override NodeResult Execute()
    {
        ReadInfo();



        PlayerInMouthCheck();
        ResolveBiteTimeout();

        return NodeResult.SUCCESS;
    }

    public void ResolveBiteTimeout()
    {
        m_biteCooldown -= (m_biteCooldown > 0) ? Time.deltaTime : 0;
        m_tree.SetValue(FishBrain.BiteCooldownName, m_biteCooldown);
    }

    public void PlayerInMouthCheck()
    {
        if (m_biteCooldown > 0)
            return;

        BiteManager myMouth = m_me.GetComponentInChildren<BiteManager>();
        if (myMouth == default)
            return;
        m_biteCooldown = myMouth.TimeBeteenBites;


        foreach(Collider chewable in myMouth.InMouth)
        {
            Player fishVictem = chewable.GetComponentInParent<Player>();
            if (fishVictem == default)
                continue;   
            fishVictem.HealthModule.TakeDamage(myMouth.BiteDamage);
        }
    }
}

