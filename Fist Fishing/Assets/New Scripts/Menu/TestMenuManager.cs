using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// test code for newest menu manager setup
/// </summary>
public class TestMenuManager : NewMenuManager, ISerializationCallbackReceiver
{
    [SerializeField]
    MenuScreens controlMenu = MenuScreens.MainMenu;

    public new static void  DisplayMenuScreen(MenuScreens NewSelectedMenuScreen)
    {


        ((TestMenuManager)Instance).DisplayMenu(NewSelectedMenuScreen);
    }

    protected new void DisplayMenu(MenuScreens newMenu)
    {
        base.DisplayMenu(newMenu);
    }

    public void OnBeforeSerialize()
    {
        if (Instance == default)
            Instance = this;
        // DisplayMenuScreen(((TestMenuManager)Instance).m_currentSelectedMenu);

        DisplayMenuScreen(controlMenu);
    }

    public void OnAfterDeserialize()
    {
    }
}
