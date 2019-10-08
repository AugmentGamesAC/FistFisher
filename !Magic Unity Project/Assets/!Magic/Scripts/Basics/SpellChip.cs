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

    [System.Serializable]
    protected class ElementColorLookup : InspectorDictionary<Spell.Elements, Color> { }
    [SerializeField]
    protected ElementColorLookup m_ElementColorLookup = new ElementColorLookup()
    {
        { Spell.Elements.Fire, new Color(1f, 0.75f, 0f)     },
        { Spell.Elements.Ice, new Color(0.4f, 0.4f, 1.0f)  },
        { Spell.Elements.Lightning, new Color(1f, 1f, 0.2f)     }
     };

    [System.Serializable]
    protected class ShapeColorLookup : InspectorDictionary<SpellDescription.Shapes, Color> { }
    [SerializeField]
    protected ShapeColorLookup m_ShapeColorLookup = new ShapeColorLookup()
    {
        { SpellDescription.Shapes.Cone, new Color(1f, 0.1f, 1f)       },
        { SpellDescription.Shapes.Cube, new Color(1f, 0.2f, 0.2f)     },
        { SpellDescription.Shapes.Sphere, new Color(0.2f, 1f, 0.2f)   }
    };


    public void AssignSpellData(Spell newSpell)
    {
        Renderer rendererToColor = transform.Find(m_ModelName).GetComponent<Renderer>();
        Material mat = rendererToColor.material;
        Color col;
        Color col2;

        if (!m_ElementColorLookup.TryGetValue(m_SpellData.ElementTypes, out col))
            col = Color.black;
        if (!m_ShapeColorLookup.TryGetValue(m_SpellData.m_Description.Shape, out col2))
            col2 = Color.black;

        mat.color = col2;
        mat.SetColor("_EmissionColor", col);
        rendererToColor.material = mat;
    }
}
