using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A list of all menu/HUD configurations, which are linked with a list of menus in the menu manager
/// </summary>
public enum Menus
{
    /// <summary>
    /// should do nothing
    /// </summary>
    NotSet,
    /// <summary>
    /// A way to start/exit game
    /// </summary>
    MainMenu,
    /// <summary>
    /// access to quit, options (which will be keybind menu access), and save/load/sound/difficulty/graphics in the far future 
    /// </summary>
    OptionsMenu, 
    /// <summary>
    /// place to set keys for KBM input
    /// </summary>
    KeyboardControlsSetup,
    /// <summary>
    /// place to set keys for controller input
    /// </summary>
    ControllerControlsSetup,
    /// <summary>
    /// the screen showing the player inventory and the shop
    /// </summary>
    ShopMenu,
    /// <summary>
    /// basic player inventory
    /// </summary>
    SwimmingInventory,
    /// <summary>
    ///  small radial menu to allow the player to choose 1 of 4 preselected baits to be the active one
    /// </summary>
    QuickSelectPinwheel,
    /// <summary>
    /// anyting player normally sees
    /// </summary>
    NormalHUD,
    /// <summary>
    /// what player sees while boat is moving around the present scene
    /// </summary>
    BoatTravel,
    /// <summary>
    /// travel map for selecting other scenes to transport to
    /// </summary>
    LakeTravel,
    /// <summary>
    /// Options available when mounted on boat
    /// </summary>
    MountBoat,
}

/// <summary>
/// singleton that handles all active menus
/// </summary>
[System.Serializable]
public class MenuManager : MonoBehaviour
{
    /// <summary>
    /// this is the mess reuired to make dictionaries with  list as a value work in inspector
    /// used in this case to pair enum of menu enum with a list of menu objects
    /// </summary>
    #region working inspector dictionary
    [System.Serializable]
    public class MenuListForInspectorDictionary { public List<BasicMenu> m_list; }
    [System.Serializable]
    public class ListOfMenuConfigurations : InspectorDictionary<Menus, MenuListForInspectorDictionary> { }
    [SerializeField]
    protected ListOfMenuConfigurations m_menuList = new ListOfMenuConfigurations();
    public ListOfMenuConfigurations MenuList { get { return m_menuList; } }
    #endregion working inspector dictionary


    /// <summary>
    /// we only need one of these
    /// </summary>
    #region singletonification
    private static MenuManager Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject); //unity is stupid. Needs this to not implode
        Instance = this;
    }

    private static void hasInstance()
    {
        if (Instance == default)
            throw new System.InvalidOperationException("Menu Manager not Initilized");
    }
    #endregion singletonification




    [SerializeField]
    public static Menus m_currentMenus = Menus.NotSet;

    /// <summary>
    /// deactivate old menu, activate the one given if applicable
    /// </summary>
    public static void ActivateMenu(Menus m)
    {
        if (m == Menus.NotSet)
            return;

        if(m_currentMenus!=Menus.NotSet)
            SetActiveSatusOncurrentMenuOption(false);

        m_currentMenus = m;

        SetActiveSatusOncurrentMenuOption(true);
    }

    /// <summary>
    /// with the current active menus, go through list nd activate/deactivate all the attached gameobjects
    /// </summary>
    static void SetActiveSatusOncurrentMenuOption(bool active)
    {
        List<BasicMenu> lm = Instance.MenuList[m_currentMenus].m_list;
        foreach (BasicMenu bm in lm)
        {
            bm.HUD.SetActive(active);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
