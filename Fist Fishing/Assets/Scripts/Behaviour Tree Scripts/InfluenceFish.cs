using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluenceFish : MonoBehaviour
{
    [SerializeField]
    protected FishBrain.FishClassification m_classification = FishBrain.FishClassification.BaitSensitive1;
    [SerializeField]
    protected float m_influence = 0.1f;
    public void ImpressFish(GameObject other, float Influencefactor = 1)
    {
        FishBrain fb = other.GetComponentInParent<FishBrain>();
        if (fb == default)
            return;

        fb.ApplyImpulse(gameObject, m_influence, m_classification);
    }
}
