using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FishSpawnerMapVisibility : MonoBehaviour
{
    public Material m_boatMapMaterial;
    public SphereCollider m_radiusWeAreUsingForThisObject;
    public TextMeshPro m_Text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        if (m_radiusWeAreUsingForThisObject != null)
        {
            Vector3 scl = gameObject.transform.localScale;
            scl *= m_radiusWeAreUsingForThisObject.radius;
            gameObject.transform.localScale = scl;
        }

        if(m_boatMapMaterial!=null)
            gameObject.GetComponent<Renderer>().material = m_boatMapMaterial;
        if(m_Text!=null)
        {
            /*FishSpawner fsp = GetComponentInParent<FishSpawner>();
            if(fsp!=null)
                m_Text.text = */
            //Vector3 v = gameObject.transform.position + gameObject.transform.up * m_radiusWeAreUsingForThisObject.radius;
            Vector3 v = gameObject.transform.position;
            v.y = 30.0f;
            m_Text.gameObject.transform.position = v;
            m_Text.gameObject.transform.localScale = Vector3.one * 3.0f;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
    }


}
