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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { MenuManager.ActivateMenu(Menus.MainMenu); };
        if (Input.GetKeyDown(KeyCode.Alpha2)) { MenuManager.ActivateMenu(Menus.OptionsMenu); };

    }
}
