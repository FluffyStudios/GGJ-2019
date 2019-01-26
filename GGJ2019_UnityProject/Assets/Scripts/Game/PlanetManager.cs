﻿using System.Collections;
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
    [SerializeField] float m_rotationSpeed = 5f;
    [SerializeField] float m_swipDistMultiplier = 5f;
    [SerializeField] float m_maxSwipDist = 5f;
    [SerializeField] float m_minDetectionSwipDist = 2f;
    [SerializeField] float m_rotationBrakeSmoothing = 0.2f;

    private int m_currentlevel;
    private Planet m_currentPlanet;
    private LevelState m_currentState;
    private PlanetCharacter m_activatedCharacter;
    private Vector2 m_startTouch;
    private float m_rotationStrenght;//Specify Angle For Rotation
    private bool m_isRotating;//Check Whether Currently Object is Rotating Or Not.
    private bool m_isSliding;
    private int m_direction;//Direction Of Rotation
    private HashSet<PlanetCharacter> m_accusedCharacters;


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
        m_accusedCharacters = new HashSet<PlanetCharacter>();
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
        if (m_isRotating && m_currentPlanet != null)
            PlanetRotation();

        if (m_currentState != LevelState.Ended && m_currentPlanet!=null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 touchPosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
                RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
                if (hitInformation.collider != null)
                {
                    GameObject touchedObject = hitInformation.transform.gameObject;
                    PlanetCharacter characterTouched = touchedObject.GetComponent<PlanetCharacter>();
                    if (characterTouched != null)
                    {
                        ResolveCharacterTouch(characterTouched);                      
                    }
                }
                
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

        
    }
    private void ResolveCharacterTouch(PlanetCharacter charTouched)
    {
        if(m_currentState == LevelState.Investigating)
        {
            // désafficher la replique du précédent character activé
            m_activatedCharacter = charTouched;
            // activer la replique du perso
        }
        else if(m_currentState == LevelState.Accusing)
        {
            m_accusedCharacters.Add(charTouched);
        }
    }

    private void Failed()///// echec de l'accusation
    {
        
        Debug.Log("RAté idiiiot!");
    }

    private void Win()
    {
        Debug.Log("Bravo le veau!");
    }

    public void ResolveAccusation()
    {
        if(m_accusedCharacters.Count != m_currentPlanet.guiltyCount)
        {
            Failed();
            return;
        }
        foreach(PlanetCharacter accused in m_accusedCharacters)
        {
            if (!accused.descriptor.isGuilty)
            {
                Failed();         
                return;
            }
        }      
    }

    public void CancelAccusation()
    {
        m_currentState = LevelState.Investigating;
        m_accusedCharacters.Clear();
    }

    public void StartAccusation()
    {
        m_currentState = LevelState.Accusing;
    }


    private void PlanetRotation()
    {
        m_currentPlanet.transform.Rotate(Vector3.forward * m_rotationStrenght * Time.fixedDeltaTime * m_direction, Space.World);
        m_rotationStrenght -= m_rotationBrakeSmoothing;
        if (m_rotationStrenght <= 0)
            m_isRotating = false;
    }

}
