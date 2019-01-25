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
    private Texture2D m_planetTexture;
    public Texture2D planetTexture { get { return m_planetTexture; } }

    [SerializeField]
    private PlanetCharacterDescriptor m_planetLeader;
    public PlanetCharacterDescriptor planetLeader { get { return m_planetLeader; } }
    
    [SerializeField]
    private PlanetCharacterDescriptor[] m_planetCharacters;
    public PlanetCharacterDescriptor[] planetCharacters { get { return m_planetCharacters; } }

    [SerializeField]
    private PlanetCharacterDescriptor[] m_planetDoodads;
    public PlanetCharacterDescriptor[] planetDoodads { get { return m_planetDoodads; } }

}
