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
    private PlanetCharacter m_planetLeader;
    public PlanetCharacter planetLeader { get { return m_planetLeader; } }

    [SerializeField]
    private PlanetCharacter[] m_guiltyCharacters;
    public PlanetCharacter[] guiltyCharacters { get { return m_guiltyCharacters; } }

    [SerializeField]
    private PlanetCharacter[] m_planetCharacters;
    public PlanetCharacter[] planetCharacters { get { return m_planetCharacters; } }

    [SerializeField]
    private PlanetDoodad[] m_planetDoodads;
    public PlanetDoodad[] planetDoodads { get { return m_planetDoodads; } }

}
