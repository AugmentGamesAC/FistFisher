using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionCrystal : BasicSlottable
{
    [SerializeField]
    protected SpellDescription m_SpellDescription;
    public SpellDescription SpellDescription { get { return m_SpellDescription; } }
}
