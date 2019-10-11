using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Throwable))]
public class BasicSlottable : ASlottable
{


    public override void ToggleKinematicAndGravityAndSphereCollider(bool isOwnObject)
    {
        ToggleKinematicAndGravityAndSphereCollider(!isOwnObject, isOwnObject, isOwnObject);
    }

    public override void ToggleKinematicAndGravityAndSphereCollider(bool kinematic, bool gravity, bool sphereCollider)
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = kinematic;
            rb.useGravity = gravity;
        }
        else
        {
            Debug.LogError("Missing Rigid Body!");
        }

        //this chunk disabled for now as slipping through ground is a larger issue than collision and should be uncommented once it's being used with VR
        /*SphereCollider sc = gameObject.GetComponent<SphereCollider>(); 
        if (sc != null)
        {
            sc.isTrigger = !sphereCollider;
        }
        else
        {
            Debug.LogError("Missing Collider!");
        }*/
    }

    public override void PlayerDrop()
    {
        m_IsGrabbed = false;
        ToggleKinematicAndGravityAndSphereCollider(true);
    }

    public override void PlayerGrab()
    {
        m_IsGrabbed = true;
        ToggleKinematicAndGravityAndSphereCollider(false);
    }

    public override void SlotDrop()
    {
        m_SlotRef.WasEmptied();
        m_SlotRef = null;
        ToggleKinematicAndGravityAndSphereCollider(true);

    }


    /// <summary>
    /// Handles the logic for figuring out when it was close to a slot and if it needs to highlight it or not.
    /// </summary>
    private void ResolveSlot()
    {
        if(m_IsSlotted == true)
        {
            m_IsSlotted = false;
            if(m_SlotRef!=null)
            {
                SlotDrop();
            }
        }

        m_TimeToDie = m_TimeToDissolve; //reset despawn timer if held. should keep it at reset time when detecting and slotting

        ASlot closestSlot = LookForSlot();
        if (closestSlot!=null)
        {
            if (m_LastSelected != null && m_LastSelected!=closestSlot)
            {
                m_LastSelected.ToggleHighlighting(false);
            }
            m_LastSelected = closestSlot;
            closestSlot.ToggleHighlighting(true);
        }
    }

    //spherecast for slots
    private ASlot LookForSlot()
    {
        //Debug.Log(m_LayerMask);

        int mask = -LayerMask.GetMask(m_LayerMask);
        RaycastHit[] hitinfos = Physics.SphereCastAll(gameObject.transform.position, m_MinDistanceToDetectSlots, Vector3.down, m_MaxDistanceToDetectSlots, mask);

        Vector3 thisLocation = gameObject.transform.position;
               
        ASlot closestSlot = null;
        float closestDist = float.MaxValue;


        foreach (RaycastHit hit in hitinfos)
        {
            ASlot hitSlot = hit.collider.gameObject.GetComponentInParent<ASlot>();
            if (hitSlot != null && hitSlot.CanAccept(this))
            {
                //Debug.Log("hit " + hitSlot.name);
                //checks to see if it's closest
                float dist = Vector3.SqrMagnitude(thisLocation - hit.collider.gameObject.transform.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestSlot = hitSlot;
                }
            }
        }
        if(closestSlot!=null)
            Debug.Log("found slot: " + closestSlot.name);
        return closestSlot;
    }


    public void Start()
    {
        m_SlotType = SlotTypes.SpellCrystal;  //TEMPORARY TO MAKE SURE IT WORKS!
    }


    public bool m_showDebugSpheres = true;
    //if toggled on, shows inner and ouret spheres to refine slottable slot detection radius
    void OnDrawGizmos()
    {
        if (m_showDebugSpheres)
        {
            Color innerColour = new Color(1, 0, 0, 0.5f);
            Gizmos.color = innerColour;
            Gizmos.DrawSphere(transform.position, m_MinDistanceToDetectSlots);

            Color outerColour = new Color(0, 1, 0, 0.25f);
            Gizmos.color = outerColour;
            Gizmos.DrawSphere(transform.position, m_MaxDistanceToDetectSlots);
        }
    }

    private void FixedUpdate() 
    {
        if (m_IsGrabbed) //if being held by player
        {
            ResolveSlot();
        }
        else
        {
            if(m_SlotRef==null) //if not held by slot or player
                m_TimeToDie -= Time.deltaTime;
        }

        if (m_TimeToDie <= 0)
            Destroy(gameObject);









        if (Input.GetKeyDown(KeyCode.H))
        {
            m_IsGrabbed = !m_IsGrabbed;
            Debug.LogError("grabbed: " + m_IsGrabbed);

            ToggleKinematicAndGravityAndSphereCollider(!m_IsGrabbed);
        }



        if (m_LastSelected != null && Input.GetKeyDown(KeyCode.L))
        {
            float dist = Vector3.SqrMagnitude(gameObject.transform.position - m_LastSelected.gameObject.transform.position);
            //something in here about distance to connect or whatever. maybe good enough that it's within detection range if it hits this
            if (m_SlotRef != null)
                SlotDrop();
            else
            {
                if (m_LastSelected.Accept(this))
                {
                    m_SlotRef = m_LastSelected;
                    m_IsGrabbed = false;
                }
            }
        }
    }
          
}
