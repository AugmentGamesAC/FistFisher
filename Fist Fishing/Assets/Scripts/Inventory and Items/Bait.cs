using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InfluenceFish))]
public class Bait : MonoBehaviour
{
    [SerializeField]
    protected float m_maxDuration = 200.0f;
    protected float m_currentDuration;
    [SerializeField]
    protected bool m_activeInWorld = false;

    protected InfluenceFish m_influenceFish;


    [SerializeField]
    protected PondManager m_pond;
    public PondManager Pond { get { return m_pond; } }

    // Start is called before the first frame update
    void Start()
    {
        m_influenceFish = GetComponent<InfluenceFish>();

        gameObject.SetActive(true);
    }

    public void Init()
    {
        m_activeInWorld = true;
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
        if (m_currentDuration <= 0 && m_activeInWorld)
            BaitDespawn();
        /*if(m_pond!=null)
        {

        }*/
        if(m_activeInWorld)
            m_currentDuration -= Time.deltaTime;
    }

    void NotifyPond(bool alive)
    {
        if(m_pond!=null)
            m_pond.ChangeBaitPresenceInThisPond(gameObject, alive);
    }

    void BaitDespawn()
    {
        NotifyPond(false);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_influenceFish != null)
            m_influenceFish.ImpressFish(other.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (m_influenceFish != null)
            m_influenceFish.ImpressFish(other.gameObject);
    }


}


