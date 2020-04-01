﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoatPlayer : MonoBehaviour
{
    [SerializeField]
    protected Transform m_mountTransform;
    [SerializeField]
    protected Transform m_dismountTransform;
    //For testing only
    public bool m_controllerToggle = false;

    [SerializeField]
    public bool m_CanMove;
    protected StatTracker turningSpeedRef = new StatTracker();
    protected StatTracker movementSpeedRef = new StatTracker();

    [SerializeField]
    protected bool m_isMounted;
    [SerializeField]
    protected PlayerMotion m_validPlayer;

    #region controlsValidPlayerRef;
    protected void OnTriggerEnter(Collider other)
    {
        if (m_isMounted)
            return;

        PlayerMotion check = other.GetComponent<PlayerMotion>();
        if (check == default)
            return;
        m_validPlayer = check;
    }
    protected void OnTriggerExit(Collider other)
    {
        if (!m_isMounted)
            return;

        PlayerMotion check = other.GetComponent<PlayerMotion>();
        if (check == default)
            return;
        m_validPlayer = default;
    }
    #endregion

    private void Start()
    {
        PlayerInstance.Instance.Health.OnMinimumAmountReached += RespawnPlayer;
    }

    public void Update()
    {
        if ((ALInput.GetKeyDown(ALInput.Action)) && (NewMenuManager.CurrentMenu == MenuScreens.MainMenu))
            SwapUI();

        if (NewMenuManager.CurrentMenu == MenuScreens.MainMenu)
            return;

        if (m_validPlayer == default) // no player around no action
            return;

        if (ALInput.GetKeyDown(ALInput.Action) ||(!m_isMounted && ALInput.GetKeyDown(ALInput.MouseAction))) //handle mounting
            MountAction();

        if (!m_isMounted)
            return;

        if (ALInput.GetKeyDown(ALInput.Toggle))
            ToggleMapInventoryDisplays();
        if (Input.GetKeyDown(KeyCode.Backspace))
            m_controllerToggle = !m_controllerToggle;
    }

    private void FixedUpdate()
    {
        if (!m_isMounted) //not mounted can't move
            return;

        //Mounted Context actions
        ResolveRotation();

        if (ALInput.GetKey(ALInput.Forward) || (m_controllerToggle && ALInput.GetAxis(ALInput.AxisCode.JoystickLVerticle) < -0.1))
            transform.position += transform.forward * Time.deltaTime * movementSpeedRef;
    }


    void ResolveRotation()
    {
        //Changed for testing
        float horizontalWeight;
        if (m_controllerToggle)
        {

             horizontalWeight = ALInput.GetAxis(ALInput.AxisCode.JoystickLHorizontal);
        }
        else
        {
             horizontalWeight = ALInput.GetAxis(ALInput.AxisCode.KeyboardHorizontal);
        }
        Vector3 desiredDirection = new Vector3
        (
            0,
            horizontalWeight,
            0
        ) * turningSpeedRef * Time.deltaTime;

        if (desiredDirection.sqrMagnitude > 0.000001)
            transform.Rotate(desiredDirection, Space.Self);
    }

    protected bool m_displayMap = true;
    protected void ToggleMapInventoryDisplays()
    {
        m_displayMap = !m_displayMap;
        SwapUI();
    }

    /// <summary>
    /// Should toggle the menu displays between swimming, map, boatInventory and options
    /// </summary>
    protected void SwapUI()
    {
        MenuScreens desiredMenu = (!m_isMounted) ? MenuScreens.NormalHUD : (m_displayMap) ? MenuScreens.BoatTravel : MenuScreens.ShopMenu;
        NewMenuManager.DisplayMenuScreen(desiredMenu);
    }

    protected void MountAction()
    {
        if (m_validPlayer == default)
            return;

        PlayerInstance.Instance.Health.ResetCurrentAmount();
        PlayerInstance.Instance.Oxygen.ResetOxygen();
        m_isMounted = !m_isMounted;
        SwapUI();
        ToggleControls();
        PositionPlayer();
    }
    protected void ToggleControls()
    {
        m_CanMove = m_isMounted;
        m_validPlayer.m_CanMove = !m_CanMove;
    }
    protected void PositionPlayer()
    {
        m_validPlayer.transform.SetParent(m_isMounted ? transform : default);

        Transform targetTransform = (m_isMounted) ? m_mountTransform : m_dismountTransform;
        m_validPlayer.transform.SetPositionAndRotation(targetTransform.position, targetTransform.rotation);
        m_validPlayer = m_isMounted ? m_validPlayer : default;
    }

    public void RespawnPlayer()
    {
        m_validPlayer = PlayerInstance.Instance.PlayerMotion;
        MountAction();
    }
}