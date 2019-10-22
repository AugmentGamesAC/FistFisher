using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class SpellChip : BasicSlottable
{
    [SerializeField]
    protected int m_GestureId;
    public int GestureId { get { return m_GestureId; } set { m_GestureId = value; } }

    [SerializeField]
    protected Spell m_SpellData;
    public Spell SpellData { get { return m_SpellData; } }

    [SerializeField]
    protected string m_ModelName;

    public void AssignSpellData(Spell newSpell)
    {
        m_SpellData = newSpell;

        Renderer rendererToColor = transform.Find(m_ModelName).GetComponent<Renderer>();
        if (rendererToColor == null)
            return;
        Material mat = rendererToColor.material;
        Color col;
        Color col2;

        if (SpellManager.Instance == null)
            return;

        if (!SpellManager.Instance.ElementColorLookup.TryGetValue(m_SpellData.ElementType, out col))
            col = Color.black;
        if (!SpellManager.Instance.ShapeColorLookup.TryGetValue(m_SpellData.Description.Shape, out col2))
            col2 = Color.black;

        mat.color = col2;
        mat.SetColor("_EmissionColor", col);
        rendererToColor.material = mat;
    }
}
