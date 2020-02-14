using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this file should store the 4 different input types: 

/// <summary>
/// the type of input
/// </summary>
public enum ActionType
{
    /// <summary>
    /// pinwheel
    /// joysticks and such
    /// </summary>
    TwoAxis,
    /// <summary>
    /// along a line (L->R, U->D)
    /// </summary>
    OneAxis,
    /// <summary>
    /// single button down input
    /// </summary>
    Button,
    /// <summary>
    /// 2axis with additional 1axis for cycling
    /// </summary>
    Page,
}

public class InputType
{
    protected ActionType m_actionType;
    public ActionType ActionType => m_actionType;

    protected Vector3 m_Input;
    public Vector3 Input => m_Input;
}

public class ButtonInput : InputType
{

}
public class Axis1Input : InputType
{

}
public class Axis2Input : InputType
{

}
public class Axis3Input : InputType
{

}