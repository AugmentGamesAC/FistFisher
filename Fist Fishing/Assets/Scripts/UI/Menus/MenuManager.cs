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
    #region working inspector dictionary
    /// <summary>
    /// this is the mess required to make dictionaries with  list as a value work in inspector
    /// used in this case to pair enum of menu enum with a list of menu objects
    /// </summary>
    [System.Serializable]
    public class MenuListForInspectorDictionary { public List<BasicMenu> m_list; }
    [System.Serializable]
    public class ListOfMenuConfigurations : InspectorDictionary<Menus, MenuListForInspectorDictionary> { }
    [SerializeField]
    protected ListOfMenuConfigurations m_menuList = new ListOfMenuConfigurations();
    public ListOfMenuConfigurations MenuList { get { return m_menuList; } }


    [SerializeField]
    public static MenuListForInspectorDictionary m_Mylist = new MenuListForInspectorDictionary();
    #endregion working inspector dictionary



    #region singletonification
    /// <summary>
    /// we only need one of these
    /// </summary>
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
    public Menus m_currentMenus = Menus.NotSet;

    public static Menus Currentsetting => (Instance == default)? Menus.NotSet: Instance.m_currentMenus;



    [SerializeField]
    GameObject m_player;

    /// <summary>
    /// deactivate old menu, activate the one given if applicable
    /// </summary>
    public static void ActivateMenu(Menus m)
    {
        if (Instance.m_currentMenus == m)
            return;

        SetActiveSatusOncurrentMenuOption(false);
        Instance.m_currentMenus = m;

        if (m == Menus.NotSet)
            return;
        if (Instance.MenuList.TryGetValue(m, out m_Mylist))
            SetActiveSatusOncurrentMenuOption(true);
    }

    /// <summary>
    /// with the current active menus, go through list nd activate/deactivate all the attached gameobjects
    /// </summary>
    static void SetActiveSatusOncurrentMenuOption(bool active)
    {
        // different code based on using list
        if (m_Mylist == null)
            return;
        if (m_Mylist.m_list == null)
            return;

            //Why doesn't bm have a ShowHUD(bool)?
        foreach (BasicMenu bm in m_Mylist.m_list)
        // bm.HUD.SetActive(active);
        {
            if (active)
                bm.OpenMenu();
            else
                bm.CloseMenu();
        }
    }

    bool m_startup = true;


    void Update()
    {
        if (m_startup)
        {
            m_startup = false;
            ActivateMenu(Menus.MainMenu);
            //ActivateMenu(Menus.MainMenu);
            //ActivateMenu(Menus.MainMenu);
        }
        //as a fllback on closing a menu, we can always set the current menu to not et, and let this deal with choosing the correct one
        if (m_currentMenus == Menus.NotSet)
        {
            //Menus m = Menus.NotSet;
            //do stuff
            ActivateMenu(Menus.NormalHUD);
        }
        //HandleMenuRelatedInputs();


        if (m_currentMenus == Menus.ShopMenu || m_currentMenus == Menus.SwimmingInventory)
        {
            ToggleMouseLock(true);
        }
        else
        {
            ToggleMouseLock(false);
        }
    }

    //forcing this to be public for the build until I can properly sort it out
    public void ToggleMouseLock(bool on)
    {
        if (on)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void HandleMenuRelatedInputs()
    {
        if(m_player==null)
            m_player = GameObject.FindGameObjectWithTag("Player");
        if (m_player == null)
            return;

        if (m_currentMenus == Menus.MainMenu)
        {
            if (ALInput.GetKeyDown(ALInput.Action))
            {
                /*if (!(m_currentMenus == Menus.MainMenu))
                    ActivateMenu(Menus.ShopMenu);
                else
                    ActivateMenu(Menus.NotSet);*/
                ActivateMenu(Menus.BoatTravel);
            }
        }
        else
        {
            if (ALInput.GetKeyDown(ALInput.Switch))
            {
                if (!(m_currentMenus == Menus.SwimmingInventory))
                {
                    ActivateMenu(Menus.SwimmingInventory);
                    //m_player.gameObject.GetComponent<PlayerMovement>().ToggleMouseLock();
                }
                else
                    ActivateMenu(Menus.NotSet);
            }

            if (ALInput.GetKeyDown(ALInput.Action))
            {
                if (m_player.GetComponent<PlayerMovement>().m_isMounted)
                {
                    if (!(m_currentMenus == Menus.BoatTravel))
                    {
                        ActivateMenu(Menus.BoatTravel);
                    }
                    else
                        ActivateMenu(Menus.NotSet);
                }
            }
            else if (ALInput.GetKeyDown(ALInput.Action))
            {
                if (!m_player.GetComponent<PlayerMovement>().m_isMounted)
                {
                    if (!(m_currentMenus == Menus.NormalHUD))
                    {
                        ActivateMenu(Menus.NormalHUD);
                    }
                    else
                        ActivateMenu(Menus.NotSet);
                }
            }


            if (ALInput.GetKeyDown(ALInput.Toggle))
            {
                if (!(m_currentMenus == Menus.ShopMenu))
                {
                    ActivateMenu(Menus.ShopMenu);
                    //m_player.gameObject.GetComponent<PlayerMovement>().ToggleMouseLock();
                }
                else
                    ActivateMenu(Menus.NotSet);
            };

        }



    }



}
