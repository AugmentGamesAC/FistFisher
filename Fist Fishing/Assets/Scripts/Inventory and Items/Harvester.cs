using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Inventory))]
public class Harvester : MonoBehaviour
{
    private Inventory m_invRef;


    public GameObject m_targetPrefab;
    public float m_targetCloseness = 0.9f;

    public float m_angleToHarvest = 30.0f;
    public float m_rangeToView = 20.0f;
    public float m_rangeToHarvest = 10.0f;


    [SerializeField]
    protected List<GameObject> m_harvestablesInView = new List<GameObject>();
    public List<GameObject> HarvestablesInView { get { return m_harvestablesInView; } }

    //public GameObject m_targetted;
    public bool m_targetingIsActive;

    [SerializeField]
    protected GameObject m_toHarvest;
    public GameObject ToHarvest { get { return m_toHarvest; } }

    // Start is called before the first frame update
    void Start()
    {
        m_targetingIsActive = false;
        m_invRef = gameObject.GetComponent<Inventory>();
        m_targetPrefab = Instantiate(m_targetPrefab, new Vector3(0, -99.9f, 0), Quaternion.identity);
        m_targetPrefab.SetActive(false);

    }

    private void ToggleTargetingReticle(bool targetingIsActive)
    {
        m_targetingIsActive = targetingIsActive;
        m_targetPrefab.SetActive(m_targetingIsActive);
    }


    /// <summary>
    /// polls everything in range, compiles a list of harvestables in view
    /// </summary>
    void UpdateHarvestableList()
    {
        HarvestablesInView.Clear();
        Collider[] hits = Physics.OverlapSphere(gameObject.transform.position, m_rangeToView);
        int i = 0;
        while (i < hits.Length)
        {
            if (hits[i].tag == "Harvestable")
            {
                Vector3 harvestablePosition = Camera.main.WorldToViewportPoint(hits[i].gameObject.transform.position);
                bool onScreen = harvestablePosition.z > 0 && harvestablePosition.x > 0 && harvestablePosition.x < 1 && harvestablePosition.y > 0 && harvestablePosition.y < 1;
                if(onScreen)
                {
                    HarvestablesInView.Add(hits[i].gameObject);
                }
            }
            i++;
        }


    }

    /// <summary>
    /// goes through harvestables in view, determines closest
    /// </summary>
    GameObject DetermineClosestHarvestable()
    {
        if (HarvestablesInView.Count == 0)
            return null;

        float dist = float.MaxValue;

        GameObject g = null;

        foreach(GameObject h in m_harvestablesInView)
        {
            float distH = (h.transform.position - gameObject.transform.position).sqrMagnitude;
            if (distH <= dist)
            {
                dist = distH;
                g = h;
            }
        }

        return g;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHarvestableList();
        m_toHarvest = DetermineClosestHarvestable();
        if (m_toHarvest == null)
        {
            ToggleTargetingReticle(false);
            return;
        }
        else
        {
            ToggleTargetingReticle(true);
        }


        if (m_targetingIsActive) //reticle
        {
            Vector3 targetPos = m_toHarvest.gameObject.transform.position;
            Vector3 cameraPos = Camera.main.transform.position;
            Vector3 newPos = Vector3.Lerp(cameraPos, targetPos, m_targetCloseness);

            m_targetPrefab.gameObject.transform.position = newPos;
        }
        

        if (m_toHarvest!=null && ALInput.GetKeyDown(ALInput.Harvest))
        {
            Harvest();
        }
    }

    void Harvest()
    {
        Vector3 offsetCam = m_toHarvest.transform.position - Camera.main.transform.position;
        Vector3 offsetPlayer = m_toHarvest.transform.position - gameObject.transform.position;
        Vector3 targetDir = offsetCam.normalized;
        float diffAngle = Mathf.Acos(Vector3.Dot(targetDir, Camera.main.transform.forward)) * Mathf.Rad2Deg;

        float sqrLen = offsetPlayer.sqrMagnitude;

        if (diffAngle <= m_angleToHarvest && sqrLen < (m_rangeToHarvest * m_rangeToHarvest))
            m_invRef.AddToInventory(m_toHarvest);
    }

}
