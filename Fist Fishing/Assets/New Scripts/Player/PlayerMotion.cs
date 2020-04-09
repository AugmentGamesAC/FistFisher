using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// player movement and camera interactions
/// </summary>
[System.Serializable]
public class PlayerMotion : MonoBehaviour
{
    CameraManager m_vision;

    public bool m_CanMove;
    public bool m_CanMoveForward;

    [SerializeField]
    protected float m_sphereCastRadius = 500;
    public float SphereCastRadius => m_sphereCastRadius;

    public List<FishInstance> SurroundingFish => FindSurroundingFish();

    [SerializeField]
    protected FishInstance m_closestFish;

    [SerializeField]
    protected StatTracker m_turningSpeedRef;
    public StatTracker TurnSpeed => m_turningSpeedRef;
    [SerializeField]
    protected StatTracker m_movementSpeedRef;
    public StatTracker MoveSpeed => m_movementSpeedRef;

    protected Dictionary<CameraManager.CameraState, System.Action> m_movementResoultion;

    protected bool m_displayInventory;

    /// <summary>
    /// gets the camera manager on the main camera, then sets up a dictionary of all the possible camera states paired to movement resolution functions
    /// </summary>
    public void Start()
    {
        m_vision = Camera.main.GetComponent<CameraManager>();
        m_movementResoultion = new Dictionary<CameraManager.CameraState, System.Action>()
        {
           //{CameraManager.CameraState.Abzu, AbzuMovement },
           {CameraManager.CameraState.FirstPerson, FirstPersonMovement },
           //{CameraManager.CameraState.Locked, LockedMovement },
           {CameraManager.CameraState.Warthog, WarthogMovement },
        };
        m_turningSpeedRef.SetValue(180.0f);

        PlayerInstance.RegisterPlayerMotion(this);
    }
    /// <summary>
    /// creates a system.action (essentiually function with no in/out - funct ptr) 
    /// sets it to the movement resolution associated to the current camera state if valid
    /// runs the move resloution funct found
    /// </summary>
    public void FixedUpdate()
    {
        if (!m_CanMove)
            return;

        System.Action MoveResolution;
        if (!m_movementResoultion.TryGetValue(m_vision.CurrentState, out MoveResolution))
            throw new System.NotImplementedException("Camera state not reconized by Player motion ");

        MoveResolution();
    }

    public void Update()
    {
        if (!m_CanMove)
            return;

        if (ALInput.GetKeyDown(ALInput.Inventory))
            ToggleInventoryDisplay();

        if (ALInput.GetKeyDown(ALInput.AltAction))
        {
            List<FishInstance> resultingFish = SurroundingFish;
            if (resultingFish.Count == 0)
                return;
            m_CanMove = false;
            CombatManager.Instance.StartCombat(true, resultingFish, this);
        }
    }

    public void SetMoveSpeedTracker(StatTracker tracker)
    {
        m_movementSpeedRef = tracker;
    }

    public void SetTurnSpeedTracker(StatTracker tracker)
    {
        m_turningSpeedRef = tracker;
    }

    protected void ToggleInventoryDisplay()
    {
        m_displayInventory = !m_displayInventory;
        SwapUI();
    }

    protected void SwapUI()
    {
        MenuScreens desiredMenu = (m_displayInventory) ? MenuScreens.SwimmingInventory : MenuScreens.NormalHUD;
        NewMenuManager.DisplayMenuScreen(desiredMenu);
    }

    protected void WarthogMovement()
    {
        //hopefully will rotate the frog to be looking facedown towards object
        transform.LookAt(m_vision.LookAtWorldTransform, Vector3.up);

        XZDirectional();
    }

    protected void FirstPersonMovement()
    {
        //hopefully will rotate the frog to be looking facedown towards object
        transform.LookAt(m_vision.LookAtWorldTransform, Vector3.up);

        XZDirectional();
    }

    protected void XZDirectional()
    {
        Vector3 desiredMovement;
        //Move forward
        desiredMovement = transform.forward * Time.deltaTime * m_movementSpeedRef * ((ALInput.GetAxis(ALInput.AxisType.MoveVertical) > 0 ? 1: 0) - (ALInput.GetAxis(ALInput.AxisType.MoveVertical) < 0 ? 0.2f : 0));
        // Left Right
        desiredMovement += transform.right * Time.deltaTime * m_movementSpeedRef * ((ALInput.GetAxis(ALInput.AxisType.MoveHorizontal) > 0 ? 0.4f : 0) - (ALInput.GetAxis(ALInput.AxisType.MoveHorizontal) < 0 ? 0.4f : 0));
        // ascend descend Not setup on controller just yet.
        desiredMovement += Vector3.up * Time.deltaTime * m_movementSpeedRef * ((ALInput.GetKey(ALInput.AltAction) ? 0.5f : 0) - (ALInput.GetKey(ALInput.Cancel) ? 0.5f : 0));

        //apply movement vector
        if (!PlayerInstance.Instance.Oxygen.m_isUnderWater)
            desiredMovement.y = Mathf.Min(0, desiredMovement.y);

        transform.position += desiredMovement;
    }

    void ResolveSwimRotation()
    {
        Vector3 desiredDirection = ALInput.GetDirection(ALInput.DirectionCode.LookInput) * m_turningSpeedRef * Time.deltaTime;

        if (desiredDirection.sqrMagnitude > 0.000001)
            transform.Rotate(desiredDirection, Space.Self);
    }

    List<FishInstance> FindSurroundingFish()
    {
        float distance = float.MaxValue;

        List<FishInstance> FishInstances = new List<FishInstance>();

        var fishInRange = Physics.SphereCastAll(transform.position, m_sphereCastRadius, transform.forward);

        foreach (var fish in fishInRange)
        {
            if (fish.collider.gameObject.GetComponent<BasicFish>() == default)
                continue;
            FishInstance def = fish.collider.gameObject.GetComponent<BasicFish>().FishInstance;
            if (def == null)
                throw new System.Exception("Fish def was null for this fish!");

            FishInstances.Add(def);

            Vector3 offset = fish.transform.position - transform.position;
            if (offset.sqrMagnitude < distance)
            {
                distance = offset.sqrMagnitude;
                m_closestFish = def;
            }
        }
        if (FishInstances.Count == 0)
            return FishInstances;

        FishInstances.Remove(m_closestFish);
        FishInstances.Insert(0, m_closestFish);
        return FishInstances;
    }
}
