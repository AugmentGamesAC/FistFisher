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

    [MenuItem("GameObject/Prefabs/Prefab1", priority = -1)]
    static void CreatePrefab1()
    {
        Instantiate(Instance.m_gameObjects[0], Selection.activeGameObject.transform);
    }


    // Disable the menu item if no selection is in place.
    [MenuItem("GameObject/Prefabs/Prefab1", true, priority = -1)]
    static bool ValidateCreatePrefab()
    {
        return Selection.activeGameObject != null && !EditorUtility.IsPersistent(Selection.activeGameObject);
    }
}


