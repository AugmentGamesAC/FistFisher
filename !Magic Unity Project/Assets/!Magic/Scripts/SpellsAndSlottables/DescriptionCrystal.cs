using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionCrystal : MonoBehaviour, ISlotable
{
    public SpellDescription.Aiming m_Aiming;
    public SpellDescription.Effect m_Effect;
    public SpellDescription.Shape m_Shape;
    public SpellDescription.Usage m_Usage;

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

    private float m_TimeToDie = 0;
    public float _timeToDissolve;
    float ISlotable.m_timeToDissolve { get { return _timeToDissolve; } }


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
            _lastSeleted.ToggleHighlighting();
        }
    }



    private void FixedUpdate()
    {
        if (m_isHeldSlot)
            return;

        if (!m_isHeldPlayer)
        {
            m_TimeToDie -= Time.deltaTime;
            if (m_TimeToDie <= 0)
               GameObject.Destroy(this);
            return;
        }

        //looking for a home
        GameObject gameObject = LookForSlot();

        if (gameObject == null)
            return;

        m_lastSelected = m_lastSelected;
    }

    public float m_distanceToDetectSlots;
    public string m_LayerMask = "Slot Interaction";
    private GameObject LookForSlot()
    {
        int mask = LayerMask.GetMask(m_LayerMask);
        RaycastHit[] hitinfos = Physics.SphereCastAll(gameObject.transform.position, m_distanceToDetectSlots, Vector3.down, 0.1f, mask);
      
        Vector3 thisLocation = gameObject.transform.position;
        GameObject closestSlot = null;
        float closestDist = float.MaxValue;


        foreach (RaycastHit hit in hitinfos)
        {
            //only care about valid slots
            if (!hit.collider.gameObject.GetComponent<ISlot>().CanAccept(m_slotType))
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
