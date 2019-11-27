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

    public GameObject m_mainMenu;




    [SerializeField]
    public InspectorDictionary<Menus, List<GameObject>> m_menuList = new InspectorDictionary<Menus, List<GameObject>> {
        { Menus.MainMenu, new List<GameObject>() }
    };




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
