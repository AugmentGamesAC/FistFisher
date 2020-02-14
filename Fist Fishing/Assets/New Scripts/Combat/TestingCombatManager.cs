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
    protected FishDefintion m_f5Fish;
    [SerializeField]
    protected FishDefintion m_f6Fish;
    [SerializeField]
    protected FishDefintion m_f7Fish;
    [SerializeField]
    protected AllFishUIUpdater m_showThemALl;
    protected AttackPinwheelUpdater m_attackPinwheelUpdater;
    [SerializeField]

    private void Start()
    {
        //List<CombatMoveInfo> moves = new List<CombatMoveInfo>
        //{
        //    new CombatMoveInfo(10, 0.5f, 30, 2, 25),
        //    new CombatMoveInfo(5, 0, 2, 3, 12),
        //    new CombatMoveInfo(45, 0, 0, 10, 10)
        //};

        //m_playerCombatInfo.m_attackPinwheel = new PinwheelTracker<CombatMoveInfo>(1, moves);
        m_playerCombatInfo = new PlayerCombatInfo(ScriptablePlayerMoves);

        m_attackPinwheelUpdater = GetComponentInChildren<AttackPinwheelUpdater>();

        if (m_attackPinwheelUpdater == null)
            throw new System.Exception(string.Format("{0} not working", m_attackPinwheelUpdater));

        m_attackPinwheelUpdater.UpdateTracker(m_playerCombatInfo.m_attackPinwheel);
        m_playerCombatInfo.m_attackPinwheel.SetSelectedOption(1);

        m_showThemALl.ForceUpdate(default);
       // m_lessshowyFish.UpdateUI(default);
    }

    [ContextMenu("CombatYeast/Player Started F5 Fish battle")]
    public void newFishF5True() { StartCombatTest(new[] { m_f5Fish }, true); }
    [ContextMenu("CombatYeast/Player Started F6 Fish battle")]
    public void newFishF6True() { StartCombatTest(new[] { m_f6Fish }, true); }
    [ContextMenu("CombatYeast/Player Started F7 Fish battle")]
    public void newFishF7True() { StartCombatTest(new[] { m_f7Fish }, true); }
    [ContextMenu("CombatYeast/Fish Started F5 Fish battle")]
    public void newFishF5False() { AddFishTest( m_f5Fish); }
    [ContextMenu("CombatYeast/Fish Started F6 Fish battle")]
    public void newFishF6False() { AddFishTest( m_f6Fish ); }
    [ContextMenu("CombatYeast/Fish Started F7 Fish battle")]
    public void newFishF7False() { AddFishTest(m_f7Fish ); }


    protected void AddFishTest(FishDefintion fish)
    {
        ResolveAddFish(new FishCombatInfo(new FishInstance(fish)));
    }

    protected void StartCombatTest(IEnumerable<FishDefintion> fishDefs, bool wasPlayer)
    {
        NewMenuManager.DisplayMenuScreen(MenuScreens.Combat);


        base.StartCombat(wasPlayer, fishDefs.Select(X => new FishInstance(X)) );
        m_showyFish.UpdateUI(m_FishSelection.SelectedItem);
    }


    public new void Update()
    {
        base.Update();

        if (ALInput.GetKeyDown(KeyCode.F5))
            newFishF5True();
        if (ALInput.GetKeyDown(KeyCode.F6))
            newFishF6True();
        if (ALInput.GetKeyDown(KeyCode.F7))
            newFishF7True();
        if (ALInput.GetKeyDown(KeyCode.F9))
            newFishF5False();
        if (ALInput.GetKeyDown(KeyCode.F10))
            newFishF6False();
        if (ALInput.GetKeyDown(KeyCode.F11))
            newFishF7False();


        if (NewMenuManager.CurrentMenu != MenuScreens.Combat)
            return;

        //if ((ALInput.GetKeyDown(ALInput.Punch)))
        //    PlayerAttack();
    }
    // m_fishInCombatInfo[0].TakeDamage(10);
}
