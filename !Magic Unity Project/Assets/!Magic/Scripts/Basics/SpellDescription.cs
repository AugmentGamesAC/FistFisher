using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpellDescription
{
    public enum Shape
    {
        Undefined = 0,
    };
    /// <summary>
    /// How the spell interacts with the world.
    /// </summary>
    public enum Effect
    {
        Undefined = 0,
    }
    /// <summary>
    /// The way in which the play interacts with the spell
    /// </summary>
    public enum Usage
    {
        Undefined = 0,
    }

    /// <summary>
    /// This is about where the spell shows up
    /// </summary>
    [Flags]
    public enum Aiming
    {
        Undefined = 0,
    }

    public Shape m_shape { get; set; }
    public Effect m_effect { get; set; }
    public Usage m_usage { get; set; }
    public Aiming m_aiming { get; set; }

    public SpellDescription(Shape shape, Effect effect, Usage usage, Aiming aiming)
    {
        m_shape = shape;
        m_effect = effect;
        m_usage = usage;
        m_aiming = aiming;
    }
}
