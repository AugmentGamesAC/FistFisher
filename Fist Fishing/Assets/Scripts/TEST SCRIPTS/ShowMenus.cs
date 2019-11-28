using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// test script to allow user to pres 1 or 2 to how placeholder menu items
/// </summary>
public class ShowMenus : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        MenuManager.ActivateMenu(Menus.NormalHUD);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { MenuManager.ActivateMenu(Menus.NormalHUD); };
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            MenuManager.ActivateMenu(Menus.SwimmingInventory);
            gameObject.GetComponent<PlayerMovement>().ToggleMouseLock();
        };
        if (Input.GetKeyDown(KeyCode.Alpha3)) { MenuManager.ActivateMenu(Menus.BoatTravel); };

    }
}
