using UnityEditor;
using UnityEngine;

public class CreateVolumeSecret
{
    [MenuItem("Assets/Inspector Planet/Secret Speech / Volume secret")]
    public static AudioVolumeSecret Create()
    {
        AudioVolumeSecret asset = ScriptableObject.CreateInstance<AudioVolumeSecret>();
        AssetDatabase.CreateAsset(asset, "Assets/Scriptables/SecretSpeech/NewVolumeSecret.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}