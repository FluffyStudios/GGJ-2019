using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class HiddenSpeechParams : ScriptableObject
{
    [SerializeField]
    private string m_secretSpeechString;
    public string secretSpeechString { get { return m_secretSpeechString; } }
}

