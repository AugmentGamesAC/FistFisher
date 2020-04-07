using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// when combat was going to be in-world, this was a way to show effects when a fish was hit
/// </summary>
[System.Serializable]
public class HitEffects : MonoBehaviour
{
    [SerializeField]
    public List<Material> m_hitEffects;
    [SerializeField]
    public GameObject m_effectPlane;
    public float m_timeToShowHitEffect = 0.5f;
    float m_timeToStopShowing = 0.0f;

    Quaternion m_startRot;

    /// <summary>
    /// when fish is hit, pick random effect texture from list and apply it to plane
    /// </summary>
    public void Hit()
    {
        m_timeToStopShowing = m_timeToShowHitEffect;

        Material m = m_hitEffects[Random.Range(0, m_hitEffects.Count)];

        if (m_effectPlane != null)
            m_effectPlane.GetComponent<Renderer>().material = m;

        m_effectPlane.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnDisable()
    {
        m_timeToStopShowing = 0.0f;
        m_effectPlane.SetActive(false);
    }

    void Awake()
    {
        m_effectPlane.SetActive(false);
        m_startRot = m_effectPlane.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_timeToStopShowing > 0.0f)
            m_timeToStopShowing -= Time.deltaTime;
        else
            m_effectPlane.SetActive(false);

        m_effectPlane.transform.rotation = m_startRot;
    }
}
