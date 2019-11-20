using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BasicFish : MonoBehaviour
{
    public float m_personalSpaceRadius = 1.0f;

    public float Speed;
    public float TurnSpeed;

    public Transform LookFrom;
    public FishSpawner Spawner;

    [SerializeField]
    protected FishArchetype m_fishArchetype;
    public FishArchetype FishType { get { return m_fishArchetype; } }

    public class FishyPart : InspectorDictionary<Collider, float> { }
    [SerializeField]
    protected FishyPart m_hitBoxModifiers;

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

        if (m_healthModule != null)
            m_healthModule.OnDeath += HandleDeath;

        m_camera = Camera.main;
        m_isListed = false;

        //Get all hitboxes from each fish compound colliders.
        FishHitBox[] m_hitBoxes = GetComponentsInChildren<FishHitBox>();

        if (m_hitBoxes != null)
        {
            //add them to our InspectorDictionary.
            foreach (FishHitBox hitBox in m_hitBoxes)
            {
                //m_hitBoxModifiers.Add(hitBox.m_HitBoxModifier.Key, hitBox.m_HitBoxModifier.Value);
            }
        }
    }

    private void Update()
    {
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
    }
}
