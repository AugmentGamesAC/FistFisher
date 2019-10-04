using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpellForge : MonoBehaviour
{
    SpellManager m_spellManager { get; set; } 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CraftSpell()
    {
        int spellInt = m_spellManager.m_availableGestures[m_spellManager.m_availableGestures.Count - 1];
    }

    void ReceiveNewGestureID(int gesture) { }
}
