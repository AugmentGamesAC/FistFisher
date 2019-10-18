using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SpellDescription
{

    public enum Shapes
    {
        Undefined = 0,
        Cube,
        Cone,
        Sphere,
        HalfSphere,
        Beam,
        Spear,
        Disk,
        DomedCylinder
    };
    /// <summary>
    /// How the spell interacts with the world.
    /// </summary>
    public enum Effects
    {
        Undefined = 0,
        Summon,
        Projectile,
        Damage,
        Swap,
        Tether,
        Alarm,
        AuraSiphon,
        ManaSiphon,
        ImpactGenade
    };
    /// <summary>
    /// The way in which the play interacts with the spell
    /// </summary>
    public enum Usages
    {
        Undefined = 0,
        Instant,
        SetTime,
        Continuous,
        RemoteControl,
    };

    /// <summary>
    /// This is about where the spell shows up
    /// </summary>
    [Flags]
    public enum Aimings
    {
        Undefined = 0,
        FromCaster = 0x00000001, //show at center of caster
        FromFinger = 0x00000002, //show at tip of finger
        FromFingerEndPoint = 0x00000004, //show at collisionPoin
        Hidden = 0x00000008, //displays nothing.

        ReverseX = 0x00000010, //Rotate 180 on X
        ReverseY = 0x00000020, //Rotate 180 on Y
        ReverseZ = 0x00000040, //Rotate 180 on Z

        NinetyX = 0x00000100, //Rotate 90 on X
        NinetyY = 0x00000200, //Rotate 90 on Y
        NinetyZ = 0x00000400, //Rotate 90 on Z

        HasRotation = ReverseX | ReverseY | ReverseZ | NinetyX | NinetyY | NinetyZ,



        HalfOffsetX = 0x00001000, //Increment position by
        HalfOffsetY = 0x00002000, //Increment position by
        HalfOffsetZ = 0x00004000, //Increment posiston by
        HasHalfOffset = HalfOffsetX | HalfOffsetY | HalfOffsetZ,
        HalfOffsetNeg = 0x00008000, //Positive offset is implied



        FullOffsetX = 0x00010000,
        FullOffsetY = 0x00020000,
        FullOffsetZ = 0x00040000,
        HasFullOffset = FullOffsetX | FullOffsetY | FullOffsetZ,
        FullOffsetNeg = 0x00080000, //Positive offset is implied

        CenteredBoxToFingerTip = FromFinger | HalfOffsetZ,
        CenteredBoxToFingerTip90RX = CenteredBoxToFingerTip | NinetyX,
        CenteredBoxToFingerTip90RY = CenteredBoxToFingerTip | NinetyY,
        CenteredBoxToFingerTip90RZ = CenteredBoxToFingerTip | NinetyZ,

        FromFingerEndPointPlusHalfExtent = FromFingerEndPoint | HalfOffsetY
    };

    [SerializeField]
    protected Shapes m_Shape;
    public Shapes Shape { get { return m_Shape; } set { m_Shape = value; } }
    [SerializeField]
    protected Effects m_Effect;
    public Effects Effect { get { return m_Effect; } set { m_Effect = value; } }
    [SerializeField]
    protected Usages m_Usage;
    public Usages Usage { get { return m_Usage; } set { m_Usage = value; } }
    [SerializeField]
    protected Aimings m_Aiming;
    public Aimings Aiming { get { return m_Aiming; } set { m_Aiming = value; } }

    public SpellDescription(Shapes shape, Effects effect, Usages usage, Aimings aiming)
    {
        m_Shape = shape;
        m_Effect = effect;
        m_Usage = usage;
        m_Aiming = aiming;
    }
}
