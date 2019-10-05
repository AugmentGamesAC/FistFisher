using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellChip : BasicSlottable
{
    protected int m_GestureId;
    public int GestureId { get { return m_GestureId; } set { m_GestureId = value; } }

    protected Spell m_SpellData;
    public Spell SpellData { get { return m_SpellData; } set { m_SpellData = value; } }
}
