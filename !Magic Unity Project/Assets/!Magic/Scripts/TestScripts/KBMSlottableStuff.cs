using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KBMSlottableStuff : MonoBehaviour
{
    public KBM_GauntletSlotsAndCastingScript m_KBMGauntlet;
    public BasicSlottable m_SlottableScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void DoSlotThings(int slotIndex)
    {
        ASlot slot = m_KBMGauntlet.m_KBMSpellList.GetSlot(slotIndex);
        if (slot!=null)
        {
            m_SlottableScript.AssignSlot(slot);
        }
        //m_KBMGauntlet.m_GauntletScript; //relevent later?
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.LogWarning(m_KBMGauntlet);
        //Debug.LogWarning(m_SlottableScript);
        //Debug.LogWarning(m_KBMGauntlet.m_isHighlightingSomething);
        //Debug.LogWarning(m_KBMGauntlet.m_highlightedObject);

        if (m_KBMGauntlet != null 
            && m_SlottableScript!=null 
            && m_KBMGauntlet.m_isHighlightingSomething
            && m_KBMGauntlet.m_highlightedObject != null)
        {
            //Vector3 distv = (gameObject.transform.position - m_KBMGauntlet.m_highlightedObject.transform.position);
            float dist = Vector3.Distance(gameObject.transform.position, m_KBMGauntlet.m_highlightedObject.transform.position);
            //Debug.LogWarning(dist);
            if (dist <= 5.0f) //ensure that this is the object highlighted by gauntlet
            {
                //Debug.LogWarning("Slottable Highlighted!");
                if (Input.GetKeyDown(m_KBMGauntlet.m_castOrAssignToSlot1))
                {
                    //Debug.LogWarning("Slottable: Pressed 1");
                    DoSlotThings(0);
                }
                if (Input.GetKeyDown(m_KBMGauntlet.m_castOrAssignToSlot2))
                {
                    //Debug.LogWarning("Slottable: Pressed 2");
                    DoSlotThings(1);
                }
                if (Input.GetKeyDown(m_KBMGauntlet.m_castOrAssignToSlot3))
                {
                    //Debug.LogWarning("Slottable: Pressed 3");
                    DoSlotThings(2);
                }
            }
        }
        else
        {
            //Debug.LogError("Slottable: no gauntlet!");
        }




    }
}
