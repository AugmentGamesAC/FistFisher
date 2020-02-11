using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "PrefabFactoryInstance", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class PrefabFactory : ScriptableObject
{
    [SerializeField]
    protected List<GameObject> m_gameObjects = new List<GameObject>();

    protected static PrefabFactory Instance;

    private void OnEnable()
    {
        if (Instance != default)
            return;
        Instance = this;
    }

    static void CreatePrefab(int index)
    {
        GameObject something = PrefabUtility.InstantiatePrefab(Instance.m_gameObjects[index], Selection.activeGameObject.transform) as GameObject;
        Selection.activeTransform = something.transform;
        PrefabUtility.UnpackPrefabInstance(
            something,
            PrefabUnpackMode.OutermostRoot,
            InteractionMode.AutomatedAction);
    }

    [MenuItem("GameObject/UIUpdaters/Image (Pie)", priority = -1)]
    public static void C0() { CreatePrefab(0); }
    [MenuItem("GameObject/UIUpdaters/Image (Bar)", priority = -1)]
    public static void C1() { CreatePrefab(1); }
    [MenuItem("GameObject/UIUpdaters/Image (Int)", priority = -1)]
    public static void C2() { CreatePrefab(2); }
    [MenuItem("GameObject/UIUpdaters/Image (Img)", priority = -1)]
    public static void C3() { CreatePrefab(3); }
    [MenuItem("GameObject/UIUpdaters/Text Percent", priority = -1)]
    public static void C4() { CreatePrefab(4); }
    [MenuItem("GameObject/UIUpdaters/Text Float  ", priority = -1)]
    public static void C5() { CreatePrefab(5); }
    [MenuItem("GameObject/UIUpdaters/Text String ", priority = -1)]
    public static void C6() { CreatePrefab(6); }


    [MenuItem("GameObject/UIUpdaters/Image (Pie)", true, priority = -1)]
    [MenuItem("GameObject/UIUpdaters/Image (Bar)", true, priority = -1)]
    [MenuItem("GameObject/UIUpdaters/Image (Int)", true, priority = -1)]
    [MenuItem("GameObject/UIUpdaters/Image (Img)", true, priority = -1)]
    [MenuItem("GameObject/UIUpdaters/Text Percent", true, priority = -1)]
    [MenuItem("GameObject/UIUpdaters/Text Float  ", true, priority = -1)]
    [MenuItem("GameObject/UIUpdaters/Text String ", true, priority = -1)]
    public static bool ValidateCreatePrefab()
    {
        return Selection.activeGameObject != null && !EditorUtility.IsPersistent(Selection.activeGameObject);
    }

}


