using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSlottable : MonoBehaviour, ISlotable
{

    public SlotTypes _SlotType;
    public SlotTypes m_slotType { get { return _SlotType; } }

    private bool _isHeldSlot;
    public bool m_isHeldSlot
    {
        get { return _isHeldSlot; }
        set
        {
            if (_isHeldSlot == value) //no change
                return;
            _isHeldSlot = value;
            if (!_isHeldSlot)
                return;

            _isHeldPlayer = false;
            m_TimeToDie = _timeToDissolve;

        }
    }
    private bool _isHeldPlayer;
    public bool m_isHeldPlayer
    {
        get { return _isHeldPlayer; }
        set
        {
            if (_isHeldPlayer == value) //no change
                return;
            _isHeldPlayer = value;
            if (!_isHeldPlayer)
                return;

            _isHeldSlot = false;         
            m_TimeToDie = _timeToDissolve;
        }
    }

    public void PlayerGrab()
    {
        if (m_lastSelected != null && m_lastSelected.m_slotted != null)
            m_lastSelected.Eject();
        m_isHeldPlayer = true;


    }
    public void PlayerDrop()
    {
        m_isHeldPlayer = false;

        if (m_lastSelected == null)
            return;
        // if object is within disance trigger it. little bit of a bug if the square is less than the minDistaceto dectect slots it's best if the value is greater than 0
        if (Mathf.Clamp(m_MaxDistanceToDetectSlots * m_MaxDistanceToDetectSlots, m_MinDistanceToDetectSlots, float.MaxValue) > Vector3.SqrMagnitude(m_lastSelected.gameObject.transform.position - transform.position))
            m_lastSelected.Accept(this);
        m_lastSelected.ToggleHighlighting();
    }


    private float m_TimeToDie = 30;
    public float _timeToDissolve;
    public float m_timeToDissolve { get { return _timeToDissolve; } }


    private ISlot _lastSeleted;
    public ISlot m_lastSelected
    {
        get { return _lastSeleted; }
        set
        {
            if (_lastSeleted == value) //same thing don't bother
                return;
            if (_lastSeleted != null) //Turn off highlighting of the second one
                _lastSeleted.ToggleHighlighting();
            //turn on highlighting of the new one, this logic will break if mul
            _lastSeleted = value;
            if(_lastSeleted != null)
                _lastSeleted.ToggleHighlighting();
        }
    }

    void Start()
    {
        m_TimeToDie = m_timeToDissolve;
    }

    private void FixedUpdate()
    {
        if (m_isHeldSlot)
            return;

        if (!m_isHeldPlayer)
        {
            m_TimeToDie -= Time.deltaTime;
            if (m_TimeToDie <= 0)
                GameObject.Destroy(this.gameObject);
            return;
        }

        //looking for a home
        GameObject newSlot = LookForSlot();

        if (newSlot == null)
        {
            if(m_lastSelected == null)
                return;
            m_lastSelected.ToggleHighlighting(false);
            m_lastSelected = null;
            return;
        }

        m_lastSelected = newSlot.GetComponent<ISlot>();
    }
    public float m_MinDistanceToDetectSlots = 0;
    public float m_MaxDistanceToDetectSlots = 0.1f;
    public string[] m_LayerMask = { "Slot Interaction" };
    private GameObject LookForSlot()
    {
        int mask = LayerMask.GetMask(m_LayerMask);
        RaycastHit[] hitinfos = Physics.SphereCastAll(gameObject.transform.position, m_MinDistanceToDetectSlots, Vector3.down, m_MaxDistanceToDetectSlots, mask);

        Vector3 thisLocation = gameObject.transform.position;
        GameObject closestSlot = null;
        float closestDist = float.MaxValue;


        foreach (RaycastHit hit in hitinfos)
        {
            //only care about valid slots
            if (!hit.collider.gameObject.GetComponentInParent<ISlot>().CanAccept(this))
                continue;

            //checks to see if it's closest
            float dist = Vector3.SqrMagnitude(thisLocation - hit.collider.gameObject.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestSlot = hit.collider.gameObject;
            }
        }

        return closestSlot;
    }
}
