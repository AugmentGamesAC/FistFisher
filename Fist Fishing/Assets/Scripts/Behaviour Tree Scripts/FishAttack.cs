using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// tasks for aggressive fish to try to attack something
/// </summary>
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

    /// <summary>
    /// figures out if it can bite something, and starts combat if it can
    /// </summary>
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
            PlayerMotion fishVictem = chewable.GetComponent<PlayerMotion>();
            if (fishVictem == default)
                continue;
            CombatManager.Instance.StartCombat(false, fishVictem.SurroundingFish, fishVictem);
        }
    }
}

