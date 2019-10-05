using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NotMagicDescription
{
    public enum Shapes
    {
        Undefined = 0,
    };
    /// <summary>
    /// How the spell interacts with the world.
    /// </summary>
    public enum Effects
    {
        Undefined = 0,
    }
    /// <summary>
    /// The way in which the play interacts with the spell
    /// </summary>
    public enum Usages
    {
        Undefined = 0,
    }

    /// <summary>
    /// This is about where the spell shows up
    /// </summary>
    [Flags]
    public enum Aimings
    {
        Undefined = 0,
    }

    protected Shapes m_Shape;
    public Shapes Shape { get { return m_Shape; } }
    protected Effects m_Effect;
    public Effects Effect { get { return m_Effect; } }
    protected Usages m_Usage;
    public Usages Usage { get { return m_Usage; } }
    protected Aimings m_Aiming;
    public Aimings Aiming { get { return m_Aiming; } }

    public NotMagicDescription(Shapes shape, Effects effect, Usages usage, Aimings aiming)
    {
        m_Shape = shape;
        m_Effect = effect;
        m_Usage = usage;
        m_Aiming = aiming;
    }
}
