using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Responsibilities
last activated MenuList
function to deactivate prior MenuList when new one is activated
Needs to be access from multiple code locations, Singleton or other solutions to house desirable. 

can get from menu list if it needs to pause or show mouse

When boat, player, or combat need to do something with menus, it calls this

*/


/// <summary>
/// A list of all menu/HUD configurations, which are linked with a list of menus in the menu manager
/// </summary>
public enum MenuScreens
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

    Combat
}

/// <summary>
/// singleton that handles all active menus
/// </summary>
[System.Serializable]
public class NewMenuManager : MonoBehaviour
{
    #region working inspector dictionary
    /// <summary>
    /// this is the mess required to make dictionaries with  list as a value work in inspector
    /// used in this case to pair enum of menu enum with a list of menu objects
    /// </summary>
    /// 
    [System.Serializable]
    public class MenuConfigurations : InspectorDictionary<MenuScreens, MenuList> { }
    [SerializeField]
    protected MenuConfigurations m_menuConfigurations = new MenuConfigurations();
    public MenuConfigurations MenuConfigs => m_menuConfigurations;
    #endregion working inspector dictionary

    public static bool MouseActiveState
    {
        get
        {
            if (!Instance.m_menuConfigurations.ContainsKey(Instance.m_currentSelectedMenu))
                return true;
            return Instance.m_menuConfigurations[Instance.m_currentSelectedMenu].MouseActive;
        }
    }
    public static bool PausedActiveState
    {
        get
        {
            if (!Instance.m_menuConfigurations.ContainsKey(Instance.m_currentSelectedMenu))
                return true;
            return Instance.m_menuConfigurations[Instance.m_currentSelectedMenu].Paused;
        }
    }

    #region singletonification
    /// <summary>
    /// we only need one of these
    /// </summary>
    protected static NewMenuManager Instance;
    protected void Awake()
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
    #endregion singletonification

    [SerializeField]
    protected MenuScreens m_currentSelectedMenu = MenuScreens.NotSet;
    public static MenuScreens CurrentMenu => Instance.m_currentSelectedMenu;


    /// <summary>
    /// Decides which menus to display based on what screen we are currently on
    /// Set the current menu  
    /// Display the current menu from the menulist
    /// </summary>
    /// <param name="currentlySelectedMenuScreen"></param>
    public static void DisplayMenuScreen(MenuScreens NewSelectedMenuScreen)
    {
        Instance.DisplayMenu(NewSelectedMenuScreen);
    }

    private void Start()
    {
        foreach (var menuList in Instance.m_menuConfigurations)
            menuList.Value.ShowActive(false);

        Instance.DisplayMenu(MenuScreens.MainMenu);
    }

    protected void DisplayMenu(MenuScreens newMenu)
    {
        //Don't change if argument is same as current.
        if (Instance.m_currentSelectedMenu == newMenu)
            return;


        MenuList resultList;

        //deactivate current menu
        if (Instance.m_menuConfigurations.TryGetValue(Instance.m_currentSelectedMenu, out resultList))
                SetMenuListActiveState(resultList, false);
        //Set Current menu to the passed in menu
        Instance.m_currentSelectedMenu = newMenu;

        if (Instance.m_menuConfigurations.TryGetValue(Instance.m_currentSelectedMenu, out resultList))
            SetMenuListActiveState(resultList, true);
    }

    /// <summary>
    /// Set the visibility of the menu items within a menu
    /// </summary>
    /// <param name="list"></param>
    /// <param name="visibility"></param>
    protected static void SetMenuListActiveState(MenuList list, bool activeState)
    {
        if (list == null)
            return;

        list.ShowActive(activeState);
    }
}
