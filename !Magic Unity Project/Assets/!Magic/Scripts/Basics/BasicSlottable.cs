using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


[RequireComponent(typeof(Valve.VR.InteractionSystem.Throwable))]
public class BasicSlottable : ASlottable
{
    protected Rigidbody m_MyRigidbody;
    protected Collider m_MyCollider;

    public void Start()
    {
        m_MyRigidbody = gameObject.GetComponent<Rigidbody>();
        m_MyCollider = gameObject.GetComponent<Collider>();
        if (m_MyCollider == null)
            throw new System.InvalidOperationException("No Collider was found");
    }

    private void ToggleKinematicAndGravityAndSphereCollider(bool isOwnObject)
    {
        ToggleKinematicAndGravityAndSphereCollider(!isOwnObject, isOwnObject);
    }

    private void ToggleKinematicAndGravityAndSphereCollider(bool kinematic, bool gravity)
    {
        m_MyRigidbody.isKinematic = kinematic;
        m_MyRigidbody.useGravity = gravity;
    }

    public override void PlayerDrop()
    {
        m_IsGrabbed = false;
        ToggleKinematicAndGravityAndSphereCollider(true);

        PutDroppedObjectIntoSlot();
        m_TimeToDie = m_TimeToDissolve;
    }

    public override void PlayerGrab()
    {
        m_IsGrabbed = true;
        
        if (m_SlotRef != null)
            m_SlotRef.Eject();

        ToggleKinematicAndGravityAndSphereCollider(false);
    }

    public override void SlotDrop()
    {
        m_SlotRef = null;
        ToggleKinematicAndGravityAndSphereCollider(true);
        m_TimeToDie = m_TimeToDissolve;
        m_IsSlotted = false;
    }
    /// <summary>
    /// Handles the logic for figuring out when it was close to a slot and if it needs to highlight it or not.
    /// </summary>
    private void ResolveSlot()
    {
        ASlot closestSlot = LookForSlot(); //scan for valid slots
        if (m_LastSelected == closestSlot) //NoChange do nothing
            return;

        if (m_LastSelected != null) //make sure previous selection is unhighlighted
            m_LastSelected.ToggleHighlighting(false);
        m_LastSelected = closestSlot;

        if (m_LastSelected != null) //hightlight new selection if it's not null.
            m_LastSelected.ToggleHighlighting(true);
    }

    //spherecast for slots
    private ASlot LookForSlot()
    {
        int mask = LayerMask.GetMask(m_LayerMask);
        RaycastHit[] hitinfos = Physics.SphereCastAll(gameObject.transform.position, m_MinDistanceToDetectSlots, Vector3.down, m_MaxDistanceToDetectSlots, mask);

        Vector3 thisLocation = gameObject.transform.position;
               
        ASlot closestSlot = null;
        float closestDist = float.MaxValue;


        foreach (RaycastHit hit in hitinfos)
        {
            ASlot hitSlot = hit.collider.gameObject.GetComponentInParent<ASlot>();
            if (hitSlot == null || !hitSlot.CanAccept(this))
                continue;
                //checks to see if it's closest
            float dist = Vector3.SqrMagnitude(thisLocation - hit.collider.gameObject.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestSlot = hitSlot;
            }

        }
        /*if(closestSlot!=null)
            Debug.Log("found slot: " + closestSlot.name);*/
        return closestSlot;
    }

    //if the player lets go of slottable, it gets added to slot
    private void PutDroppedObjectIntoSlot()
    {
        if (m_LastSelected == null)
            return;

        m_LastSelected.ToggleHighlighting(false);
        m_IsSlotted = m_LastSelected.Accept(this);
        if (m_IsSlotted)
            m_SlotRef = m_LastSelected;

        ToggleKinematicAndGravityAndSphereCollider(false);
        m_LastSelected = null;
    }

    private void FixedUpdate() 
    {
        if (m_IsGrabbed) // is being held by player
            ResolveSlot();
        else if (!m_IsSlotted) //isn't being held, observe time issues.
        {
            m_TimeToDie -= Time.deltaTime;
            if (m_TimeToDie <= 0)
                Destroy(gameObject);
        }
    }
          
}
