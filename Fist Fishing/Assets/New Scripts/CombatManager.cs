using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Should be Singleton
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

    PlayerCombatInfo m_player = new PlayerCombatInfo();

    CombatStates m_currentCombatState = CombatStates.OutofCombat;

    FishCombatInfo m_selectedFish = new FishCombatInfo();

    int m_fishSelection = 0;

    public FishCombatInfo SelectedFish { get { return m_fishInCombatInfo[m_fishSelection]; } }

    public void StartCombat(bool didPlayerStartIt)
    {
        m_currentCombatState = (didPlayerStartIt) ? CombatStates.AwaitingPlayerRound : CombatStates.AwaitingFishRound;

        if (didPlayerStartIt)
            m_roundQueue.Enqueue(m_player);

        AddFishToQueue();

        if (!didPlayerStartIt)
        {
            m_roundQueue.Enqueue(m_player);
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
        //listen to combat during AwaitingPlayerRound.

        if (m_currentCombatState != CombatStates.AwaitingPlayerRound)
            return;

        //listen to input cases.
        //5 input cases, attack, flee, item, 1 axis for m_selectedFish swapping.

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

        changeselectedfish(ALInput.GetAxis(ALInput.AxisCode.Horizontal));
    }

    public void ResolvePlayerRound()
    {







        //move has happened so resolve round.



    }


    public void PlayerFlee()
    {

    }

    /// <summary>
    /// Grabs selected Move from the player's attackPinwheel.
    /// </summary>
    public void PlayerAttack()
    {
        //Get the player's current pinwheel choice.

        //apply stat changes to selected and the player.

        //apply stat changes to the player. eg. oxygen, position on the field.


    }

    /// <summary>
    /// Grabs selected item from the player's itemPinwheel.
    /// </summary>
    public void PlayerItem()
    {

    }

    /// <summary>
    /// Determines whether the fish is alive or out of combat.
    /// </summary>
    /// <param name=""></param>
    public void ResolveFishCombatant(FishCombatInfo fishCombatInfo)
    {
        //check fishCombatInfo.FishInstance for Health, position.

        //if health is below 0, add to the player's inventory.

        //if position is out of combat

        //enqueue fish.
    }

    /// <summary>
    /// Update the round Queue.
    /// </summary>
    public void ResolveRound() //fish disapear etc, reload roundqueue
    {
        //checks combat State.
        //AwaitingPlayerRound: call ResolvePlayerRound();
        //AwaitingFishRound: call ResolveFishCombatant(currentQueued);



        //evaluate the fish list's stats, and the player's stats.
        //ResolveFishCombatant(/*Selected fish*/);
    }

    /// <summary>
    /// return true if selected fish is successful.
    /// </summary>
    /// <param name="leftRight">-1 or +1 is left or right</param>
    /// <returns></returns>
    public bool changeselectedfish(float leftRight)
    {
        //no axis input? do nothing.
        if ((leftRight == 0) ||( m_fishInCombatInfo.Count == 0))
            return false;

        m_fishSelection += (int)leftRight;
        m_fishSelection %= m_fishInCombatInfo.Count;

        if (m_fishSelection < 0)
            m_fishSelection += m_fishInCombatInfo.Count;

        return true;
    }

    public void AwaitPlayerAnimation()
    {

    }

    public void AwaitFishAnimation()
    {

    }
}
