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
        m_currentDuration = m_maxDuration;
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

    }

    void BaitDespawn()
    {

    }

}
