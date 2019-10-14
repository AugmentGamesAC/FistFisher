using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSlot : ASlot
{
    [SerializeField]
    protected Material m_DefaultMaterial;
    [SerializeField]
    protected Material m_SecondaryMaterial;
    protected Renderer m_MyRender;
    public override void Start()
    {
        m_MyRender = GetComponentInChildren<Renderer>();
        base.Start();
    }
    


    public override void ToggleHighlighting(bool toggle)
    {
        m_IsHighlighted = toggle;

        //Debug.Log("Toggled Highlighting to " + m_IsHighlighted);
        if (m_MyRender != null)
            m_MyRender.material = (m_IsHighlighted) ? m_SecondaryMaterial : m_DefaultMaterial;
    }



}
