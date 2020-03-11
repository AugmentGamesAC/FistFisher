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

    public enum noiseThreshold
    {
        Quiet,
        Small,
        Medium,
        Large
    }


    private static CombatManager m_instance;
    public static CombatManager Instance => m_instance;
    public void Awake()
    {
        if (CombatManager.m_instance == null)
            CombatManager.m_instance = this;
    }

    [System.Serializable]
    public class NoiseThresholdsDict : InspectorDictionary<noiseThreshold, float> { }
    [SerializeField]
    protected NoiseThresholdsDict m_noiseThresholds = new NoiseThresholdsDict();
    public NoiseThresholdsDict NoiseThresholds => m_noiseThresholds;


    [SerializeField]
    protected noiseThreshold m_currentNoiseState = noiseThreshold.Quiet;
    public noiseThreshold CurrentNoiseState => m_currentNoiseState;

    [SerializeField]
    protected bool m_isItemActive;
    protected Bait_IItem m_Baititem;
    protected Repellent_IItem m_RepellentItem;

    [SerializeField]
    protected List<FishCombatInfo> m_aggressiveFishToSpawn = new List<FishCombatInfo>();
    [SerializeField]
    protected List<FishCombatInfo> m_deadFishPile = new List<FishCombatInfo>();

    [SerializeField]
    protected List<CombatMoveInfo> ScriptablePlayerMoves = new List<CombatMoveInfo>();

    //NEED TO DISCUSS THIS IITEM CANT BE ADDED TO IN INSPECTOR DUE TO LACK OF MONOBEHAVIOUR
    [SerializeField]
    protected List<Bait_IItem> ScriptableItems = new List<Bait_IItem>();



    protected float m_maxSpawnChance = 0;
    public float MaxSpawnChance => m_maxSpawnChance;

    protected Queue<CombatInfo> m_roundQueue = new Queue<CombatInfo>();

    protected PlayerCombatInfo m_playerCombatInfo;

    [SerializeField]
    protected CombatStates m_currentCombatState = CombatStates.OutofCombat;


    protected float m_combatZone = 30.0f;

    protected PlayerMotion m_player;


    private void Start()
    {
        /// m_playerCombatInfo.NoiseTracker.OnCurrentAmountChanged += ResolveNoiseChange;



        m_playerCombatInfo = new PlayerCombatInfo(ScriptablePlayerMoves);
        if (m_playerCombatInfo == null)
            throw new System.InvalidOperationException();

        foreach (var fish in m_aggressiveFishToSpawn)
        {
            //m_maxSpawnChance += fish.SpawnChance;
        }
    }

    public void StartCombat(bool didPlayerStartIt, IEnumerable<FishInstance> fishes, PlayerMotion player = default(PlayerMotion))
    {

        PlayerInstance.Instance.PromptManager.HideCurrentPrompt();
        //getDepending on biome, fill aggressive fish dictionary with different fishCombatInfo.
        //ResolveAggressiveFishes(Biome biomeType)
        PlayerInstance.Instance.Health.OnMinimumAmountReached -= EndCombat;
        PlayerInstance.Instance.Health.OnMinimumAmountReached += EndCombat;

        if (m_playerCombatInfo == null)
            throw new System.InvalidOperationException();


        NewMenuManager.DisplayMenuScreen(MenuScreens.Combat);

        m_player = player;

        //Clear the list from previous battles just in case player didn't grab em all.
        m_deadFishPile.Clear();

        m_FishSelection.AddItems(fishes.Select(x => new FishCombatInfo(x)));

        if (didPlayerStartIt)
        {
            m_roundQueue.Enqueue(m_playerCombatInfo);
            m_currentCombatState = CombatStates.AwaitingPlayerRound;
        }

        AddFishToQueue();

        if (!didPlayerStartIt)
        {
            m_roundQueue.Enqueue(m_playerCombatInfo);
            m_currentCombatState = CombatStates.AwaitingFishRound;
        }
        ResolveRound();
    }


    protected SingleSelectionListTracker<FishCombatInfo> m_FishSelection = new SingleSelectionListTracker<FishCombatInfo>();
    [SerializeField]
    protected DisappearingMenu m_puchingUI;

    protected void AddFishToQueue()
    {
        for (int i = 0; i < m_FishSelection.Count; i++)
        {
            m_roundQueue.Enqueue(m_FishSelection[i]);
        }
    }
    [SerializeField]
    protected float m_keypressTimeout;
    [SerializeField]
    protected float m_keypressTimeoutDuration;


    // Update is called once per frame
    protected void Update()
    {
        //can switch targets even when fish are attacking.
        ChangeSelectedFish((ALInput.GetKeyDown(KeyCode.O) ? 1 : 0) - (ALInput.GetKeyDown(KeyCode.P) ? 1 : 0));


        if (m_keypressTimeout > 0)
            m_keypressTimeout -= Time.deltaTime;

        //listen to inputs only during AwaitingPlayerRound.
        if (m_currentCombatState != CombatStates.AwaitingPlayerRound)
            return;

        //listen for input cases.
        //5 input cases, attack, flee, item, 1 axis for m_selectedFish swapping. (toggle left, right)
        if (ALInput.GetKeyDown(KeyCode.F))
        {
            PlayerAttack();
        }
        if (!m_isItemActive)
            if (ALInput.GetKeyDown(ALInput.Item))
            {
                PlayerItem();
            }

        if (ALInput.GetKeyDown(ALInput.Flee))
        {
            PlayerFlee();
        }

        int newselection = PinWheel<CombatMoveInfo>.TwoDToSelection
        (
        (ALInput.GetKey(ALInput.GoRight) ? 1 : 0) - (ALInput.GetKey(ALInput.GoLeft) ? 1 : 0),
        (ALInput.GetKey(ALInput.Forward) ? 1 : 0) - (ALInput.GetKey(ALInput.Backward) ? 1 : 0)
        );
        if (newselection != 0 && m_keypressTimeout <= 0)
        {
            m_playerCombatInfo.m_attackPinwheel.SetSelectedOption(newselection);
            if (m_puchingUI != default)
                m_puchingUI.WasKeyed();

            m_keypressTimeout = m_keypressTimeoutDuration;
        }


    }

    protected void PlayerFlee()
    {
        //Take away player's oxygen.
        m_playerCombatInfo.UpdateOxygen(-30);
        //remove last caught fish from inventory.

        m_player.m_CanMove = true;
        NewMenuManager.DisplayMenuScreen(MenuScreens.NormalHUD);

        EndCombat();
    }

    /// <summary>
    /// Grabs selected Move from the player's attackPinwheel.
    /// </summary>
    protected void PlayerAttack()
    {
        //Get the player's current pinwheel choice.
        CombatMoveInfo move = m_playerCombatInfo.m_attackPinwheel.GetSelectedOption();

        //apply stat changes to the player.
        //Oxygen
        // noise .
        m_playerCombatInfo.UpdateOxygen(-move.OxygenConsumption);
        m_playerCombatInfo.UpdateNoise(move.Noise);


        //apply damage from the player's move to the selected fish.
        MoveFishes(move.MoveDistance);

        m_FishSelection.SelectedItem.TakeDamage(move.Damage);
        m_FishSelection.SelectedItem.SlowDown(move.Slow);

        m_roundQueue.Enqueue(m_playerCombatInfo);

        ResolveRound();
    }

    /// <summary>
    /// Grabs selected item from the player's itemPinwheel.
    /// </summary>
    protected void PlayerItem()
    {

        //Set bool that item is now in play
        m_isItemActive = true;

        //FOR TESTING SET ITEM TO BAIT OR REPELLENT
        m_Baititem = (Bait_IItem)ScriptableItems[0];
      //  m_RepellentItem = (Repellent_IItem)ScriptableItems[1];

        //Apply effect to the combat (None yet till pinwheel).

        //Get the player's current pinwheel choice.

        //m_currentCombatState = CombatStates.AwaitingPlayerAnimation;
        //Enqueue for next round
        m_roundQueue.Enqueue(m_playerCombatInfo);
        //this happens after stat and State changes.
        ResolveRound();
    }

    /// <summary>
    /// moves selected fish closer and others further by distance amount.
    /// </summary>
    /// <param name="distance"></param>
    protected void MoveFishes(float distance)
    {
        //Increase distance to other fish.
        for (int i = 0; i < m_FishSelection.Count; i++)
        {
            bool isTheSelectedOne = (i == m_FishSelection.Selection);
            float value = Mathf.Max(0, m_FishSelection[i].CombatDistance + (isTheSelectedOne ? -distance : distance));
            m_FishSelection[i].CombatDistance.SetValue(value);
        }
    }
    /// <summary>
    /// Attacks, Moves or and checks if it left combat.
    /// </summary>
    /// <param name=""></param>
    protected void ResolveFishCombatant(FishCombatInfo fishCombatInfo)
    {
        fishCombatInfo.CombatDistance.SetValue(Mathf.Max(0, fishCombatInfo.CombatDistance + ResolveFishDirection(fishCombatInfo)));
        ResolveFishAttack(fishCombatInfo);

        //if the fish doesn't leave the battle, enqueue for next turn.
        if (!ResolveLeaveCombat(fishCombatInfo) && m_currentCombatState != CombatStates.CombatFinished)
            m_roundQueue.Enqueue(fishCombatInfo);

        fishCombatInfo.ResetMoveSpeed();

        ResolveRound();
    }

    protected bool ResolveOutOfRange(FishCombatInfo fish)
    {
        if (fish.CombatDistance <= m_combatZone)
            return false;
        m_FishSelection.Remove(fish);
        return true;
    }

    /// <summary>
    /// returns true if fish is out of range or dead.
    /// </summary>
    /// <param name="fish"></param>
    /// <returns></returns>
    protected bool ResolveLeaveCombat(FishCombatInfo fish)
    {
        if (ResolveOutOfRange(fish))
            return true;

        if (ResolveDeadFish(fish))
            return true;

        return false;
    }


    /// <summary>
    /// needs new work with inventory.
    /// returns true if fish's health is 0 or below.
    /// </summary>
    /// <param name="fishCombatInfo"></param>
    /// <returns></returns>
    protected bool ResolveDeadFish(FishCombatInfo fishCombatInfo)
    {
        if (fishCombatInfo.FishInstance.Health.CurrentAmount > 0)
            return false;

        PlayerInstance.Instance.PromptManager.DeregisterPrompt(fishCombatInfo.FishInstance.FishPrompt);

        m_deadFishPile.Add(fishCombatInfo);
        m_FishSelection.Remove(fishCombatInfo);
        return true;
    }

    protected void ResolveFishAttack(FishCombatInfo fish)
    {
        if (fish.FishInstance.FishData.AttackRange > fish.CombatDistance)
        {
            m_playerCombatInfo.TakeDamage(fish.FishInstance.FishData.Damage);
        }
    }


    protected float ResolveFishDirection(FishCombatInfo fish)
    {
        //To be changed once pinwheels playercombatinfo is finished

        //BAIT TESTING
        //Check if there is an item (bait) & if the fish is sensitive to the item (bait)
        if (m_isItemActive != false && (fish.FishInstance.FishData.FishClassification.HasFlag(m_Baititem.BaitType)))
        {
            Debug.Log("Fish Moved Toward Bait");
            //decrease the active turns by 1
         
            //update fish direction using the bait direction 
            fish.ChangeDirection(-1);
            //Check if item should still be active after this turn
            m_isItemActive = m_Baititem.IsStillActive();
            return fish.Speed;
        }
        //REPELLENT TESTING
        //Check if there is an item (repell) & if the fish is sensitive to the item (repell)
        if (m_isItemActive != false && (fish.FishInstance.FishData.FishClassification.HasFlag(m_RepellentItem.RepellentType)))
        {
            Debug.Log("Fish Moved Toward Bait");
            //decrease the active turns by 1

            //update fish direction using the bait direction 
            fish.ChangeDirection(1);
            //Check if item should still be active after this turn
            m_isItemActive = m_RepellentItem.IsStillActive();
            return fish.Speed;
        }

        Debug.Log("Fish Moved due to behaviour");
        return (fish.FishInstance.FishData.FishClassification.HasFlag(FishBrain.FishClassification.Aggressive)) ? -fish.Speed : fish.Speed;
    }

    protected void ResolveAddFish(FishCombatInfo fish)
    {
        m_roundQueue.Enqueue(fish);
        m_FishSelection.AddItem(fish);
    }

    /// <summary>
    /// Gets called before player's turn. 
    /// Spawns fish if RNG allows it.
    /// </summary>
    protected bool ResolveAggressiveFishSpawn()
    {
        if (CurrentNoiseState == noiseThreshold.Quiet)
            return false;

        //check spawn chances of each aggressive fish. combatfishinfo needs spawn chance for this to work.
        //spawn one fish if the RNG says so.

        //do we span a fish at all
        float RNGNumber = Random.Range(0.0f, 5);

        throw new System.NotImplementedException();

        //float spawnThreshold = Mathf.Log(5, m_playerCombatInfo.NoiseTracker);

        //if (RNGNumber < 0/*spawnThreshold*/)
        //    return false;

        ////a fish spawns

        //float whichfish = Random.Range(0, MaxSpawnChance);  //

        //foreach (var fish in m_aggressiveFishToSpawn)
        //{
        //    //whichfish -= fish.SpawnChance;
        //    if (whichfish < 0)
        //    {
        //        m_roundQueue.Enqueue(fish);
        //        return true;
        //    }
        //}


        //return false;
    }

    /// <summary>
    /// Spawns aggressive fish depending on noise amount.
    /// Gets called when noise change delegate gets invoked.
    /// </summary>
    protected void ResolveNoiseChange()
    {
        throw new System.NotImplementedException();

        //foreach (var threshold in m_noiseThresholds)
        //{
        //    if (m_playerCombatInfo.NoiseTracker.CurrentAmount > threshold.Value)
        //    {
        //        m_currentNoiseState = threshold.Key;
        //        break;
        //    }
        //}
    }

    /// <summary>
    /// Update the round Queue.
    /// </summary>
    protected void ResolveRound()
    {
        if (m_currentCombatState == CombatStates.CombatFinished)
            return;
        if (PlayerWon())
            return;
        if (FishWon())
            return;


        //get current combabant and remove from queue.
        CombatInfo nextCombatant = m_roundQueue.Dequeue();

        //check for next combatant type and change the state. check if a fish will spawn.
        if (nextCombatant as PlayerCombatInfo != null)
        {
            //ResolveAggressiveFishSpawn();
            m_currentCombatState = CombatStates.AwaitingPlayerRound;
            return;
        }

        m_currentCombatState = CombatStates.AwaitingFishRound;

        ResolveFishCombatant(nextCombatant as FishCombatInfo);
    }

    protected bool PlayerWon()
    {
        if (m_roundQueue.Count == 1 && m_roundQueue.Peek() == m_playerCombatInfo)
        {
            foreach (var deadFish in m_deadFishPile)
            {
                PlayerInstance.Instance.PlayerInventory.AddItem(deadFish.FishInstance.FishData.Item, 1);
            }
            m_deadFishPile.Clear();
            m_player.m_CanMove = true;
            NewMenuManager.DisplayMenuScreen(MenuScreens.NormalHUD);
            EndCombat();
            return true;
        }
        return false;
    }

    protected bool FishWon()
    {
        if (!m_roundQueue.Contains(m_playerCombatInfo))
        {
            EndCombat();
            return true;
        }
        return false;
    }

    protected void EndCombat()
    {
        m_currentCombatState = CombatStates.CombatFinished;

        m_FishSelection.Clear();
        m_roundQueue.Clear();
    }

    /// <summary>
    /// return true if selected fish is successful.
    /// </summary>
    protected void ChangeSelectedFish(float leftRight)
    {
        //no axis input? do nothing.
        if ((leftRight == 0) || (m_FishSelection.Count == 0))
            return;

        m_FishSelection.IncrementSelection((int)leftRight);
    }

    //Plays aninmation
    protected IEnumerable StartPlayerAttackAnimation(CombatMoveInfo move)
    {
        m_currentCombatState = CombatStates.AwaitingPlayerAnimation;

        //Play animation
        //wait for animation length
        //change combat state to next.

        yield return null;
    }

    protected IEnumerable StartFishAnimation()
    {
        m_currentCombatState = CombatStates.AwaitingFishAnimation;

        //Play animation
        //wait for animation length
        //change combat state to next.

        yield return null;
    }

}
