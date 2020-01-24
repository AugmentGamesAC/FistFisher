using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    #region working inspector dictionary
    /// <summary>
    /// this is the mess required to make dictionaries with  list as a value work in inspector
    /// used in this case to pair enum of menu enum with a list of menu objects
    /// </summary>
    [System.Serializable]
    public class MenuListForInspectorDictionary { public List<MenuList> m_listsOfMenu; }
    [System.Serializable]
    public class ListOfMenuConfigurations : InspectorDictionary<MenuScreens, MenuListForInspectorDictionary> { }
    [SerializeField]
    protected ListOfMenuConfigurations m_listOfConfigurations = new ListOfMenuConfigurations();
    public ListOfMenuConfigurations MenuList { get { return m_listOfConfigurations; } }


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
    /// Need help with this because im not sure how to get the MenuList's List of Menus
    /// </summary>
    /// <param name="currentlySelcetedMenuScreen"></param>
    public static void DisplayMenuScreen(MenuScreens currentlySelcetedMenuScreen)
    {
        List<Menu> menuList = new List<Menu>();
        if (Instance.m_currentSelectedMenu == currentlySelcetedMenuScreen)
            return;

        SetMenuListVisibility(default, false);
        Instance.m_currentSelectedMenu = currentlySelcetedMenuScreen;

        if (Instance.m_currentSelectedMenu == MenuScreens.NotSet)
            return;
        if (Instance.MenuList.TryGetValue(currentlySelcetedMenuScreen, out m_Mylist))
        {
            SetMenuListVisibility(m_Mylist,true);
        }
    }

    protected static void SetMenuListVisibility(List<Menu> list, bool visibility)
    {
        // different code based on using list
        if (m_Mylist == null)
            return;
        if (m_Mylist.m_listsOfMenu == null)
            return;
        if (list == null)
            return;

        //Why doesn't bm have a ShowHUD(bool)?
        foreach (Menu bm in list)
        // bm.HUD.SetActive(active);
        {
                bm.Show(visibility);
           
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
