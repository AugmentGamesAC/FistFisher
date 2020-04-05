﻿using System.Collections.Generic;
using UnityEngine;




public class ALInput : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    private static void hasInstance()
    {
        if (Instance == default)
            throw new System.InvalidOperationException("ALInput not Initilized");
    }


    #region keycodes keycodes
    [SerializeField]
    List<KeyCode> m_action;
    /// <summary>
    /// Covers Selecting/Starting, Quick Buy, Quick Sell, Attacking, Mounting, Dismounting, and Gathering, Combat: Attack Action
    /// </summary>
    public static KeyCode Action;
    [SerializeField]
    List<KeyCode> m_altAction;
    /// <summary>
    /// Covers Canceling Menus, Quick Buy, Quick Sell, Ascending, Combat: Item Action
    /// </summary>
    public static KeyCode AltAction;
    [SerializeField]
    List<KeyCode> m_cancel;
    /// <summary>
    /// Button for exiting menus, decsending, and Combat: using item
    /// </summary>
    public static KeyCode Cancel;
    [SerializeField]
    List<KeyCode> m_toggle;
    /// <summary>
    /// Toggles screens, camera mode, quips?
    /// </summary>
    public static KeyCode Toggle;
    [SerializeField]
    List<KeyCode> m_menu;
    /// <summary>
    /// Opens Menu
    /// </summary>
    public static KeyCode Menu;
    [SerializeField]
    KeyCode m_menuKeyboard;
    /// <summary>
    /// Opens Menu
    /// </summary>
    public static KeyCode MenuKeyboard { get { hasInstance(); return Instance.m_menuKeyboard; } }
    [SerializeField]
    List<KeyCode> m_selectMouse;
    /// <summary>
    /// Make selections with the LMB, acts as a second action button for prompts
    /// </summary>
    public static KeyCode MouseAction;
    [SerializeField]
    List<KeyCode> m_inventory;
    /// <summary>
    /// Opening player inventory
    /// </summary>
    public static KeyCode Inventory;
    [SerializeField]
    List<KeyCode> m_cancelMouse;
    public static KeyCode MouseCancel;

    #endregion keycodes


    /// <summary>
    /// These are the AxisCodes that we configured in unity.Input manager as name
    /// </summary>
    public enum AxisCode
    {
        /// <summary>
        /// this is a cheat that will always return 0 using ALInput.GetAxis
        /// </summary>
        Unset,
        LookHorizontal,
        LookVertical,
        MoveHorizontal,
        MoveVertical,
        JoystickMoveHorizontal,
        JoystickMoveVertical,
        JoystickLookHorizontal,
        JoystickLookVertical,
        Switch,
        JoystickSwitch
    };

    public enum AxisType
    {
        LookHorizontal,
        LookVertical,
        MoveHorizontal,
        MoveVertical,
        Switch
    };

    public enum DirectionCode
    {
        /// <summary>
        /// returns Vector3.zero
        /// </summary>
        Unset,
        /// <summary>
        /// AxisCode.Horizontal,0,AxisCode.Vertical
        /// </summary>
        MoveInput,
        /// <summary>
        /// AxisCode.MouseX,AxisCode.MouseY,0
        /// </summary>
        LookInput,
        JoystickMoveInput,
        JoystickLookInput
    }
    /// <summary>
    /// Tuple is a dummy object that lets me link 3 objects into one without an offical class
    /// this allows us to define behavior in the dictionary here rather than in movementDirection
    /// will need to be refined when doing dynamic updating to controls
    /// </summary>
    private static Dictionary<DirectionCode, System.Tuple<AxisCode, AxisCode, AxisCode>> m_registeredMouseDirections =
        new Dictionary<DirectionCode, System.Tuple<AxisCode, AxisCode, AxisCode>>()
        {
            {DirectionCode.LookInput, new System.Tuple<AxisCode, AxisCode, AxisCode>(AxisCode.LookHorizontal,AxisCode.LookVertical,AxisCode.Unset) },
            {DirectionCode.MoveInput, new System.Tuple<AxisCode, AxisCode, AxisCode>(AxisCode.MoveHorizontal,AxisCode.Unset,AxisCode.MoveVertical) }
        };
    private static Dictionary<DirectionCode, System.Tuple<AxisCode, AxisCode, AxisCode>> m_registeredJoystickDirections =
        new Dictionary<DirectionCode, System.Tuple<AxisCode, AxisCode, AxisCode>>()
        {
            {DirectionCode.JoystickLookInput, new System.Tuple<AxisCode, AxisCode, AxisCode>(AxisCode.JoystickLookHorizontal,AxisCode.JoystickLookVertical,AxisCode.Unset) },
            {DirectionCode.JoystickMoveInput, new System.Tuple<AxisCode, AxisCode, AxisCode>(AxisCode.JoystickMoveHorizontal,AxisCode.Unset,AxisCode.JoystickMoveVertical) }
        };

    public static bool GetKeyUp(KeyCode key) { hasInstance(); return Input.GetKeyUp(key); }
    public static bool GetKeyDown(KeyCode key) { hasInstance(); return Input.GetKeyDown(key); }
    public static bool GetKey(KeyCode key) { hasInstance(); return Input.GetKey(key); }


    public static float GetAxis(AxisType type)
    {
        hasInstance();
        //Return the correct control input based on if controller is toggled or not

        //Invert is toggled return the inverse 
        if (InvertToggle && type == AxisType.LookVertical)
            return (ControllerToggle ? -Input.GetAxis("Joystick" + type.ToString()) : -Input.GetAxis(type.ToString()));

        return (ControllerToggle ? Input.GetAxis("Joystick" + type.ToString()) : Input.GetAxis(type.ToString()));

    }

    public static float GetAxisByCode(AxisCode key)
    {
        hasInstance();
        //Unset set to 0 here for efficiency reasons
        return (key == AxisCode.Unset) ? 0 : Input.GetAxis(key.ToString());
    }
    /// <summary>
    /// Returns true on the frame an axis that has a button is modified by a press. (Use in update)
    /// AXIS NEEDS TO HAVE A BUTTON
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool GetAxisPress(AxisType type)
    {
        if (!ControllerToggle && type == AxisType.Switch)
            return (Input.GetAxis(type.ToString()) != 0 ? true : false);

        return (ControllerToggle ? Input.GetButtonDown("Joystick" + type.ToString()) : Input.GetButtonDown(type.ToString()));
    }
    public static bool IsControllerToggled()
    {
        return (Instance.m_controllerToggle ? true : false);
    }

    public static void ToggleController()
    {
        Instance.m_controllerToggle = !ControllerToggle;

        int i = (ControllerToggle ? 1 : 0);

        //Swap controls
        {
            Action = Instance.m_action[i];
            AltAction = Instance.m_altAction[i];
            Cancel = Instance.m_cancel[i];
            Toggle = Instance.m_toggle[i];
            //Menu = Instance.m_menu[i];
            //MouseAction = Instance.m_selectMouse[i];
            Inventory = Instance.m_inventory[i];
            //MouseCancel = Instance.m_cancelMouse[i];
        }
    }

    public static void InvertLookYAxis()
    {
        Instance.m_invertToggle = !InvertToggle;

    }
    /// <summary>
    /// this function checks our registered direction codes and supples a vec3 as desired 
    /// </summary>
    /// <param name="dC">Enum defined in ALinput</param>
    /// <returns>either Vector3.zero on failure or a new vector 3 based on registeredDirections</returns>
    public static Vector3 GetDirection(DirectionCode dC)
    {
        System.Tuple<AxisCode, AxisCode, AxisCode> directionInstructions;

        //this breaks and directionInstuctions is null so player cannot receive input yet.
        if (!ControllerToggle)
        {
            if (!m_registeredMouseDirections.TryGetValue(dC, out directionInstructions))
                return Vector3.zero;
        }
        else
        {
            if (!m_registeredJoystickDirections.TryGetValue(dC, out directionInstructions))
                return Vector3.zero;
        }
        return new Vector3
        (
                GetAxisByCode(directionInstructions.Item1),
                GetAxisByCode(directionInstructions.Item2),
                GetAxisByCode(directionInstructions.Item3)
        );
    }


    private static ALInput Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject); //unity is stupid. Needs this to not implode
        Instance = this;
        LoadFromFile();
        //Set starting controls
        {
            Action = Instance.m_action[0];
            AltAction = Instance.m_altAction[0];
            Cancel = Instance.m_cancel[0];
            Toggle = Instance.m_toggle[0];
            Menu = Instance.m_menu[0];
            MouseAction = Instance.m_selectMouse[0];
            Inventory = Instance.m_inventory[0];
            MouseCancel = Instance.m_cancelMouse[0];
        }
    }


    /// <summary>
    /// at some point this will load configuration from file, right now just applies defaults
    /// </summary>
    private void LoadFromFile()
    {
        //m_action = KeyCode.F;
        //m_altAction = KeyCode.R;
        //m_toggle = KeyCode.Space;
        //m_menuKey = KeyCode.Escape;
        //m_cancleKey = KeyCode.E;

    }

    [SerializeField]
    protected bool m_controllerToggle;
    public static bool ControllerToggle { get { hasInstance(); return Instance.m_controllerToggle; } }
    [SerializeField]
    protected bool m_invertToggle;
    public static bool InvertToggle { get { hasInstance(); return Instance.m_invertToggle; } }
}
