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
        }
        ResolveRound();
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
        m_playerCombatInfo.UpdateOxygen(-30);
        //remove last caught fish from inventory.


        EndCombat();
    }

    /// <summary>
    /// Grabs selected Move from the player's attackPinwheel.
    /// </summary>
    public void PlayerAttack()
    {
        //Get the player's current pinwheel choice.
        CombatMoveInfo move = m_playerCombatInfo.m_attackPinwheel.GetSelectedOption();

        StartCoroutine("StartPlayerAttackAnimation", move);

        //apply stat changes to the player. eg. oxygen.
        // oxegen
        // noise - don't worry about this one yet.

        m_playerCombatInfo.UpdateOxygen(move.m_oxygenConsumption);


        //apply damage from the player's move to the selected fish.
        MoveFishes(move.m_moveDistance);
        SelectedFish.TakeDamage(move.m_damage);
        SelectedFish.SlowDown(move.m_slow);

        m_roundQueue.Enqueue(m_playerCombatInfo);

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


    protected void MoveFishes(float distance)
    {
        //Increase distance to other fish.
        foreach (var fishCombatInfo in m_fishInCombatInfo)
            fishCombatInfo.m_combatDistance += (fishCombatInfo == SelectedFish) ? -distance : distance;
    }
    /// <summary>
    /// Determines whether the fish is alive or out of combat.
    /// </summary>
    /// <param name=""></param>
    public void ResolveFishCombatant(FishCombatInfo fishCombatInfo)
    {
        if (ResolveDeadFish(fishCombatInfo))
            return;

        fishCombatInfo.m_combatDistance += ResolveFishDirection(fishCombatInfo);
        ResolveFishAttack(fishCombatInfo);

        m_roundQueue.Enqueue(fishCombatInfo);
        ResolveRound();
    }

    public bool ResolveDeadFish(FishCombatInfo fishCombatInfo)
    {
        return (fishCombatInfo.FishData.Health.CurrentAmount > 0);
    }

    protected void ResolveFishAttack(FishCombatInfo fish)
    {
        if (fish.FishData.AttackRange > fish.m_combatDistance)
            m_playerCombatInfo.TakeDamage(fish.FishData.Damage);
    }


    public float ResolveFishDirection(FishCombatInfo fish)
    {
        return (fish.FishData.FishClassification.HasFlag(FishBrain.FishClassification.Agressive) )? -fish.FishData.CombatSpeed : fish.FishData.CombatSpeed; 
    }


    /// <summary>
    /// Update the round Queue.
    /// </summary>
    private void ResolveRound() 
    {
        //player wins if he's the only one in the queue
        if (m_roundQueue.Count == 1)
        {
            EndCombat();
            return;
        }
        

        CombatInfo nextCombatant = m_roundQueue.Dequeue();

        if (nextCombatant as PlayerCombatInfo != null)
        {
            m_currentCombatState = CombatStates.AwaitingPlayerRound;
            return;
        }

        m_currentCombatState = CombatStates.AwaitingFishRound;
        ResolveFishCombatant(nextCombatant as FishCombatInfo);
    }

    protected void EndCombat()
    {
        m_currentCombatState = CombatStates.CombatFinished;
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
