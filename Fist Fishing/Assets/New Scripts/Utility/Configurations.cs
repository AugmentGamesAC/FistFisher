using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configurations : MonoBehaviour
{
    //make this a singleton?

    //the key config and the current one
    protected List<KeyConfiguration> m_keyConfigurations;
    protected int m_currentKeyConfiguration;
    public KeyConfiguration CurrentKeyConfiguration()
    {
        if(m_keyConfigurations == default)
        {
            //put in a default profile
        }
        if(m_keyConfigurations.Count < m_currentKeyConfiguration || m_currentKeyConfiguration <0)
        {
            m_currentKeyConfiguration = 0;
        }


        return m_keyConfigurations[m_currentKeyConfiguration];
    }

}
