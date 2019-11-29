using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : MonoBehaviour
{
    public float m_maxDuration = 200.0f;
    private float m_currentDuration;
    public bool m_activeInWorld = false;

    [SerializeField]
    protected PondManager m_pond;
    public PondManager Pond { get { return m_pond; } }

    // Start is called before the first frame update
    void Start()
    {


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
        InfluenceFish(other);
    }

    private void OnTriggerStay(Collider other)
    {
        InfluenceFish(other);
    }

    protected void InfluenceFish(Collider other)
    {
        FishBrain fb = other.GetComponentInParent<FishBrain>();
        if (fb == default)
            return;

        fb.ApplyImpulse(gameObject, 0.1f, FishBrain.FishClassification.BaitSensitive1);
    }
}


