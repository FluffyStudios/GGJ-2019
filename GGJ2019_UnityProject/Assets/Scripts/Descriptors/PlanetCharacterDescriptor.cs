using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlanetCharacterDescriptor : PlanetEntityDescriptor
{
    [SerializeField]
    private string m_characterName;
    public string characterName { get { return m_characterName; } }
    
    [SerializeField]
    private string m_speech;
    public string speech { get { return m_speech; } }

    [SerializeField]
    private string m_accusedSpeech;
    public string accusedSpeech { get { return m_accusedSpeech; } }

    [SerializeField]
    private bool m_isGuilty;
    public bool isGuilty { get { return m_isGuilty; } }

    
    [SerializeField]
    private AudioClip m_accusedSound;
    public AudioClip accusedSound { get { return m_accusedSound; } }

}
