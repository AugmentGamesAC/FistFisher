using UnityEngine;
public class CoreFish: MonoBehaviour, IDyingThing
{
    [SerializeField]
    protected FishDefintion m_defintion;
    public FishDefintion Defintion => m_defintion;

    protected MeshCollider m_biome;
    protected SkinnedMeshRenderer mesh;

    protected FishInstance m_Instance;

    public void Init(FishDefintion fishDefinition, MeshCollider m_biome)
    {
        Prompt prompt = gameObject.AddComponent<CombatPrompt>();
        prompt.Init(fishDefinition.IconDisplay, "P to Fight {0} Fish!", 1);

        m_Instance = new FishInstance(fishDefinition, prompt);
        m_defintion = fishDefinition;
        
    }

    public event Death Death
    {
        add { m_Instance.Health.OnMinimumHealthReached += value; }
        remove { m_Instance.Health.OnMinimumHealthReached -= value; }
    }
}
