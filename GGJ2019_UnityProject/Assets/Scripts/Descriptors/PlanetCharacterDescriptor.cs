using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlanetCharacterDescriptor : PlanetEntityDescriptor
{
    [SerializeField]
    private string m_characterName;
    public string characterName { get { return m_characterName; } }

    [Header("Speech variables")]
    [SerializeField]
    private bool m_reverse;
    public bool reverse { get { return m_reverse; } }

    [SerializeField]
    private string m_speech;
    public string speech { get
        {
            if (m_reverse)
            {
                Char[] charArray = m_speech.ToCharArray();
                Array.Reverse(charArray);
                return new string(charArray);
            }
            return m_speech;
        }
    }

    [SerializeField]
    private string m_accusedSpeech;
    public string accusedSpeech { get { return m_accusedSpeech; } }

    [SerializeField]
    private bool m_isGuilty;
    public bool isGuilty { get { return m_isGuilty; } }
  
    [SerializeField]
    private AudioClip m_accusedSound;
    public AudioClip accusedSound { get { return m_accusedSound; } }

    [SerializeField]
    private HiddenSpeechParams m_secretSpeech;
    public HiddenSpeechParams secretSpeech { get { return m_secretSpeech; } }
}
