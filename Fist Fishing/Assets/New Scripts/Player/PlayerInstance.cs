using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInstance : MonoBehaviour, IPlayerData
{
    public static IPlayerData Instance { get; private set; }

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
    protected PlayerHealth m_health = new PlayerHealth(100);
    public PlayerHealth Health => m_health;

    [SerializeField]
    protected OxygenTracker m_oxygen = new OxygenTracker(100);
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

    [SerializeField]
    protected PlayerStatManager m_playerStatManager = new PlayerStatManager();
    public PlayerStatManager PlayerStatMan => m_playerStatManager;
}
