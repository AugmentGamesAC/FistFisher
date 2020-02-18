using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInstance : MonoBehaviour, IPlayerData
{
    public static IPlayerData Instance { get; private set; }
    protected static PlayerInstance MyInstance => Instance as PlayerInstance;

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

        m_oxygen = new OxygenTracker(m_maxOxygen);
        m_health = new PlayerHealth(m_maxHealth); 
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
    protected float m_maxHealth = 500;
    public float MaxHealth => m_maxHealth;

    [SerializeField]
    protected float m_maxOxygen = 200;
    public float MaxOxygen => m_maxOxygen;

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
    }
    public static void RegisterItemInventory(SlotManager newInventory)
    {
        MyInstance.m_itemInventory = newInventory;
    }

    [SerializeField]
    protected PlayerStatManager m_playerStatManager = new PlayerStatManager();
    public PlayerStatManager PlayerStatMan => m_playerStatManager;

    [SerializeField]
    protected PlayerMotion m_playerMotion = new PlayerMotion();
    public PlayerMotion PlayerMotion => m_playerMotion;
}
