using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelState : byte
{
    Starting,
    Investigating,
    Accusing,
    Ended
}

public class PlanetManager : MonoBehaviour
{
    public static PlanetManager Instance;
    [SerializeField] private Planet m_planetPrefab;
    [SerializeField] private PlanetCharacter m_characterPrefab;
    public PlanetCharacter characterPrefab { get { return m_characterPrefab; } }
    [SerializeField] private PlanetDescriptor[] m_levels;
    private int m_currentlevel;
    private Planet m_currentPlanet;
    private LevelState m_currentState;

    private Vector2 m_startTouch;
    private float m_rotationStrenght;//Specify Angle For Rotation
    private bool m_isRotating;//Check Whether Currently Object is Rotating Or Not.
    private bool m_isSliding;
    private int m_direction;//Direction Of Rotation
    [SerializeField] float m_rotationSpeed = 5f;
    [SerializeField] float m_swipDistMultiplier = 5f;
    [SerializeField] float m_maxSwipDist = 5f;
    [SerializeField] float m_minDetectionSwipDist = 2f;
    [SerializeField] float m_rotationBrakeSmoothing = 0.2f;

    void Awake()
    {
        if (Instance != null)
        {
            GameObject.Destroy(Instance);
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(this);
        }

        m_currentlevel = 0;
    }

    private void Start()
    {
        GeneratePlanet(m_levels[m_currentlevel]);
        m_currentState = LevelState.Investigating;
    }

    private void GeneratePlanet(PlanetDescriptor planetDescriptor)
    {
        if(m_currentPlanet != null)
        {
            Destroy(m_currentPlanet.gameObject);
        }

        m_currentPlanet = Instantiate(m_planetPrefab, transform) as Planet;
        m_currentPlanet.transform.position = new Vector3(0f, -6.2f, 0f);
        m_currentPlanet.Generate(planetDescriptor);
    }

    private void Update()
    {
        if(m_currentState != LevelState.Ended && m_currentPlanet!=null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_startTouch = Input.mousePosition;
                m_isSliding = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                m_isSliding = false;
            }

            if (m_isSliding)
            {
                Vector2 mousePos = Input.mousePosition;
                if (mousePos.x - m_startTouch.x < 0)
                    m_direction = 1;
                else
                    m_direction = -1;

                float swipDist = Mathf.Abs(mousePos.x - m_startTouch.x);
                m_rotationStrenght = m_rotationSpeed * Mathf.Min(m_maxSwipDist,(swipDist*m_swipDistMultiplier));

                m_isRotating = true;
            }
            
        }

        if(m_isRotating && m_currentPlanet!=null)
            PlanetRotation();
    }

    void PlanetRotation()
    {
        m_currentPlanet.transform.Rotate(Vector3.forward * m_rotationStrenght * Time.fixedDeltaTime * m_direction, Space.World);
        m_rotationStrenght -= m_rotationBrakeSmoothing;
        if (m_rotationStrenght <= 0)
            m_isRotating = false;
    }

}
