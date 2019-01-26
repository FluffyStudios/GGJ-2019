using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public static PlanetManager Instance;
    [SerializeField] private Planet m_planetPrefab;
    [SerializeField] private PlanetCharacter m_characterPrefab;
    public PlanetCharacter characterPrefab { get { return m_characterPrefab; } }
    [SerializeField] private PlanetDescriptor[] m_levels;
    private int m_currentlevel;
    private Planet m_currentPlanet;

    void Awake()
    {
        if (Instance != null)
        {
            GameObject.Destroy(Instance);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        m_currentlevel = 0;
    }

    private void Start()
    {
        GeneratePlanet(m_levels[m_currentlevel]);
    }

    private void GeneratePlanet(PlanetDescriptor planetDescriptor)
    {
        if(m_currentPlanet != null)
        {
            Destroy(m_currentPlanet.gameObject);
        }

        m_currentPlanet = Instantiate(m_planetPrefab) as Planet;
        m_currentPlanet.Generate(planetDescriptor);
    }

}
