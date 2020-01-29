using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Should be Singleton
/// handles list of combatants and resolves moves/move order
/// </summary>
public class CombatManager : MonoBehaviour
{
    public enum CombatStates
    {
        OutofCombat,
        AwaitingPlayerRound,
        AwaitingFishRound,
        AwaitingPlayerAnimation,
        AwaitingFishAnimation,
        CombatFinished
    }

    Queue<CombatInfo> m_roundQueue = new Queue<CombatInfo>();

    List<FishCombatInfo> m_fishInCombatInfo = new List<FishCombatInfo>();

    PlayerCombatInfo m_playerCombatInfo = new PlayerCombatInfo();

    CombatStates m_currentCombatState = CombatStates.OutofCombat;

    FishCombatInfo m_selectedFish = new FishCombatInfo();

    //gets controlled by left right inputs.
    int m_fishSelection = 0;

    public FishCombatInfo SelectedFish {  get { return m_fishInCombatInfo[m_fishSelection]; } }

    public void StartCombat(bool didPlayerStartIt)
    {
        m_currentCombatState = (didPlayerStartIt) ? CombatStates.AwaitingPlayerRound : CombatStates.AwaitingFishRound;

        if (didPlayerStartIt)
            m_roundQueue.Enqueue(m_playerCombatInfo);

        AddFishToQueue();

        if (!didPlayerStartIt)
        {
            m_roundQueue.Enqueue(m_playerCombatInfo);
            ResolveFishCombatant(m_roundQueue.Dequeue() as FishCombatInfo);
        }
    }

    void AddFishToQueue()
    {
        foreach (var fishInfo in m_fishInCombatInfo)
        {
            m_roundQueue.Enqueue(fishInfo);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //can switch targets even when fish are attacking.
        ChangeSelectedFish(ALInput.GetAxis(ALInput.AxisCode.Horizontal));

        //listen to inputs only during AwaitingPlayerRound.
        if (m_currentCombatState != CombatStates.AwaitingPlayerRound)
            return;

        //listen for input cases.
        //5 input cases, attack, flee, item, 1 axis for m_selectedFish swapping. (toggle left, right)
        if (ALInput.GetKeyDown(ALInput.Attack))
        {
            PlayerAttack();
        }

        if (ALInput.GetKeyDown(ALInput.Item))
        {
            PlayerItem();
        }

        if (ALInput.GetKeyDown(ALInput.Flee))
        {
            PlayerFlee();
        }
    }


    public void PlayerFlee()
    {
        //Take away player's oxygen.

        //remove last caught fish from inventory.

        //Transition from AwaitingPlayerRound to outOfCombat
        m_currentCombatState = CombatStates.CombatFinished;

        throw new System.NotImplementedException("No End Combat situation");

        ResolveRound();
    }

    /// <summary>
    /// Grabs selected Move from the player's attackPinwheel.
    /// </summary>
    public void PlayerAttack()
    {
        //Get the player's current pinwheel choice.
        CombatMoveInfo move = m_playerCombatInfo.m_attackPinwheel.GetSelectedOption();

        StartCoroutine("StartPlayerAttackAnimation", move);

        //reduce distance from selected fish.
        SelectedFish.Distance -= move.m_moveDistance;
        //Increase distance to other fish.
        foreach (var fishCombatInfo in m_fishInCombatInfo)
        {
            if (fishCombatInfo == SelectedFish)
                continue;
            fishCombatInfo.Distance += move.m_moveDistance;
        }
        

        throw new System.NotImplementedException(" dependant on fish Instance.");

        //apply stat changes to the player. eg. oxygen.
            

        //apply damage from the player's move to the selected fish.


        ResolveRound();
    }

    /// <summary>
    /// Grabs selected item from the player's itemPinwheel.
    /// </summary>
    public void PlayerItem()
    {
        throw new System.NotImplementedException("dependency items implementation.");

        //Get the player's current pinwheel choice.

        //Apply effect to the combat.

        m_currentCombatState = CombatStates.AwaitingPlayerAnimation;

        //this happens after stat and State changes.
        ResolveRound();
    }

    /// <summary>
    /// Determines whether the fish is alive or out of combat.
    /// </summary>
    /// <param name=""></param>
    public void ResolveFishCombatant(FishCombatInfo fishCombatInfo)
    {
        throw new System.NotImplementedException("dependency on combat position and fish Instance.");

        StartCoroutine("StartFishAnimation");

        //check fishCombatInfo.FishInstance for Health, position.
    }

    /// <summary>
    /// Update the round Queue.
    /// </summary>
    private void ResolveRound() //fish disapear etc, reload if empty
    {
        throw new System.NotImplementedException("Dependant on fish stats.");

        //checks combat State.

        //determine next turn.

    }

    /// <summary>
    /// return true if selected fish is successful.
    /// </summary>
    private bool ChangeSelectedFish(float leftRight)
    {
        //no axis input? do nothing.
        if ((leftRight == 0) || (m_fishInCombatInfo.Count == 0))
            return false;

        m_fishSelection += (int)leftRight;
        m_fishSelection %= m_fishInCombatInfo.Count;

        if (m_fishSelection < 0)
            m_fishSelection += m_fishInCombatInfo.Count;

        return true;
    }

    //Plays aninmation
    private IEnumerable StartPlayerAttackAnimation(CombatMoveInfo move)
    {
        m_currentCombatState = CombatStates.AwaitingPlayerAnimation;

        //Play animation
        //wait for animation length
        //change combat state to next.

        yield return null;
    }

    private IEnumerable StartFishAnimation()
    {
        m_currentCombatState = CombatStates.AwaitingFishAnimation;

        //Play animation
        //wait for animation length
        //change combat state to next.

        yield return null;
    }
}
