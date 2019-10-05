using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotMagicChip : BasicSlottable
{
    protected int m_GestureId;
    public int GestureId { get { return m_GestureId; } set { m_GestureId = value; } }

    protected NotMagic m_SpellData;
    public NotMagic SpellData { get { return m_SpellData; } set { m_SpellData = value; } }
}
