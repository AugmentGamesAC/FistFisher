﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// when combat was going to be in-world, this handled the players shoulders and zones, 
/// and handles what happens when fish is targetted and a punch is thrown
/// </summary>
public class CombatModule : MonoBehaviour
{
    //(float Range, float ModifierPercent).
    List<CombatZone> m_combatZones;

    public float m_punchDamage = 30.0f;

    public Shoulder m_rightShoulder;
    public Shoulder m_leftShoulder;

    public Shoulder m_currentShoulder;

    public CombatZone m_currentCombatZone;

    private GameObject m_targetedFish;

    public TargetController m_targetController;

    // Start is called before the first frame update
    void Start()
    {

        m_influenceFish = GetComponent<InfluenceFish>();
    }

    // Update is called once per frame
    void Update()
    {
        //get targetedfish from targeting system.
        m_targetedFish = null;

        if (m_targetedFish == null)
            return;

        //Set current closest shoulder to the target.
        SetCurrentShoulder();

        //Get the zone the fish is in, to apply damage modifier.
        m_currentCombatZone = m_currentShoulder.GetCombatZone(m_targetedFish);

        //don't run Punch() if we don't have a fish in range or if the targeting is off.
        if (m_currentCombatZone == null || m_targetController.m_targetingIsActive == false)
            return;

        //needs to be replaced with brian's method of input.
        if (ALInput.GetKeyDown(ALInput.AltAction))
            Punch();
    }

    //sets shoulder being used by the player depending on distance.
    void SetCurrentShoulder()
    {
        Vector3 fishPos = m_targetedFish.transform.position;

        float RightShoulderDist = Vector3.Distance(m_rightShoulder.transform.position, fishPos);
        float LeftShoulderDist = Vector3.Distance(m_leftShoulder.transform.position, fishPos);

        if (RightShoulderDist > LeftShoulderDist)
        {
            m_currentShoulder = m_leftShoulder;
        }
        else if (RightShoulderDist < LeftShoulderDist)
        {
            m_currentShoulder = m_rightShoulder;
        }
    }

    protected InfluenceFish m_influenceFish;

    //apply damage to targeted fish health component based on zone modifiers and m_damage.
    private void Punch()
    {
        HealthModule tempHealthModule = m_targetedFish.GetComponent<HealthModule>();


        if (m_influenceFish != null)
            m_influenceFish.ImpressFish(m_targetedFish,1000);

        if (tempHealthModule == null)
            return;

        tempHealthModule.TakeDamage(m_currentCombatZone.DamageModifier * m_punchDamage);

        m_targetController.m_targetPrefab.GetComponent<HitEffects>().Hit();
    }
}
