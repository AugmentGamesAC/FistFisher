using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//a slottable completed spell
//does a spherecast to determine nearby slots it can go into, and allows attaching/detaching from that slot
public class SlottableSpellPrism : MonoBehaviour, ISlotable
{


    public float m_distanceToDetectSlots = 0.01f;
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


        //m_spellID = Random.Range(1, 5);

    }

    // Update is called once per frame
    void Update()
    {


        if (!m_isHeldSlot)
        {
            if (m_isHeldPlayer)
            {
                int mask = LayerMask.GetMask("Slot Interaction");
                RaycastHit[] hitinfos = Physics.SphereCastAll(gameObject.transform.position, m_distanceToDetectSlots, Vector3.down, 0.1f, mask);

                Vector3 thisLocation = gameObject.transform.position;
                GameObject closestSlot = null;
                float closestDist = float.MaxValue;


                foreach (RaycastHit hit in hitinfos)
                {
                    float dist = Vector3.Distance(thisLocation, hit.collider.gameObject.transform.position);
                    //Debug.Log("SlotDistance: " + dist);
                    //Debug.Log("found collision: " + hit.collider.gameObject);
                    if (dist < closestDist && hit.collider.gameObject.GetComponent<GauntletSlots>().CanAccept(m_slotType))
                    {
                        closestDist = dist;
                        closestSlot = hit.collider.gameObject;
                    }
                }

                if (closestSlot != null)
                {
                    if (m_lastSelected != null)
                    {
                        if (m_lastSelected != closestSlot.GetComponent<GauntletSlots>())
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

        //Temp key to attach/detach
        //this'll have to be changed with the logic I don't yet know of to pickup
        if (Input.GetKeyDown(KeyCode.L))
        {
            //Debug.Log("pressed");
            if (m_lastSelected != null && m_isHeldSlot == false)
            {
                AttachToSlot();
            }
            else if (m_lastSelected != null && m_isHeldSlot == true)
            {
                DetachFromSlot();
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            m_isHeldPlayer = !m_isHeldPlayer;
            //Debug.LogError("Held?: " + m_isHeldPlayer);
        }
        //Debug.Log("To Despawn: " + m_disovleTimeRemaining);



    }

    public void AttachToSlot()
    {
        if (m_lastSelected == null)
            return;
        //Debug.Log("attached");
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
    public void DetachFromSlot()
    {
        if (m_lastSelected == null)
            return;
        //Debug.Log("detached");
        GauntletSlots oldSlot = (GauntletSlots)m_lastSelected;
        oldSlot.SpellPrismRemovedFromSlot();

        m_lastSelected.m_slotted = null;
        m_isHeldSlot = false;
        //gameObject.GetComponent<Rigidbody>().AddExplosionForce(10.0f, Vector3.up, 5.0f);
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.transform.parent = null;
        m_disovleTimeRemaining = m_timeToDissolve; //reset disolve timer


    }

    void OnDrawGizmosSelected()
    {
                //Gizmos.color = Color.yellow;
                //Gizmos.DrawSphere(gameObject.transform.position, 0.1f);
    }



}
