using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Menus
{
    NotSet,
    MainMenu,
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

    [SerializeField]
    public List<BasicMenu> test;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
