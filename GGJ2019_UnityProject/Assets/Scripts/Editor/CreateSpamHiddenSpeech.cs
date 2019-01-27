using UnityEditor;
using UnityEngine;

public class CreateSpamHiddenSpeech
{
    [MenuItem("Assets/Inspector Planet/Secret Speech / Spam secret")]
    public static SpamRevealedSecret Create()
    {
        SpamRevealedSecret asset = ScriptableObject.CreateInstance<SpamRevealedSecret>();
        AssetDatabase.CreateAsset(asset, "Assets/Scriptables/SecretSpeech/NewSecret.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}

