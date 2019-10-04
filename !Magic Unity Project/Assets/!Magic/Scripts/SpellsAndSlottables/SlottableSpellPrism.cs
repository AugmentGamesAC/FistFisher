using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//a slottable completed spell
//does a spherecast to determine nearby slots it can go into, and allows attaching/detaching from that slot
public class SlottableSpellPrism : MonoBehaviour, ISlotable
{


    public float m_slotDetectionInner = 0.01f;
    public float m_slotDetectionOuter = 0.1f;
    public float m_ditancePulledBeforeDetachingFromSlot = 0.01f;

    public SlotTypes m_slotType { get; set; }


    public bool m_isHeldSlot { get; set; }
    [SerializeField]
    public bool m_isHeldPlayer { get; set; }

    public float m_timeToDissolve { get; set; }
    float m_disovleTimeRemaining;

    public ISlot m_lastSelected { get; set; }

    public Spell m_spell;

    public int m_spellID;

    public void SetSpellID(int spellID) { m_spellID = spellID; }

    // Start is called before the first frame update
    void Start()
    {
        m_isHeldSlot = false;
        m_isHeldPlayer = false;
        m_slotType = SlotTypes.SpellCrystal;
        m_timeToDissolve = 30.0f;
        //m_lastSelected
        m_disovleTimeRemaining = m_timeToDissolve;

        //m_spellID = int.MaxValue;

    }

    // Update is called once per frame
    //this is where the spherecasting is for slot detection
    void Update()
    {


        if (!m_isHeldSlot)
        {
            if (m_isHeldPlayer)
            {
                int mask = LayerMask.GetMask("Slot Interaction");
                RaycastHit[] hitinfos = Physics.SphereCastAll(gameObject.transform.position, m_slotDetectionInner, Vector3.down, m_slotDetectionOuter, mask);

                Vector3 thisLocation = gameObject.transform.position;
                GameObject closestSlot = null;
                float closestDist = float.MaxValue;


                foreach (RaycastHit hit in hitinfos)
                {
                    float dist = Vector3.Distance(thisLocation, hit.collider.gameObject.transform.position);
                    if (dist < closestDist && hit.collider.gameObject.GetComponent<GauntletSlots>().CanAccept(this))
                    {
                        closestDist = dist;
                        closestSlot = hit.collider.gameObject;
                    }
                }

                if (closestSlot != null)
                {
                    if (m_lastSelected != null)
                    {
                        if ((GauntletSlots)m_lastSelected != closestSlot.GetComponent<GauntletSlots>())
                        {
                            m_lastSelected.ToggleHighlighting(); //toggle old back to default
                            m_lastSelected = closestSlot.GetComponent<GauntletSlots>();
                            m_lastSelected.ToggleHighlighting(); //toggle new
                        }
                    }
                    else
                    {
                        m_lastSelected = closestSlot.GetComponent<GauntletSlots>();
                        m_lastSelected.ToggleHighlighting(); //toggle new
                    }
                }
                else if (closestSlot == null && m_lastSelected != null)
                {
                    m_lastSelected.ToggleHighlighting(); //toggle old back to default
                    m_lastSelected = null;
                }
            }
            else
            {
                m_disovleTimeRemaining -= Time.deltaTime; //slowly count down
            }
        }
        else
        {

            if (m_isHeldPlayer)
            {

                GauntletSlots slotCurrentlyIn = (GauntletSlots)m_lastSelected;
                if (slotCurrentlyIn != null)
                {
                    float dist = Vector3.Distance(gameObject.transform.position, slotCurrentlyIn.transform.position);
                    if (dist > m_ditancePulledBeforeDetachingFromSlot)
                    {
                        DetachFromSlot();
                    }
                }
            }
        }


        //temp keys
        {
            //Temp key to attach/detach
            //this'll have to be changed with the logic I don't yet know of to pickup
            /*if (Input.GetKeyDown(KeyCode.L))
            {
                if (m_lastSelected != null && m_isHeldSlot == false)
                {
                    AttachToSlot();
                }
                else if (m_lastSelected != null && m_isHeldSlot == true)
                {
                    DetachFromSlot();
                }
            }*/

            if (Input.GetKeyDown(KeyCode.J))
            {
                m_isHeldPlayer = !m_isHeldPlayer;
            }
        }


    }

    //if it's in a valid slot, attach this to that slot, disable the rigidbody, and pass relenet vars to slot
    public void AttachToSlot()
    {
        if (m_lastSelected == null)
            return;
        if (m_lastSelected.Accept(this))
        {
            m_isHeldSlot = true;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            GauntletSlots slot = (GauntletSlots)m_lastSelected;
            if(slot !=null)
            {
                slot.m_spellID = this.m_spellID;
            }
        }
    }

    //if detached, reset relevent vars in slot, reset disolve timer, unparent
    public void DetachFromSlot()
    {
        if (m_lastSelected == null)
            return;
        GauntletSlots oldSlot = (GauntletSlots)m_lastSelected;
        oldSlot.SpellPrismRemovedFromSlot();

        m_lastSelected.m_slotted = null;
        m_isHeldSlot = false;
        //gameObject.transform.parent = null;
        m_disovleTimeRemaining = m_timeToDissolve; //reset disolve timer


    }


}
