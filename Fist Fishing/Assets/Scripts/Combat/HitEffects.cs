using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HitEffects : MonoBehaviour
{
    [SerializeField]
    public List<Material> m_hitEffects;
    [SerializeField]
    public GameObject m_effectPlane;
    public float m_timeToShowHitEffect = 0.5f;
    float m_timeToStopShowing = 0.0f;



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
    }

    // Update is called once per frame
    void Update()
    {
        if (m_timeToStopShowing > 0.0f)
            m_timeToStopShowing -= Time.deltaTime;
        else
            m_effectPlane.SetActive(false);
    }
}
