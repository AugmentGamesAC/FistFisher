using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEditor;

public class TestCombatManager : CombatManager
{


    [Test]
    public void A_CombatManagerStart()
    {
        var something = AssetDatabase.LoadAssetAtPath<FishDefintion>("Assets/Resources/Aggresive 1.asset");
        var somethingelse = new FishInstance(something);
        var someelsething = new FishCombatInfo(somethingelse);
        m_FishSelection.AddItem(someelsething);
        m_FishSelection.AddItem(new FishCombatInfo(new FishInstance(something)));
        m_FishSelection.AddItem(new FishCombatInfo(new FishInstance(something)));

        foreach (var fish in m_FishSelection)
        {
            Assert.NotNull(fish);
        }
    }


    [Test]
    public void B_PlayerStartedCombat_Test()
    {
        StartCombat(true);

        //doesn't contain after startCombat() since he gets dequeued once it's his turn.
        Assert.AreEqual(false, m_roundQueue.Contains(m_playerCombatInfo));

        //should contain all fish still until the fish turn has started.
        foreach (var fish in m_FishSelection)
        {
            Assert.That(m_roundQueue.Contains(fish));
        }

        Assert.That(m_roundQueue.Count > 1, string.Format("this was the real item was {0}", m_roundQueue.Count.ToString()));
        //The CombatManager should be awaiting player input...
        Assert.That(m_currentCombatState == CombatStates.AwaitingPlayerRound, string.Format("this was the real item was {0}" , m_currentCombatState.ToString()));
        Assert.AreEqual(3, m_roundQueue.Count);//3 fish in q.
    }

    [Test]
    public void C_ResolvePlayerAttack_Test()
    {

        //ARRANGE
        //had to create moves for the player attack test to work properly.
        List<CombatMoveInfo> moves = new List<CombatMoveInfo>
        {
            new CombatMoveInfo(10, 0.5f, 30, 2, 25),
            new CombatMoveInfo(5, 0, 2, 3, 12),
            new CombatMoveInfo(45, 0, 0, 10, 10)
        };

        m_playerCombatInfo.m_attackPinwheel = new PinwheelTracker<CombatMoveInfo>(1, moves);

        foreach (var slot in m_playerCombatInfo.m_attackPinwheel.Slots)
        {
            Assert.NotNull(slot.Key);
            Assert.NotNull(slot.Value);
        }

        //This call will come from the User's selection.
        m_playerCombatInfo.m_attackPinwheel.SetSelectedOption(1);
        m_FishSelection.SetSelection(0);
        m_FishSelection.SelectedItem.CombatDistance.SetValue(10);

        float distanceAfterAttack = m_FishSelection.SelectedItem.CombatDistance
            - m_playerCombatInfo.m_attackPinwheel.GetSelectedOption().m_moveDistance //this works
            + ResolveFishDirection(m_FishSelection.SelectedItem) * m_playerCombatInfo.m_attackPinwheel.GetSelectedOption().m_slow;


        //ACT
        PlayerAttack();


        //All fish should have finished their turn within one frame.


        //ASSERT
        //check fish if fish position was affected properly.
        //should be 10 - 2(move.distance) +/- 4(fish.moveSpeed.)

        //test this elswhere.
        Assert.AreEqual(m_FishSelection[0].CombatDistance, m_FishSelection.SelectedItem.CombatDistance);
        Assert.AreEqual(distanceAfterAttack, (float) m_FishSelection.SelectedItem.CombatDistance);
        //Assert.AreEqual(m_FishSelection[1].m_combatDistance, SelectedFish.m_combatDistance);



        //check health on fish, should be 90 after the first attack.
        Assert.IsTrue(m_FishSelection.SelectedItem.FishInstance.Health.Max - moves[0].m_damage == m_FishSelection.SelectedItem.FishInstance.Health.CurrentAmount);

        //check that the queue is what expected.
        //the player turn is dequeued when awaiting input so 3 fish is in queue.
        Assert.AreEqual(m_FishSelection.Count, m_roundQueue.Count);
        //Should be awaiting player input...
        Assert.IsTrue(m_currentCombatState == CombatStates.AwaitingPlayerRound);
    }

    [Test]
    public void D_ChangeSelectedFish_Test()
    {
        m_FishSelection.SetSelection(0);

        //ie. went around the list forwards once.
        for (int i = 0; i < m_FishSelection.Count; i++)
        {
            Assert.AreEqual(i, m_FishSelection.Selection);
            Assert.AreEqual(m_FishSelection.SelectedItem, m_FishSelection[i]);

            ChangeSelectedFish(1);
        }

        Assert.AreEqual(0, m_FishSelection.Selection);
        Assert.AreEqual(m_FishSelection.SelectedItem, m_FishSelection[0]);

        ChangeSelectedFish(-1);
        //ie. went around the list backwards once.
        for (int i = m_FishSelection.Count - 1; i >= 0; i--)
        {
            Assert.AreEqual(i, m_FishSelection.Selection);
            Assert.AreEqual(m_FishSelection.SelectedItem, m_FishSelection[i]);

            ChangeSelectedFish(-1);
        }

        Assert.AreEqual(m_FishSelection.Count - 1, m_FishSelection.Selection);
    }

}
