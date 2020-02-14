using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BasicFish : MonoBehaviour
{



    #region working inspector dictionary
    /// <summary>
    /// this is the mess required to make dictionaries with  list as a value work in inspector
    /// used in this case to pair enum of menu enum with a list of menu objects
    /// </summary>
    [System.Serializable]
    public class ListOfFishHitboxes : InspectorDictionary<Collider, float> { }
    [SerializeField]
    protected ListOfFishHitboxes m_fishHitboxList = new ListOfFishHitboxes();
    public ListOfFishHitboxes FishHitboxList { get { return m_fishHitboxList; } }


    #endregion working inspector dictionary













    public float m_personalSpaceRadius = 1.0f;

    public float Speed;
    public float TurnSpeed;

    [SerializeField]
    protected FishBrain.FishClassification m_fishClass = FishBrain.FishClassification.Fearful;
    public FishBrain.FishClassification FishClass {  get { return m_fishClass; } }

    public Transform LookFrom;
    public FishSpawner Spawner;

    public bool m_inBaitZone = false;

    [SerializeField]
    protected FishArchetype m_fishArchetype;
    public FishArchetype FishArcheType { get { return m_fishArchetype; } }

    /*public class FishyPart : InspectorDictionary<Collider, float> { }
    [SerializeField]
    protected FishyPart m_hitBoxModifiers;*/

    private Camera m_camera;
    public TargetController m_targetController;//don't set this manually.
    public bool m_isListed;

    //behaviour
    protected BehaviorTree m_behaviour;
    protected HealthModule m_healthModule;

    // Start is called before the first frame update
    void Start()
    {
        m_healthModule = GetComponent<HealthModule>();
        m_behaviour = GetComponent<BehaviorTree>();

        if (m_healthModule != null)
            m_healthModule.OnDeath += HandleDeath;

        m_camera = Camera.main;
        m_isListed = false;
        m_targetController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<TargetController>();

        //Get all hitboxes from each fish compound colliders.
        /*FishHitBox[] m_hitBoxes = GetComponentsInChildren<FishHitBox>();

        if (m_hitBoxes != null)
        {
            //add them to our InspectorDictionary.
            foreach (FishHitBox hitBox in m_hitBoxes)
            {
                //m_hitBoxModifiers.Add(hitBox.m_HitBoxModifier.Key, hitBox.m_HitBoxModifier.Value);
            }
        }*/
    }

    private void Update()
    {

        if(m_healthModule.CurrentHealth<=0.0f)
        {
            m_targetController.m_fishInViewList.Remove(gameObject);
            m_isListed = false;
            return;
        }

        //move all this stuff to a targetable class.
        Vector3 fishPosition = m_camera.WorldToViewportPoint(gameObject.transform.position);

        //Checks if the position is in viewport.
        bool onScreen = fishPosition.z > 0 && fishPosition.x > 0 && fishPosition.x < 1 && fishPosition.y > 0 && fishPosition.y < 1;

        if (m_targetController == null)
            return;

        //adds and removes fish from a targetable list.
        if (onScreen && !m_isListed)
        {
            m_targetController.m_fishInViewList.Add(gameObject);
            m_isListed = true;
        }
        else if(!onScreen && m_isListed)
        {
            m_targetController.m_fishInViewList.Remove(gameObject);
            m_isListed = false;
        }
    }

    private void HandleDeath()
    {
        //ObjectPool should Handle fish.

        //Fish turns into a harvestable.
        Harvestable h = gameObject.AddComponent<Harvestable>();
        h.m_harvestableType = HarvestableType.DeadFish;
        h.m_targetController = m_targetController;
        tag = "Harvestable";

        Collider[] fishyParts = GetComponents<Collider>();

        foreach(Collider fp in fishyParts)
        {
            fp.tag = "Harvestable";
        }

        //gameObject.SetActive(false);
        m_behaviour.enabled = false;
        //m_healthModule.ResetHealth();
        m_healthModule.enabled = false;


    }
}
