using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFish2 : BasicFish, IDyingThing
{
    protected MeshCollider m_biome;
    public MeshCollider Home => m_biome;
    public FishDefintion Defintion => m_fishDef;

    private void HandleDeath()
    {
        //ObjectPool should Handle fish.
        gameObject.SetActive(false);
        //TODO: Fix prompt disapearing during combat.
    }

    void Start()
    {
        Prompt prompt = gameObject.AddComponent<CombatPrompt>();
        prompt.Init(m_fishDef.IconDisplay, "P to Fight {0} Fish!", 1);

        m_fish = new FishInstance(m_fishDef, prompt);
        m_fish.Health.OnMinimumHealthReached += HandleDeath;
        //TODO: clean
        m_behaviour = GetComponent<BehaviorTree>();
    }

    public event Death Death
    {
        add { m_fish.Health.OnMinimumHealthReached += value; }
        remove { m_fish.Health.OnMinimumHealthReached -= value; }
    }

    public void Init(FishDefintion fishDefinition, MeshCollider biome)
    {
        m_biome = biome;
        m_fishDef = fishDefinition;
    }

    public new FishBrain.FishClassification FishClass { get { return m_fishDef.FishClassification; } }
}
