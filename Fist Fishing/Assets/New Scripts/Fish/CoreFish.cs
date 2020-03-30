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

        /**********************************************************************************************/
        /**********************************NEW FISH STUFF NEEDED HERE**********************************/
        /**********************************************************************************************/
        //m_Instance = new FishInstance(fishDefinition);
        m_defintion = fishDefinition;
        
    }

    public event CleanupCall Death
    {
        add { }
        remove { }
    }
}
