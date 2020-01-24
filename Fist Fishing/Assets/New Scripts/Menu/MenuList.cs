using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuList : MonoBehaviour
{
    protected List<Menu> m_menuList;

    bool m_pausesGameIfActive;
    bool m_allowMouseToBeActive;

    /// <summary>
    /// Sets each item in the list to not show
    /// Still need to setup the canvas' to make them show/not show
    /// </summary>
    /// <param name="activeState"></param>
    public void ShowActive(bool activeState)
    {
        if (activeState)
        {
            m_pausesGameIfActive = true;
            m_allowMouseToBeActive = true;

            foreach (Menu item in m_menuList)
            {
                item.Show(true);
            }
        }
        else
        {
            m_allowMouseToBeActive = false;
            m_pausesGameIfActive = false;

            foreach (Menu item in m_menuList)
            {
                item.Show(false);
            }
        }
    }
}

