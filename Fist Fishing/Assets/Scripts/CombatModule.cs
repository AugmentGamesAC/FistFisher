using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatModule : MonoBehaviour
{
    //(float Range, float ModifierPercent).
    List<CombatZone> m_combatZones;

    public Shoulder m_rightShoulder;
    public Shoulder m_leftShoulder;

    public Shoulder m_currentShoulder;

    private GameObject m_targetedFish;

    public TargetController m_targetController;

    // Start is called before the first frame update
    void Start()
    {
        m_smallZone.DamageModifier = 1.0f;
        m_smallZone.Range = 5.0f;

        m_mediumZone.DamageModifier = 0.8f;
        m_mediumZone.Range = 10.0f;

        m_largeZone.DamageModifier = 0.5f;
        m_largeZone.Range = 15.0f;


        //get target Controller from player.
        m_targetController = GetComponentInParent<TargetController>();

        //get targeted fish.
        m_targetedFish = m_targetController.m_targetedFish;
    }

    // Update is called once per frame
    void Update()
    {
        m_targetedFish = m_targetController.m_targetedFish;


    }

    private void Punch()
    {
        //apply damage to targeted fish health component based on zone modifiers and m_damage.
    }

    
}
