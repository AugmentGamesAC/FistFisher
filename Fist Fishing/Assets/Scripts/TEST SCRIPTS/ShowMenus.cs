using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (Input.GetKeyDown(KeyCode.Alpha2)) { MenuManager.ActivateMenu(Menus.Test2); };

    }
}
