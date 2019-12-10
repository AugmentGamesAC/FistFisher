using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishReactingToPlayerSwimmingInAnAreaNearThemOrSomething : MonoBehaviour
{
    protected InfluenceFish m_influenceFish;
    private Player m_player;
    public Vector3 m_playerPosition;
    public Quaternion m_playerRotation;

    private bool m_playerHasMovedThisUpdate = false;

    // Start is called before the first frame update
    void Start()
    {
        m_influenceFish = GetComponent<InfluenceFish>();

        if (m_player == null)
            m_player = gameObject.GetComponentInChildren<Player>();
        if (m_playerPosition == null)
            m_playerPosition = m_player.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Transform testAgainst = m_player.gameObject.transform;
        float f = (testAgainst.position - m_playerPosition).sqrMagnitude;
        if (f > 0.00007f) //the Brian number
        {
            m_playerHasMovedThisUpdate = true;
         //   Debug.LogError("Player Moved");
        }
        else
            m_playerHasMovedThisUpdate = false;
    }
    //runs before usually update
    void FixedUpdate()
    {
        m_playerPosition = m_player.gameObject.transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (m_influenceFish != null && m_playerHasMovedThisUpdate)
        {
            m_influenceFish.ImpressFish(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (m_influenceFish != null && m_playerHasMovedThisUpdate)
        {
            m_influenceFish.ImpressFish(other.gameObject);
        }
    }
}
