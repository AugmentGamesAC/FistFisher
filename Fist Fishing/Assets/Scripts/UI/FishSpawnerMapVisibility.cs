using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/// <summary>
/// has been replaced in biome instance
/// was used to create overhead-map-visible-only spheres and text for fish spawners
/// </summary>
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
        //scale the sphere
        if (m_radiusWeAreUsingForThisObject != null)
        {
            Vector3 scl = gameObject.transform.localScale;
            scl *= m_radiusWeAreUsingForThisObject.radius;
            gameObject.transform.localScale = scl;
        }
        //apply colour/meterial to sphere
        if(m_boatMapMaterial!=null)
            gameObject.GetComponent<Renderer>().material = m_boatMapMaterial;

        //position and scale text label
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
