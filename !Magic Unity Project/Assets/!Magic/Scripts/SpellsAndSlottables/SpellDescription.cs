using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Shape2
{
    ThisEldrichHorrorOfAValueDoesNotExistHere  =0,
    Cube = 1,
    Sphere,
    Cone
};

public class SpellDescription {
    public enum Shape
    {
        ThisEldrichHorrorOfAValueDoesNotExistHere = 0,
        Cube = 1,
        Sphere,
        Cone,
		Grenade
    };
    /// <summary>
    /// How the spell interacts with the world.
    /// </summary>
    public enum Effect
    {
        ThisEldrichHorrorOfAValueDoesNotExistHere = 0,
        Damage = 1,
        Summon
    }
    /// <summary>
    /// The way in which the play interacts with the spell
    /// </summary>
    public enum Usage
    {
        ThisEldrichHorrorOfAValueDoesNotExistHere = 0,
        Duration = 1,
        ManualDuration,
        ManualTrigger,
        Instant
    }

    [Flags]
    public enum Aiming
    {
        FromCaster          = 0x0001, //show at center of caster
        FromFinger          = 0x0002, //show at tip of finger
        FromFingerEndPoint  = 0x0004, //show at collisionPoint
        Hidden              = 0x0008, //displays nothing.

        ReverseX            = 0x0010, //Rotate 180 on X
        ReverseY            = 0x0020, //Rotate 180 on Y
        ReverseZ            = 0x0040, //Rotate 180 on Z
        HasRotation         = ReverseX|ReverseY|ReverseZ, 


        HalfOffsetX         = 0x0100, //Increment position by
        HalfOffsetY         = 0x0200, //Increment position by
        HalfOffsetZ         = 0x0400, //Increment posiston by
        HasHalfOffset       = HalfOffsetX|HalfOffsetY|HalfOffsetZ,
        HalfOffsetNeg       = 0x0800, //Positive offset is implied



        FullOffsetX         = 0x1000,
        FullOffsetY         = 0x2000,
        FullOffsetZ         = 0x4000,
        HasFullOffset       = FullOffsetX| FullOffsetY| FullOffsetZ,
        FullOffsetNeg       = 0x8000, //Positive offset is implied

        CenteredBoxToFingerTip = FromFinger|HalfOffsetZ,
        CenteredBoxToFingerTip90RX = CenteredBoxToFingerTip |ReverseX,
        CenteredBoxToFingerTip90RY = CenteredBoxToFingerTip | ReverseY,
        CenteredBoxToFingerTip90RZ = CenteredBoxToFingerTip | ReverseZ,

        FromFingerEndPointPlusHalfExtent = FromFingerEndPoint|HalfOffsetY
    }

    public Shape m_shape{ get; set; }
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
