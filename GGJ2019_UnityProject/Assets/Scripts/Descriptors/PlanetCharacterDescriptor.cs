using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlanetCharacterDescriptor : PlanetEntity
{
    [SerializeField]
    private string m_characterName;
    public string characterName { get { return m_characterName; } }
    
    [SerializeField]
    private string m_speech;
    public string speech { get { return m_speech; } }

    [SerializeField]
    private bool m_isGuilty;
    public bool isGuilty { get { return m_isGuilty; } }

    [SerializeField]
    private AudioClip m_characterSound;
    public AudioClip characterSound { get { return m_characterSound; } }

}
