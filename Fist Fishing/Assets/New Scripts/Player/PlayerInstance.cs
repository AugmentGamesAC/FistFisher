using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInstance : MonoBehaviour, IPlayerData
{
    public static IPlayerData Instance { get; private set; }
    protected static PlayerInstance MyInstance => Instance as PlayerInstance;

    [SerializeField]
    protected FloatTextUpdater m_clamsUpdater;
    public FloatTextUpdater ClamsUpdater => m_clamsUpdater;

    #region singletonification
    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject); //unity is stupid. Needs this to not implode
        Instance = this;

        m_playerStatManager.Init();

        m_oxygen = new OxygenTracker(PlayerInstance.Instance.PlayerStatMan[Stats.MaxAir]);
        m_health = new PlayerHealth(PlayerInstance.Instance.PlayerStatMan[Stats.MaxHealth]);

        m_promptManager = new PromptManager();

        m_clamsUpdater.UpdateTracker(m_clams);


        Debug.Log("Don't forget to SetTrackers: stealth and damage");
        //m_playerStatManager.SetTracker(Stats.Power, damageTracker);
        //m_playerStatManager.SetTracker(Stats.Stealth, noiseTracker);
    }

    private static void HasInstance()
    {
        if (Instance == default)
            throw new System.InvalidOperationException("Menu Manager not Initilized");
    }
    #endregion

    protected CombatManager m_cM;
    public CombatManager CM {
        get
        {
            if (m_cM == default)
                m_cM = GameObject.Find("CombatScreen").GetComponent<CombatManager>();
            return m_cM;
        }
    }

    private void Update()
    {
        //needs to update for regen and degen.
        m_oxygen.Update();
    }

    [SerializeField]
    protected PlayerHealth m_health;
    public PlayerHealth Health => m_health;

    [SerializeField]
    protected OxygenTracker m_oxygen;
    public OxygenTracker Oxygen => m_oxygen;

    [SerializeField]
    protected float m_attackRange = 20;
    public float AttackRange => m_attackRange;

    [SerializeField]
    protected ImageTracker m_iconDisplay;
    public ImageTracker IconDisplay => m_iconDisplay;

    [SerializeField]
    protected FloatTracker m_clams = new FloatTracker();
    public FloatTracker Clams => m_clams;

    protected SlotManager m_playerInventory;
    public SlotManager PlayerInventory => m_playerInventory;

    protected SlotManager m_itemInventory;
    public SlotManager ItemInventory => m_itemInventory;


    public static void RegisterPlayerInventory(SlotManager newInventory)
    {
        MyInstance.m_playerInventory = newInventory;
        MyInstance.m_playerInventory.OnGatheringProgress += QuestManager.Instance.CheckGatheringProgress;
    }

    public static void RegisterItemInventory(SlotManager newInventory)
    {
        MyInstance.m_itemInventory = newInventory;
        MyInstance.m_itemInventory.OnGatheringProgress += QuestManager.Instance.CheckGatheringProgress;
    }

    public static void RegisterPlayerMotion(PlayerMotion playerMotion)
    {
        MyInstance.m_playerMotion = playerMotion;
        playerMotion.SetMoveSpeedTracker(PlayerInstance.Instance.PlayerStatMan[Stats.MovementSpeed]);
        playerMotion.SetTurnSpeedTracker(PlayerInstance.Instance.PlayerStatMan[Stats.TurnSpeed]);
    }

    [SerializeField]
    protected PlayerStatManager m_playerStatManager = new PlayerStatManager();
    public PlayerStatManager PlayerStatMan => m_playerStatManager;

    [SerializeField]
    protected PlayerMotion m_playerMotion;
    public PlayerMotion PlayerMotion => m_playerMotion;

    [SerializeField]
    protected PromptManager m_promptManager;
    public PromptManager PromptManager => m_promptManager;
}
