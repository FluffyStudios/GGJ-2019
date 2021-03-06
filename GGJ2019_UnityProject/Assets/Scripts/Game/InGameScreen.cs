﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FluffyTools;

public class InGameScreen : FluffyBox.GuiScreen
{
    [SerializeField] private Button m_accuseBtn;
    public Button accuseBtn { get { return m_accuseBtn; } }
    [SerializeField] private Button m_cancelAccuseBtn;
    [SerializeField] private Button m_validateAccuseBtn;
    [SerializeField] private Text m_validateAccuseBtnText;
    [SerializeField] private SpeechBubble m_speechBubblePrefab;
    [SerializeField] private Transform m_speechBubblecontent;
    [SerializeField] private MissionTitle m_missionTitle;
    public MissionTitle missionTitle { get { return m_missionTitle; } }
    [SerializeField] private AccusationPopup m_accusationPopup;
    public AccusationPopup accusationPopup { get { return m_accusationPopup; } }

    [SerializeField] private AudioSource m_AudioSource;
    public AudioSource AudioSource { get { return m_AudioSource; } }
    [SerializeField] private AudioClip m_SuccessAudioClip;
    public AudioClip SuccessAudioClip { get { return m_SuccessAudioClip; } }
    [SerializeField] private AudioClip m_FailAudioClip;
    public AudioClip FailAudioClip { get { return m_FailAudioClip; } }

    private Dictionary<PlanetCharacter, SpeechBubble> m_charactersToSpeeches;

    void Start()
    {
        m_charactersToSpeeches = new Dictionary<PlanetCharacter, SpeechBubble>();
        m_cancelAccuseBtn.gameObject.SetActive(false);
        m_validateAccuseBtn.gameObject.SetActive(false);
        m_accuseBtn.gameObject.SetActive(false);
        PlanetManager.OnCharSelectedEvent += OnCharSelected;
        PlanetManager.OnCharUnSelectedEvent += OnCharUnSelected;
    }

    public void OnAccuseModeCb()
    {
        m_accusationPopup.AccusationPopupAnim().Start(this);
        m_cancelAccuseBtn.gameObject.SetActive(true);
        m_validateAccuseBtn.gameObject.SetActive(true);
        this.SetValidateButtonState(false);
        m_accuseBtn.gameObject.SetActive(false);
        PlanetManager.Instance.StartAccusation();
    }

    public void OnAccuseCanceledCb()
    {
        m_cancelAccuseBtn.gameObject.SetActive(false);
        m_validateAccuseBtn.gameObject.SetActive(false);
        m_accuseBtn.gameObject.SetActive(true);
        PlanetManager.Instance.CancelAccusation();

        CleanAllBubbles();
    }

    public void OnAccuseValidatedCb()
    {
        m_cancelAccuseBtn.gameObject.SetActive(false);
        m_validateAccuseBtn.gameObject.SetActive(false);
        m_accuseBtn.gameObject.SetActive(false);
        PlanetManager.Instance.ResolveAccusation();

        CleanAllBubbles();
    }

    public void OnQuitCb()
    {
        ICameraService cameraService = FluffyBox.Services.GetService<ICameraService>();
        cameraService.Zoom(false);
        Gui.GuiService.HideWindow<InGameScreen>();
        Gui.GuiService.ShowWindow<MainGameScreen>(false);
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

    public void CleanAllBubbles()
    {
        foreach (KeyValuePair<PlanetCharacter, SpeechBubble> kv in m_charactersToSpeeches)
        {
            Destroy(kv.Value.gameObject);
        }
        m_charactersToSpeeches.Clear();
    }

    public void SetValidateButtonState(bool value)
    {
        this.m_validateAccuseBtn.interactable = value;
        this.m_validateAccuseBtnText.color = new Color(this.m_validateAccuseBtnText.color.r, this.m_validateAccuseBtnText.color.g, this.m_validateAccuseBtnText.color.b, value ? 1f : 0.75f);
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
