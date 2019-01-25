using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlanetCharacter : PlanetEntity
{
    [SerializeField]
    private string m_characterName;
    public string characterName { get { return m_characterName; } }

    [SerializeField]
    private string m_speech;
    public string speech { get { return m_speech; } }
    
}
