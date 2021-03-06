﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// this exists as an example to show that things can be killed and eventually removed from biome instance counter
/// </summary>
public class DyingRock : MonoBehaviour, IDyingThing
{
    public event Death Death;

    [ContextMenu("From hells heart I stab at thee")]
    public void GoDie()
    {
        this.gameObject.SetActive(false);
        Death?.Invoke();
    }
}
