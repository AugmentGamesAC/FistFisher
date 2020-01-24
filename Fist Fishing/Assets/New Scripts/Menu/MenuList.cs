using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*Responsibilities
Shows and hides.
knows if mouse is available
knows if menu pauses game
*/
[System.Serializable]
public class MenuList
{
    [SerializeField]
    protected List<Menu> m_menuList;
    [SerializeField]
    protected bool m_gamePaused;
    [SerializeField]
    protected bool m_mouseActive;
    public bool Paused { get { return m_gamePaused; } }
    public bool MouseActive { get { return m_mouseActive; } }
    /// <summary>
    /// Sets each menu in the list that is active to the bool parameter
    /// Still need to setup the canvas' to make them show/not show
    /// </summary>
    /// <param name="activeState"></param>
    public void ShowActive(bool activeState)
    {
        if (activeState)
        {            
            foreach (Menu menu in m_menuList)
            {
                menu.Show(activeState);
            }
        }
        else
        {         
            foreach (Menu menu in m_menuList)
            {
                menu.Show(activeState);
            }
        }
    }
}

