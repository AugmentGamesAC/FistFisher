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


    [System.Serializable]
    public class NoiseThresholdsDict : InspectorDictionary<float, noiseThreshold> { }
    [SerializeField]
    protected NoiseThresholdsDict m_noiseThresholds = new NoiseThresholdsDict();
    public NoiseThresholdsDict NoiseThresholds => m_noiseThresholds;

    [SerializeField]
    protected noiseThreshold m_currentNoiseState = noiseThreshold.Quiet;
    public noiseThreshold CurrentNoiseState => m_currentNoiseState;

    [SerializeField]
    protected List<FishCombatInfo> m_aggressiveFishToSpawn = new List<FishCombatInfo>();

    protected float m_maxSpawnChance = 0;
    public float MaxSpawnChance => m_maxSpawnChance;

    protected Queue<CombatInfo> m_roundQueue = new Queue<CombatInfo>();

    protected List<FishCombatInfo> m_fishInCombatInfo = new List<FishCombatInfo>();

    protected PlayerCombatInfo m_playerCombatInfo = new PlayerCombatInfo();

    protected CombatStates m_currentCombatState = CombatStates.OutofCombat;

    //gets controlled by left right inputs.
    protected int m_fishSelection = 0;

    protected float m_combatZone = 30.0f;

    public FishCombatInfo SelectedFish { get { return m_fishInCombatInfo[m_fishSelection]; } }

    private void Start()
    {
        m_playerCombatInfo.NoiseTracker.OnCurrentAmountChanged += ResolveNoiseChange;

        foreach (var fish in m_aggressiveFishToSpawn)
        {
            m_maxSpawnChance += fish.SpawnChance;
        }
    }
    
    public void StartCombat(bool didPlayerStartIt)
    {
        //getDepending on biome, fill aggressive fish dictionary with different fishCombatInfo.
        //ResolveAggressiveFishes(Biome biomeType)

        if (didPlayerStartIt)
            m_roundQueue.Enqueue(m_playerCombatInfo);

        AddFishToQueue();

        if (!didPlayerStartIt)
        {
            m_roundQueue.Enqueue(m_playerCombatInfo);
        }
        ResolveRound();
    }

    protected void AddFishToQueue()
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


    protected void PlayerFlee()
    {
        //Take away player's oxygen.
        m_playerCombatInfo.UpdateOxygen(-30);
        //remove last caught fish from inventory.


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
        m_playerCombatInfo.UpdateOxygen(move.m_oxygenConsumption);
        m_playerCombatInfo.UpdateNoise(move.m_noise);


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
    protected void PlayerItem()
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
    protected void ResolveFishCombatant(FishCombatInfo fishCombatInfo)
    {

        fishCombatInfo.m_combatDistance += ResolveFishDirection(fishCombatInfo);
        ResolveFishAttack(fishCombatInfo);

        //if the fish doesn't leave the battle, enqueue for next turn.
        if (!ResolveLeaveCombat(fishCombatInfo))
            m_roundQueue.Enqueue(fishCombatInfo);


        ResolveRound();
    }

    ///returns true if out of range or dead.
    protected bool ResolveLeaveCombat(FishCombatInfo fish)
    {
        if (fish.m_combatDistance > m_combatZone)
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
        return (fishCombatInfo.FishData.Health.CurrentAmount <= 0);
    }

    protected void ResolveFishAttack(FishCombatInfo fish)
    {
        if (fish.FishData.AttackRange > fish.m_combatDistance)
            m_playerCombatInfo.TakeDamage(fish.FishData.Damage);
    }


    protected float ResolveFishDirection(FishCombatInfo fish)
    {
        return (fish.FishData.FishClassification.HasFlag(FishBrain.FishClassification.Agressive)) ? -fish.FishData.CombatSpeed : fish.FishData.CombatSpeed;
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

        float spawnThreshold = Mathf.Log(5, m_playerCombatInfo.NoiseTracker);

        if (RNGNumber < spawnThreshold)
            return false;

        //a fish spawns

        float whichfish = Random.Range(0, MaxSpawnChance);  //

        foreach (var fish in m_aggressiveFishToSpawn)
        {
            whichfish -= fish.SpawnChance;
            if (whichfish < 0)
            {
                m_roundQueue.Enqueue(fish);
                return true;
            }
        }


        return false;
    }

    /// <summary>
    /// Spawns aggressive fish depending on noise amount.
    /// Gets called when noise change delegate gets invoked.
    /// </summary>
    protected void ResolveNoiseChange()
    {
        foreach (var threshold in m_noiseThresholds)
        {
            if (m_playerCombatInfo.NoiseTracker.CurrentAmount > threshold.Key)
            {
                m_currentNoiseState = threshold.Value;
                ResolveSpawnChances(m_playerCombatInfo.NoiseTracker.CurrentAmount);
                break;
            }
        }
    }

    /// <summary>
    /// higher amount of noise, increase spawn chance.
    /// </summary>
    /// <param name="noiseAmount"></param>
    protected void ResolveSpawnChances(float noiseAmount)
    {
        //this could be logarithmic change depending on noiseTracker's currentAmount.
        foreach (var fish in m_aggressiveFishToSpawn)
        {
            fish.ChangeSpawnChance(noiseAmount);
        }
    }

    /// <summary>
    /// Update the round Queue.
    /// </summary>
    protected void ResolveRound()
    {
        //player wins if he's the only one in the queue
        if (m_roundQueue.Count == 1)
        {
            EndCombat();
            return;
        }

        //get current combabant and remove from queue.
        CombatInfo nextCombatant = m_roundQueue.Dequeue();

        //check for next combatant type and change the state.
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
    protected bool ChangeSelectedFish(float leftRight)
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
