using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : MonoBehaviour
{
    public float m_maxDuration = 200.0f;
    private float m_currentDuration;

    [SerializeField]
    protected PondManager m_pond;
    public PondManager Pond { get { return m_pond; } }

    // Start is called before the first frame update
    void Start()
    {


    }

    public void Init()
    {

        gameObject.SetActive(true);
        m_currentDuration = m_maxDuration;

        float m_radius = gameObject.GetComponent<SphereCollider>().radius; //hacky way to get radius for sphere overlap as opposed to collisions
        Collider[] hits = Physics.OverlapSphere(gameObject.transform.position, m_radius);
        int i = 0;
        while (i < hits.Length)
        {
            PondManager f = hits[i].GetComponent<PondManager>();
            if (f != null)
            {
                m_pond = f;
                return;
            }
            else
                i++;
        }

        NotifyPond(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_currentDuration <= 0)
            BaitDespawn();
        if(m_pond==null)
        {

        }
        m_currentDuration -= Time.deltaTime;
    }

    void NotifyPond(bool alive)
    {
        m_pond.ChangeBaitPresenceInThisPond(gameObject, alive);
    }

    void BaitDespawn()
    {
        NotifyPond(false);
        gameObject.SetActive(false);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fish")
        {
            other.GetComponentInParent<BasicFish>().m_inBaitZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Fish")
        {
            other.GetComponentInParent<BasicFish>().m_inBaitZone = false;
        }
    }
}
