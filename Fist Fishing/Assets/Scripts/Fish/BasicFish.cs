using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BasicFish : MonoBehaviour
{
    public float m_personalSpaceRadius = 2.0f;

    [SerializeField]
    protected FishArchetype m_fishArchetype;
    public FishArchetype FishType { get { return m_fishArchetype; } }

    [SerializeField]
    public class FishyPart : InspectorDictionary<Collider, float> { }
    [SerializeField]
    public FishyPart m_hitBoxModifiers = new FishyPart();

    //behaviour
    protected BehaviorTree m_behaviour;

    // Start is called before the first frame update
    void Start()
    {
        //Get all hitboxes from each fish compound colliders.
        /*FishHitBox[] m_hitBoxes = GetComponentsInChildren<FishHitBox>();

        if (m_hitBoxes != null)
        {
            //add them to our InspectorDictionary.
            foreach (FishHitBox hitBox in m_hitBoxes)
            {
                m_hitBoxModifiers.Add(hitBox.m_HitBoxModifier.Key, hitBox.m_HitBoxModifier.Value);
            }
        }*/
    }
}
