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
}

/// <summary>
/// singleton that handles all active menus
/// </summary>
[System.Serializable]
public class NewMenuManager : MonoBehaviour
{

    protected Dictionary<MenuScreens, MenuList> m_menuLists;
    #region working inspector dictionary
    /// <summary>
    /// this is the mess required to make dictionaries with  list as a value work in inspector
    /// used in this case to pair enum of menu enum with a list of menu objects
    /// </summary>
    [System.Serializable]
    public class MenuListForInspectorDictionary { public List<MenuList> m_listsOfMenu; }
    [System.Serializable]
    public class MenuConfigurations : InspectorDictionary<MenuScreens, MenuListForInspectorDictionary> { }
    [SerializeField]
    protected MenuConfigurations m_menuConfigurations = new MenuConfigurations();
    public MenuConfigurations MenuConfigs { get { return m_menuConfigurations; } }
   // public MenuList ListsOfMenu 


    [SerializeField]
    public static MenuListForInspectorDictionary m_Mylist = new MenuListForInspectorDictionary();
    #endregion working inspector dictionary

    #region singletonification
    /// <summary>
    /// we only need one of these
    /// </summary>
    private static NewMenuManager Instance;
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

    private static void HasInstance()
    {
        if (Instance == default)
            throw new System.InvalidOperationException("Menu Manager not Initilized");
    }
    #endregion singletonification

    [SerializeField]
    protected MenuScreens m_currentSelectedMenu = MenuScreens.NotSet;
    /// <summary>
    /// Decides which menus to display based on what screen we are currently on
    /// Set the current menu  
    /// Display the current menu from the menulist
    /// </summary>
    /// <param name="currentlySelectedMenuScreen"></param>
    public static void DisplayMenuScreen(MenuScreens NewSelectedMenuScreen)
    {
        //Check if current menus is the same as the passed in menu
        if (Instance.m_currentSelectedMenu == NewSelectedMenuScreen)
            return;

        //If it is not then deactivate current menu
        SetMenuListActiveState(Instance.m_menuLists[Instance.m_currentSelectedMenu], false);
        //Set Current menu to the passed in menu
        Instance.m_currentSelectedMenu = NewSelectedMenuScreen;

        if (Instance.MenuConfigs.TryGetValue(NewSelectedMenuScreen, out m_Mylist))
        {
            SetMenuListActiveState(Instance.m_menuLists[NewSelectedMenuScreen], true);
        }
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
    /// <summary>
    /// Helper to get current menu's mouse active state
    /// </summary>
    /// <returns></returns>
    protected bool GetCurrentMouseActive()
    {
        return m_menuLists[m_currentSelectedMenu].MouseActive;
    }
    /// <summary>
    /// Helper to get current menu's game paused state
    /// </summary>
    /// <returns></returns>
    protected bool GetCurrentGamePause()
    {
        return m_menuLists[m_currentSelectedMenu].Paused;
    }
}
