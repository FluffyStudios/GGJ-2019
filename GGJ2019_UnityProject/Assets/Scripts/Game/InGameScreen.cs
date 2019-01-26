using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FluffyTools;

public class InGameScreen : FluffyBox.GuiScreen
{
    public static InGameScreen Instance;
    [SerializeField] private Button m_accuseBtn;
    [SerializeField] private Button m_cancelAccuseBtn;
    [SerializeField] private Button m_validateAccuseBtn;
    [SerializeField] private GameObject m_accusationFilter;
    [SerializeField] private SpeechBubble m_speechBubblePrefab;
    [SerializeField] private Transform m_speechBubblecontent;
    [SerializeField] private MissionTitle m_missionTitle;
    public MissionTitle missionTitle { get { return m_missionTitle; } }
    private Dictionary<PlanetCharacter, SpeechBubble> m_charactersToSpeeches;

    void Awake()
    {
        if (Instance != null)
            Destroy(Instance);
        else
            Instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        m_charactersToSpeeches = new Dictionary<PlanetCharacter, SpeechBubble>();
        m_cancelAccuseBtn.gameObject.SetActive(false);
        m_validateAccuseBtn.gameObject.SetActive(false);
        m_accusationFilter.SetActive(false);
        PlanetManager.OnCharSelectedEvent += OnCharSelected;
        PlanetManager.OnCharUnSelectedEvent += OnCharUnSelected;
    }

    public void OnAccuseBtnClick()
    {
        /*Cx.Sequence(
               Cx.Call(() =>
               {
                   m_mainPopupMessageText.text = "ACCUSATION TIME !";
                   m_mainPopupMessageText.gameObject.SetActive(true);
               }),
               Cx.Delay(0.01f),
               Cx.Call(() => {
                
                })
                ).Start(this);*/
        m_cancelAccuseBtn.gameObject.SetActive(true);
        m_validateAccuseBtn.gameObject.SetActive(true);
        m_accuseBtn.gameObject.SetActive(false);
        m_accusationFilter.SetActive(true);
        PlanetManager.Instance.StartAccusation();

    }
    public void OnAccuseCanceled()
    {
        m_cancelAccuseBtn.gameObject.SetActive(false);
        m_validateAccuseBtn.gameObject.SetActive(false);
        m_accuseBtn.gameObject.SetActive(true);
        m_accusationFilter.SetActive(false);
        PlanetManager.Instance.CancelAccusation();

        foreach(KeyValuePair< PlanetCharacter, SpeechBubble > kv in m_charactersToSpeeches)
        {           
            Destroy(kv.Value.gameObject);
        }
        m_charactersToSpeeches.Clear();
    }

    public void OnAccuseValidated()
    {
        m_cancelAccuseBtn.gameObject.SetActive(false);
        m_validateAccuseBtn.gameObject.SetActive(false);
        m_accuseBtn.gameObject.SetActive(false);
        m_accusationFilter.SetActive(false);
        PlanetManager.Instance.ResolveAccusation();
    }

    private void OnCharSelected(PlanetCharacter character, LevelState state)
    {
        if (Game.CameraService.CurrentState != CameraManager.CameraState.InGame)
        {
            return;
        }

        SpeechBubble currentBubble;
        if(m_charactersToSpeeches.TryGetValue(character, out currentBubble))
        {
            currentBubble.ActivateBubble(character, state);
        }
        else
        {
            currentBubble = Instantiate(m_speechBubblePrefab, m_speechBubblecontent);
            currentBubble.gameObject.SetActive(false);
            currentBubble.ActivateBubble(character, state);
            m_charactersToSpeeches.Add(character, currentBubble);
        }
        // s'il n'a pas déjà de speech enclenchée, créé la buble speech et l'ajouter au dico
    }

    private void OnCharUnSelected(PlanetCharacter character, LevelState state)
    {
        Debug.Log("personnage déséléctionné");
        SpeechBubble currentBubble;
        if (m_charactersToSpeeches.TryGetValue(character, out currentBubble))
        {
            m_charactersToSpeeches.Remove(character);
            Destroy(currentBubble.gameObject);
        }
        // s'il est dans le dico, supprimer la speech et l'enlever du dico
    }
}
