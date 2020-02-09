using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;

/// <summary>
/// Should be Singleton
/// handles list of combatants and resolves moves/move order
/// </summary>
public class TestingCombatManager : CombatManager
{
    [SerializeField]
    protected FishDefintion m_fish;
    [SerializeField]
    protected SelectedFishUI m_showyFish;

    private void Start()
    {
        List<CombatMoveInfo> moves = new List<CombatMoveInfo>
        {
            new CombatMoveInfo(10, 0.5f, 30, 2, 25),
            new CombatMoveInfo(5, 0, 2, 3, 12),
            new CombatMoveInfo(45, 0, 0, 10, 10)
        };

        m_playerCombatInfo.m_attackPinwheel = new PinwheelTracker<CombatMoveInfo>(1, moves);
        m_playerCombatInfo.m_attackPinwheel.SetSelectedOption(1);
    }

    [ContextMenu("Create Test Fish")]
    public void newFish()
    {
        //Create new fish data
        FishCombatInfo NewFish = new FishCombatInfo(new FishInstance(m_fish));
        m_FishSelection.AddItem(NewFish);
        m_showyFish.UpdateUI(NewFish);
        m_roundQueue.Enqueue(NewFish);
    }

    public new void Update()
    {
        base.Update();

        if (NewMenuManager.CurrentMenu != MenuScreens.Combat)
            return;

        if (ALInput.GetKeyDown(ALInput.Start))
        {
            newFish();
            StartCombat(true);
        }
        //if ((ALInput.GetKeyDown(ALInput.Punch)))
        //    PlayerAttack();
    }
    // m_fishInCombatInfo[0].TakeDamage(10);
}
