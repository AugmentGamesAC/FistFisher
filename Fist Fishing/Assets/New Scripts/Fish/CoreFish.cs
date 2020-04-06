using UnityEngine;
public class CoreFish: MonoBehaviour, IDyingThing
{
    [SerializeField]
    protected FishDefintion m_defintion;
    public FishDefintion Defintion => m_defintion;

    protected MeshCollider m_biome;
    protected SkinnedMeshRenderer mesh;

    protected FishInstance m_Instance;

    public void Init(FishDefintion fishDefinition, MeshCollider biome)
    {

        /**********************************************************************************************/
        /**********************************NEW FISH STUFF NEEDED HERE**********************************/
        /**********************************************************************************************/
        //m_Instance = new FishInstance(fishDefinition);
        m_biome = biome;

        Prompt prompt = GetComponent<CombatPrompt>();
        if (prompt == default)
            prompt = gameObject.AddComponent<CombatPrompt>();
        prompt.Init(fishDefinition.IconDisplay, "P to Fight {0} Fish!", 1);

        m_Instance = new FishInstance(fishDefinition, prompt);
        m_defintion = fishDefinition;

        mesh = GetComponent<SkinnedMeshRenderer>();
        if ((mesh == default) || (mesh.sharedMesh != fishDefinition.SkinedMesh.sharedMesh))
        {
            if (mesh = default)
                mesh = gameObject.AddComponent<SkinnedMeshRenderer>();
            mesh.sharedMesh = fishDefinition.SkinedMesh.sharedMesh;
            mesh.bones = fishDefinition.SkinedMesh.bones;
            mesh.rootBone = fishDefinition.SkinedMesh.rootBone;
            mesh.material = fishDefinition.Skin;
        }
    }

    public event Death Death
    {
        add { m_Instance.Health.OnMinimumHealthReached += value; }
        remove { m_Instance.Health.OnMinimumHealthReached -= value; }
    }
}
