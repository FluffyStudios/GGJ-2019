using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluffyTools;

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
    [SerializeField] private float m_planetPosY = -9f;
    [SerializeField] private PlanetCharacter m_characterPrefab;    
    public PlanetCharacter characterPrefab { get { return m_characterPrefab; } }
    [SerializeField] private PlanetSceneryElement m_doodadPrefab;
    public PlanetSceneryElement doodadPrefab { get { return m_doodadPrefab; } }
    [SerializeField] private PlanetDescriptor[] m_levels;
    [SerializeField] private float m_rotationSpeed = 5f;
    [SerializeField] private float m_swipDistMultiplier = 5f;
    [SerializeField] private float m_maxSwipDist = 5f;
    [SerializeField] private float m_minDetectionSwipDist = 2f;
    [SerializeField] private float m_rotationBrakeSmoothing = 0.2f;
    [SerializeField] private float m_planetTransitionAnimDuration = 2f;

    public delegate void SelectCharAction(PlanetCharacter target, LevelState state);
    public static event SelectCharAction OnCharSelectedEvent;
    public static event SelectCharAction OnCharUnSelectedEvent;

    private LevelState m_currentState;
    private int m_currentlevel;
    private Planet m_currentPlanet;
    public Planet currentPlanet { get { return m_currentPlanet; } }
    private Planet m_previousPlanet;
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
        m_currentState = LevelState.Starting;
        m_accusedCharacters = new HashSet<PlanetCharacter>();
    }


    public void StartMissionAnim()
    {
        InGameScreen inGameScreen = Gui.GuiService.GetWindow<InGameScreen>();
        inGameScreen.CleanAllBubbles();
        m_currentState = LevelState.Starting;
        m_isRotating = false;
        m_isSliding = false;
        Cx.Routine NewPlanetArrival = Cx.Sequence(

        inGameScreen.missionTitle.TitleAnimation(m_currentlevel, m_levels[m_currentlevel].caseName, m_levels[m_currentlevel].planetName),
        Cx.Call(() => {
            if (m_currentPlanet.planetLeader != null)
            {
                if (OnCharSelectedEvent != null)
                {
                    OnCharSelectedEvent(m_currentPlanet.planetLeader, m_currentState);
                }
            }
            m_currentPlanet.planetLeader.PlaySelectionSound();
            m_activatedCharacter = m_currentPlanet.planetLeader;
            m_currentState = LevelState.Investigating;
            inGameScreen.accuseBtn.gameObject.SetActive(true);
        }));

        if (m_previousPlanet == null)
        {            
            NewPlanetArrival.Start(this);
        }
        else
        {
            Cx.Sequence(
                   Cx.Parallel(
                       Cx.Call(() =>
                       {
                           Game.CameraService.Zoom(false);
                       }),
                       Cx.MoveTo(m_previousPlanet.gameObject, new Vector2(-50f, m_planetPosY), m_planetTransitionAnimDuration, false, Easing.EaseType.Linear),
                       Cx.MoveTo(m_currentPlanet.gameObject, new Vector2(0f, m_planetPosY), m_planetTransitionAnimDuration, false, Easing.EaseType.Linear),
                       Cx.CallLater(m_planetTransitionAnimDuration - 1f, () =>
                       {
                           Game.CameraService.Zoom(true);
                       })
                    ),
                   Cx.Call(() => {
                       Destroy(m_previousPlanet.gameObject);
                       // lancer affichage du nom de mission
                   }),
                   NewPlanetArrival

            ).Start(this);
        }
    }

    private void GeneratePlanet(PlanetDescriptor planetDescriptor)
    {
        m_currentState = LevelState.Starting;
        m_isRotating = false;
        m_isSliding = false;
        if (m_currentPlanet != null)
        {
            m_previousPlanet = m_currentPlanet;
        }
        m_currentPlanet = Instantiate(m_planetPrefab, transform) as Planet;
        m_currentPlanet.Generate(planetDescriptor);

        if (m_previousPlanet == null)
        {
            m_currentPlanet.transform.position = new Vector2(0f, m_planetPosY);

        }
        else
        {
            m_currentPlanet.transform.position = new Vector2(50f, m_planetPosY);
        }

        m_currentPlanet.PlayMusic();
    }

    private void Update()
    {
        if (Game.CameraService.CurrentState != CameraManager.CameraState.InGame || m_currentState == LevelState.Starting || m_currentState == LevelState.Ended)
        {
            return;
        }

        if (m_isRotating && m_currentPlanet != null)
        {
            this.PlanetRotation();
        }

        if (m_currentState != LevelState.Ended && m_currentPlanet != null)
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
                    PlanetSceneryElement objTouched = touchedObject.GetComponent<PlanetSceneryElement>();
                    if (objTouched != null)
                    {
                        ResolveDoodadTouch(objTouched);
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

    private void ResolveDoodadTouch(PlanetSceneryElement objTouched)
    {
        if (m_currentState == LevelState.Investigating)
        {
            objTouched.PlaySelectionSound();
        }
    }


    private void ResolveCharacterTouch(PlanetCharacter charTouched)
    {
        if (m_currentState == LevelState.Investigating)
        {
            if(m_activatedCharacter != null)
            {
                if(m_activatedCharacter == charTouched)
                {
                    if (OnCharUnSelectedEvent != null)
                    {
                        OnCharUnSelectedEvent(charTouched, m_currentState);
                    }
                    m_activatedCharacter = null;
                }
                else
                {
                    if (OnCharUnSelectedEvent != null)
                    {
                        OnCharUnSelectedEvent(m_activatedCharacter, m_currentState);
                    }
                    if (OnCharSelectedEvent != null)
                    {
                        OnCharSelectedEvent(charTouched, m_currentState);
                    }
                    charTouched.PlaySelectionSound();
                    m_activatedCharacter = charTouched;
                }
            }
            else
            {
                if (OnCharSelectedEvent != null)
                {
                    OnCharSelectedEvent(charTouched, m_currentState);
                }
                charTouched.PlaySelectionSound();
                m_activatedCharacter = charTouched;
            }      
        }
        else if (m_currentState == LevelState.Accusing)
        {
            if (m_accusedCharacters.Contains(charTouched))
            {
                if (OnCharUnSelectedEvent != null)
                {
                    OnCharUnSelectedEvent(charTouched, m_currentState);
                }
                m_accusedCharacters.Remove(charTouched);
            }
            else
            {
                if (OnCharSelectedEvent != null)
                {
                    OnCharSelectedEvent(charTouched, m_currentState);
                }
                charTouched.PlayAccusedSound();
                m_accusedCharacters.Add(charTouched);                
            }
            
            InGameScreen inGameScreen = Gui.GuiService.GetWindow<InGameScreen>();
            inGameScreen.SetValidateButtonState(m_accusedCharacters.Count > 0);
        }
        else
            return;      
    }

    private void Failed()///// echec de l'accusation
    {
        InGameScreen inGameScreen = Gui.GuiService.GetWindow<InGameScreen>();
        inGameScreen.OnAccuseCanceledCb();
    }

    private void Win()
    {
        if (m_currentlevel < m_levels.Length - 1)
            m_currentlevel += 1;
        this.GeneratePlanet(m_levels[m_currentlevel]);
        StartMissionAnim();
    }

    public void ResolveAccusation()
    {
        bool asFailed = false;
        if(m_accusedCharacters.Count != m_currentPlanet.guiltyCount)
        {
            asFailed = true;
        }

        foreach(PlanetCharacter accused in m_accusedCharacters)
        {
            if (!accused.GetCharacterDescriptor().isGuilty)
            {
                asFailed = true;
            }
        }

        InGameScreen inGameScreen = Gui.GuiService.GetWindow<InGameScreen>();
        Cx.Sequence(
            inGameScreen.accusationPopup.ValidateAccusationPopupAnim(!asFailed),
            Cx.Call(() =>
            {
                if (asFailed)
                    Failed();
                else
                    Win();

            })).Start(this);
    }

    public void CancelAccusation()
    {
        m_currentState = LevelState.Investigating;
        m_accusedCharacters.Clear();
    }

    public void StartAccusation()
    {
        if (m_activatedCharacter != null)
        {
            if (OnCharUnSelectedEvent != null)
            {
                OnCharUnSelectedEvent(m_activatedCharacter, m_currentState);
            }
        }
        m_currentState = LevelState.Accusing;
        
    }

    private void PlanetRotation()
    {
        m_currentPlanet.transform.Rotate(Vector3.forward * m_rotationStrenght * Time.fixedDeltaTime * m_direction, Space.World);
        m_rotationStrenght -= m_rotationBrakeSmoothing;
        if (m_rotationStrenght <= 0)
            m_isRotating = false;
    }

    public void MufflePlanetMusic(float duration)
    {
        m_currentPlanet.AudioSource.volume = 0.1f;
        Cx.CallLater(duration, () =>
        {
            m_currentPlanet.AudioSource.volume = FluffyBox.Application.Instance.MusicVolume;
        }).Start(this);
    }
}
