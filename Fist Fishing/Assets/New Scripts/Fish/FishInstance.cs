﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// holds all core gameplay data for fish other than Combat Information.
/// </summary>
[System.Serializable]
public class FishInstance
{
    [SerializeField]
    protected FishDefintion m_fishData;
    public IFishData FishData => m_fishData;

    [SerializeField]
    protected FishHealth m_health;
    public FishHealth Health => m_health;


    protected Prompt m_fishPrompt;
    public Prompt FishPrompt => m_fishPrompt;

    public FishInstance(FishDefintion fishDef, Prompt prompt)
    {
        m_fishData = fishDef;
        m_health = new FishHealth(m_fishData.MaxHealth);
        m_fishPrompt = prompt;
    }
}
