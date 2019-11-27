using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Menus
{
    NotSet,
    MainMenu,
    Test2,
}

[System.Serializable]
public class MenuManager : MonoBehaviour
{
    #region working inspector dictionary
    [System.Serializable]
    public class MenuListForInspectorDictionary { public List<BasicMenu> m_list; }
    [System.Serializable]
    public class ListOfMenuConfigurations : InspectorDictionary<Menus, MenuListForInspectorDictionary> { }
    [SerializeField]
    protected ListOfMenuConfigurations m_menuList = new ListOfMenuConfigurations();
    public ListOfMenuConfigurations MenuList { get { return m_menuList; } }
    #endregion working inspector dictionary

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

    public static void ActivateMenu(Menus m)
    {
        if (m == Menus.NotSet)
            return;

        if(m_currentMenus!=Menus.NotSet)
            SetActiveSatusOncurrentMenuOption(false);

        m_currentMenus = m;

        SetActiveSatusOncurrentMenuOption(true);
    }

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
