using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the boat and behaviours if player is mounted
/// </summary>
[System.Serializable]
public class BoatPlayer : MonoBehaviour
{
    [SerializeField]
    protected Transform m_mountTransform;
    [SerializeField]
    protected Transform m_dismountTransform;

    [SerializeField]
    public bool m_CanMove;
    protected StatTracker turningSpeedRef = new StatTracker();
    protected StatTracker movementSpeedRef = new StatTracker();

    [SerializeField]
    protected bool m_isMounted;
    [SerializeField]
    protected PlayerMotion m_validPlayer;

    protected float m_dismountcooldown = 1.0f;

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
        else if (m_dismountcooldown > 0)
            m_dismountcooldown -= Time.deltaTime;
        /*if (m_validPlayer == default) // no player around no action
            return;*/

        if (ALInput.GetKeyDown(ALInput.Action) && m_dismountcooldown <=0 /*||(!m_isMounted && ALInput.GetKeyDown(ALInput.MouseAction))*/) //handle mounting
            MountAction();

        if (m_validPlayer == default) // no player around no action
            return;

        if (!m_isMounted)
            return;

        if (ALInput.GetKeyDown(ALInput.Toggle))
            ToggleMapInventoryDisplays();
        
    }

    private void FixedUpdate()
    {
        if (!m_isMounted) //not mounted can't move
            return;

        //Mounted Context actions
        ResolveRotation();
        if (ALInput.GetAxis(ALInput.AxisType.MoveVertical) > 0.2f)
        {
            //RaycastHit hit;
            Vector3 forwardmovement = transform.forward * Time.deltaTime * movementSpeedRef;
            //Physics.Raycast(transform.position, Vector3.forward, out hit, Mathf.Infinity, ~LayerMask.GetMask("Player", "Ignore Raycast", "Water", "BoatMapOnly"));
            //if(hit.distance > forwardmovement.magnitude)
                transform.position += forwardmovement;
        }
    }


    void ResolveRotation()
    {
        //Changed for testing
        float horizontalWeight;

        horizontalWeight = ALInput.GetAxis(ALInput.AxisType.MoveHorizontal);
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