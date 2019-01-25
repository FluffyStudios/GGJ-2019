using UnityEditor;
using UnityEngine;

public class CreatePlanetDescriptor
{
    [MenuItem("Assets/Inspector Planet/New Planet")]
    public static PlanetDescriptor Create()
    {
        PlanetDescriptor asset = ScriptableObject.CreateInstance<PlanetDescriptor>();
        AssetDatabase.CreateAsset(asset, "Assets/Scriptables/Planets/NewPlanet.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}

