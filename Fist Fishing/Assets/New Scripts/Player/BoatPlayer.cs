using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoatPlayer : MonoBehaviour
{
    [SerializeField]
    protected Transform m_mountTransform;
    [SerializeField]
    protected Transform m_dismountTransform;


    [SerializeField]
    public bool m_CanMove;
    protected statclassPlaceholder turningSpeedRef = new statclassPlaceholder();
    protected statclassPlaceholder movementSpeedRef = new statclassPlaceholder();

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

    public void FixedUpdate()
    {
        if ((ALInput.GetKeyDown(ALInput.Start)) && (MenuManager.Currentsetting == Menus.MainMenu))
            MenuManager.ActivateMenu(Menus.BoatTravel);

        if (MenuManager.Currentsetting == Menus.MainMenu)
            return;

        if (m_validPlayer == default) // no player around no action
            return;

        if (ALInput.GetKeyDown(ALInput.MountBoat)) //handle mounting
            MountAction();

        if (!m_isMounted) //not mounted can't move
            return;

        //Mounted Context actions
        ResolveRotation();

        if (ALInput.GetKey(ALInput.Forward))
            transform.position += transform.forward * Time.deltaTime * movementSpeedRef;

        if (ALInput.GetKeyDown(ALInput.ToggleInventory))
            ToggleMapInventoryDisplays();
    }


    void ResolveRotation()
    {
        float horizontalWeight = ALInput.GetAxis(ALInput.AxisCode.Horizontal);

        Vector3 desiredDirection = new Vector3
        (
            0,
            horizontalWeight,
            0
        ) * turningSpeedRef * Time.deltaTime;

        if (desiredDirection.sqrMagnitude > 0.000001)
            transform.Rotate(desiredDirection, Space.Self);
    }

    protected bool m_wasMapLast;
    protected void ToggleMapInventoryDisplays()
    {
        m_wasMapLast = !m_wasMapLast;
        //should be calling swapUI (future)
    }

    /// <summary>
    /// Should toggle the menu displays between swimming, map, boatInventory and options
    /// </summary>
    protected void SwapUI()
    {
        if (!m_isMounted)
            MenuManager.ActivateMenu(Menus.NormalHUD);
    }

    protected void MountAction()
    {
        m_isMounted = !m_isMounted;
        SwapUI();
        ToggleControls();
        PositionPlayer();
    }
    protected void ToggleControls()
    {
        m_CanMove = m_isMounted;
        m_validPlayer.m_CanMove = !m_isMounted;
    }
    protected void PositionPlayer()
    {
        m_validPlayer.transform.SetParent(m_isMounted ? transform : default);

        Transform targetTransform = (m_isMounted) ? m_mountTransform : m_dismountTransform;
        m_validPlayer.transform.SetPositionAndRotation(targetTransform.position, targetTransform.rotation);
    }

    public void RespawnPlayer(PlayerMotion player)
    {
        m_validPlayer = player;
        MountAction();
    }
}