using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFish : AHealthUser
{
    protected InspectorDictionary<Collider, float> m_hitBoxModifiers;

    public float Speed;
    public float TurnSpeed;


    //behaviour
    protected Task m_behaviour;

    // Start is called before the first frame update
    void Start()
    {
        //Get all hitboxes from each fish compound colliders.
        FishHitBox[] m_hitBoxes = GetComponentsInChildren<FishHitBox>();

        if (m_hitBoxes != null)
        {
            //add them to our InspectorDictionary.
            foreach (FishHitBox hitBox in m_hitBoxes)
            {
                m_hitBoxModifiers.Add(hitBox.m_HitBoxModifier.Key, hitBox.m_HitBoxModifier.Value);
            }
        }
    }
}
