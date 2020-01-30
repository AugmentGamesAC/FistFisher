using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class TestCombatManager : CombatManager
{
    [Test]
    public void CombatManagerStart()
    {
        m_fishInCombatInfo.Add(new FishCombatInfo());
        m_fishInCombatInfo.Add(new FishCombatInfo());
        m_fishInCombatInfo.Add(new FishCombatInfo());

        foreach (var fish in m_fishInCombatInfo)
        {
            Assert.NotNull(fish);
        }
    }


    [Test]
    public void PlayerStartedCombat_Test()
    {
        StartCombat(true);

        //doesn't contain after startCombat() since he gets dequeued once it's his turn.
        Assert.That(!m_roundQueue.Contains(m_playerCombatInfo));

        //should contain all fish still until the fish turn has started.
        foreach (var fish in m_fishInCombatInfo)
        {
            Assert.That(m_roundQueue.Contains(fish));
        }

        //The CombatManager should be awaiting player input...
        Assert.That(m_currentCombatState == CombatStates.AwaitingPlayerRound);
        //this kind of thing works too. (as long as return type is bool)
        //Assert.That(ResolvePlayerMove_Test()); 
    }

    [Test]
    public void ResolvePlayerAttack_Test()
    {

        //ARRANGE
        //had to create moves for the player attack test to work properly.
        List<CombatMoveInfo> moves = new List<CombatMoveInfo>
        {
            new CombatMoveInfo(10, 0.5f, 30, 2, 25),
            new CombatMoveInfo(5, 0, 2, 3, 12),
            new CombatMoveInfo(45, 0, 0, 10, 10)
        };

        m_playerCombatInfo.m_attackPinwheel = new PinWheel<CombatMoveInfo>(1, moves);

        foreach (var slot in m_playerCombatInfo.m_attackPinwheel.Slots)
        {
            Assert.NotNull(slot.Key);
            Assert.NotNull(slot.Value);
        }

        //This call will come from the User's selection.
        m_playerCombatInfo.m_attackPinwheel.SetSelectedOption(1);
        m_fishSelection = 0;
        SelectedFish.m_combatDistance = 10;

        float distanceAfterAttack = SelectedFish.m_combatDistance 
            - m_playerCombatInfo.m_attackPinwheel.GetSelectedOption().m_moveDistance //this works
            + SelectedFish.FishData.CombatSpeed; //this doesn't work

        //ACT
        PlayerAttack();

        
        //All fish should have finished their turn within one frame.


        //ASSERT
        //check fish if fish position was affected properly.
        //should be 10 - 2(move.distance) + 4(fish.moveSpeed.)

        //test this elswhere.
        Assert.AreEqual(m_fishInCombatInfo[0].m_combatDistance, SelectedFish.m_combatDistance);
        Assert.AreEqual(distanceAfterAttack, SelectedFish.m_combatDistance);
        //Assert.AreEqual(m_fishInCombatInfo[1].m_combatDistance, SelectedFish.m_combatDistance);



        //check health on fish, should be 90 after the first attack.
        Assert.AreEqual(90.0f, SelectedFish.FishData.Health.CurrentAmount);

        //check that the queue is what expected.
        //the player turn is dequeued when awaiting input so 3 fish is in queue.
        Assert.AreEqual(3, m_roundQueue.Count); 
        //Should be awaiting player input...
        Assert.AreEqual(m_currentCombatState,CombatStates.AwaitingPlayerRound);
    }



}
