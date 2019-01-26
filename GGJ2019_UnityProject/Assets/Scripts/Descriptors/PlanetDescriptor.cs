using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetDescriptor : ScriptableObject
{

    [SerializeField]
    private string m_caseName;
    public string caseName { get { return m_caseName; } }

    [SerializeField]
    private string m_planetName;
    public string planetName { get { return m_planetName; } }

    [SerializeField]
    private Sprite m_planetTexture;
    public Sprite planetTexture { get { return m_planetTexture; } }
        
    [SerializeField]
    private AudioClip m_planetMusic;
    public AudioClip planetMusic { get { return m_planetMusic; } }

    [SerializeField]
    private PlanetCharacterDescriptor m_planetLeader;
    public PlanetCharacterDescriptor planetLeader { get { return m_planetLeader; } }
    
    [SerializeField]
    private PlanetCharacterDescriptor[] m_planetCharacters;
    public PlanetCharacterDescriptor[] planetCharacters { get { return m_planetCharacters; } }

    [SerializeField]
    private PlanetDoodadDescriptor[] m_planetDoodads;
    public PlanetDoodadDescriptor[] planetDoodads { get { return m_planetDoodads; } }

}
