using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KBM_GauntletSlotsAndCastingScript : MonoBehaviour
{
    public KeyCode m_castOrAssignToSlot1 = KeyCode.Alpha1;
    public KeyCode m_castOrAssignToSlot2 = KeyCode.Alpha2;
    public KeyCode m_castOrAssignToSlot3 = KeyCode.Alpha3;

    public Gauntlet m_GauntletScript;
    public KBMSpellList m_KBMSpellList;

    public GameObject m_highlightedObject = null;
    public bool m_isHighlightingSomething;

    // Start is called before the first frame update
    void Start()
    {
        m_GauntletScript = gameObject.GetComponent<Gauntlet>();
    }


    void DetermineIfSomethingIsHighlighted()
    {
        m_highlightedObject = null;
        GameObject[] rootObjs = gameObject.scene.GetRootGameObjects();
        foreach (GameObject g in rootObjs)
        {
            if (g.name == "Highlighter")
            {
                m_highlightedObject = g;
                //Debug.LogWarning("Found Highlighted!");
                break;
            }
        }

        if (m_highlightedObject != null)
            m_isHighlightingSomething = true;
        else
            m_isHighlightingSomething = false;
    }

    // Update is called once per frame
    void Update()
    {
        DetermineIfSomethingIsHighlighted();

        if(!m_isHighlightingSomething)
        {

            //do stuff here for getting the correct slot
            if (Input.GetKeyDown(m_castOrAssignToSlot1))
            {

            }
            if (Input.GetKeyDown(m_castOrAssignToSlot2))
            {

            }
            if (Input.GetKeyDown(m_castOrAssignToSlot3))
            {

            }

        }


    }
}
