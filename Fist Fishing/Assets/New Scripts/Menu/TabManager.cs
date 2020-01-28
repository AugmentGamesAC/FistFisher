using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
{
    protected Tab m_currentSelectedTab;

    public void NewActivation(Tab currentSelected)
    {
        if (m_currentSelectedTab != null)
            m_currentSelectedTab.SetIsSelected(false);

        m_currentSelectedTab = currentSelected;

        m_currentSelectedTab.SetIsSelected(true);
    }
}
