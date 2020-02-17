using UnityEngine;
using UnityEditor;

public class FishDefinitionFactory : FishDefintion
{
    [MenuItem("Assets/QuickCreate/Aggressive Fish")]
    public static void CreateAgressive()
    {
        FishDefintion asset = ScriptableObject.CreateInstance<FishDefintion>();
        asset.ConfigFish(FishBrain.FishClassification.Agressive | FishBrain.FishClassification.BaitSensitive1);

        AssetDatabase.CreateAsset(asset,
            AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/Aggresive.asset")
            );
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
    [MenuItem("Assets/QuickCreate/Fearful Fish")]
    public static void CreateFearful()
    {
        FishDefintion asset = ScriptableObject.CreateInstance<FishDefintion>();
        asset.ConfigFish(FishBrain.FishClassification.Fearful | FishBrain.FishClassification.BaitSensitive1);

        AssetDatabase.CreateAsset(asset,
            AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/Fearful.asset")
            );
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
    [MenuItem("Assets/QuickCreate/Passive Fish")]
    public static void CreatePassive()
    {
        FishDefintion asset = ScriptableObject.CreateInstance<FishDefintion>();
        asset.ConfigFish(FishBrain.FishClassification.Passive | FishBrain.FishClassification.BaitSensitive1);

        AssetDatabase.CreateAsset(asset,
            AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/Passive.asset")
            );
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

}

