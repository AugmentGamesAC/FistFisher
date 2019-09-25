using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpellManager : MonoBehaviour
{

    public List<SpellDescription.Shape> m_ShapeId;
    public List<GameObject> m_ShapeShape;

    public List<Spell.Elements> m_SpellId;
    public List<Material> m_SpellMaterials;


    public Dictionary<SpellDescription.Shape, GameObject> m_AimingShapes;
    public Dictionary<Spell.Elements, Material> m_SpellEffects;

    int TryCasting(IEnumerable<int> validSpells) { return 0; }

    public List<int> m_availableGestures;

    public void Start()
    {
        int offset = 0;
        m_AimingShapes = new Dictionary<SpellDescription.Shape, GameObject>();
        foreach(SpellDescription.Shape shape in m_ShapeId)
            m_AimingShapes.Add(shape, m_ShapeShape[offset++]);

        offset = 0;
        m_SpellEffects = new Dictionary<Spell.Elements, Material>();
        foreach (Spell.Elements shape in m_SpellId)
            m_SpellEffects.Add(shape, m_SpellMaterials[offset++]);

        
        
    }

}
