using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSlot : MonoBehaviour, ISlot
{
    public SlotTypes m_Slotfilter;
    public Vector3 m_SlottedLocalOffset;

    public Material m_slotDefault;
    public Material m_slotHighlight;

    private bool m_IsHighlighted = true;

    // Start is called before the first frame update
    private ISlotable _slotted; 
    public ISlotable m_slotted { get { return _slotted; } set { _slotted = value;  }  }

    public void Start()
    {
        m_IsHighlighted = true;
        ToggleHighlighting();
    }


    public bool Accept(ISlotable slottingObject)
    {
        if (!CanAccept(slottingObject))
            return false;

        //out with the old
        if (m_slotted != null)
            Eject();

        //inwiththe new
        m_slotted = slottingObject;
        m_slotted.m_isHeldSlot = true;
        //m_slotted.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().useGravity = false;
        m_slotted.gameObject.transform.SetParent(gameObject.transform, false);
        m_slotted.gameObject.transform.localPosition = m_SlottedLocalOffset;

        return true ;
    }


    public void Eject()
    {
        m_slotted.gameObject.transform.localPosition = Vector3.zero;
        //GameObject.Destroy(m_slotted.gameObject);
        m_slotted.m_isHeldSlot = false;
        //m_slotted.gameObject.transform.SetParent(null);
        //m_slotted.gameObject.GetComponent<Rigidbody>().useGravity = true;
        Debug.Log("Ejecting");
        m_slotted = null;
        ToggleHighlighting();
    }


    public bool CanAccept(ISlotable slotable)
    {
        return ((slotable.m_slotType & m_Slotfilter) > 0);
    }

    public void ToggleHighlighting()
    {
        m_IsHighlighted = !m_IsHighlighted;
        ToggleHighlighting(m_IsHighlighted);
    }

    public void ToggleHighlighting(bool toggle)
    {
         gameObject.GetComponentInChildren<Renderer>().material = (m_IsHighlighted) ? m_slotHighlight : m_slotDefault;
    }
}
