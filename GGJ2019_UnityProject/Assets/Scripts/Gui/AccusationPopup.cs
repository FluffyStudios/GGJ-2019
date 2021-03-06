﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FluffyTools;

public class AccusationPopup : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Text m_titleText;
    [SerializeField] private Image m_inspectorPortraitImg;
    [SerializeField] private RectTransform m_titleRect;
    [SerializeField] private RectTransform m_portraitRect;
    [SerializeField] private Image m_bgImage;
    [SerializeField] private Text m_successText;
    [SerializeField] private Text m_failedText;
    [SerializeField] private float m_arrivalSpeed;
    [SerializeField] private float m_remainingTime;
    [SerializeField] private float m_ValidationRemainingTime;
    [SerializeField] private float m_portraitSlowDecalValue;
    [SerializeField] private float m_bgFadeTime;
    [SerializeField] private string m_accusationTimeText;
    [SerializeField] private string m_accusationValidationText;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    public Cx.Routine AccusationPopupAnim()
    {
        m_titleText.text = m_accusationTimeText;
        m_bgImage.color = new Color(m_bgImage.color.r, m_bgImage.color.g, m_bgImage.color.b, 0f);
        Vector2 startTitlePos = new Vector2(Screen.currentResolution.width * 0.5f + m_titleRect.rect.width * 0.5f, m_titleRect.position.y);
        Vector2 startPortraitPos = new Vector2(-Screen.currentResolution.width * 0.5f - m_portraitRect.rect.width * 0.5f, m_portraitRect.position.y);
        Vector2 portraitInPos = new Vector2(0f, m_portraitRect.position.y);
        m_titleRect.anchoredPosition = startTitlePos;
        m_portraitRect.anchoredPosition = startTitlePos;
        return Cx.Sequence(
               Cx.Call(() =>
               {
                   gameObject.SetActive(true);
               }),
               Cx.Parallel(
                   Cx.ValueTo((float f) => m_titleRect.anchoredPosition = new Vector2(f, startTitlePos.y), startTitlePos.x, 0f, m_arrivalSpeed),
                   Cx.ValueTo((float f) => m_portraitRect.anchoredPosition = new Vector2(f, startPortraitPos.y), startPortraitPos.x, portraitInPos.x, m_arrivalSpeed),
                   Cx.ValueTo((float f) => m_bgImage.color = new Color(m_bgImage.color.r, m_bgImage.color.g, m_bgImage.color.b, f), 0f, 0.5f, m_bgFadeTime)
                   ),
               Cx.ValueTo((float f) => m_portraitRect.anchoredPosition = new Vector2(f, startPortraitPos.y), portraitInPos.x, portraitInPos.x + m_portraitSlowDecalValue, m_remainingTime),
               Cx.Parallel(
                  Cx.ValueTo((float f) => m_titleRect.anchoredPosition = new Vector2(f, startTitlePos.y), 0f, Screen.currentResolution.width * 0.5f + m_titleRect.rect.width * 0.5f, m_arrivalSpeed),
                  Cx.ValueTo((float f) => m_portraitRect.anchoredPosition = new Vector2(f, startPortraitPos.y), portraitInPos.x + m_portraitSlowDecalValue, -Screen.currentResolution.width * 0.8f - m_portraitRect.rect.width * 0.5f, m_arrivalSpeed),
                  Cx.ValueTo((float f) => m_bgImage.color = new Color(m_bgImage.color.r, m_bgImage.color.g, m_bgImage.color.b, f), 1f, 0f, m_bgFadeTime)

                  ),
               Cx.Call(() =>
               {
                   gameObject.SetActive(false);
               }));
    }
    public Cx.Routine ValidateAccusationPopupAnim(bool asWon)
    {
        m_failedText.gameObject.SetActive(false);
        m_successText.gameObject.SetActive(false);
        GameObject resultGo = m_failedText.gameObject;
        if (asWon)
            resultGo = m_successText.gameObject;
        m_titleText.text = m_accusationValidationText;
        m_bgImage.color = new Color(m_bgImage.color.r, m_bgImage.color.g, m_bgImage.color.b, 0f);
        Vector2 startTitlePos = new Vector2(Screen.currentResolution.width * 0.5f + m_titleRect.rect.width * 0.5f, m_titleRect.position.y);
        Vector2 startPortraitPos = new Vector2(-Screen.currentResolution.width * 0.5f - m_portraitRect.rect.width * 0.5f, m_portraitRect.position.y);
        Vector2 portraitInPos = new Vector2(0f, m_portraitRect.position.y);
        m_titleRect.anchoredPosition = startTitlePos;
        m_portraitRect.anchoredPosition = startTitlePos;
        return Cx.Sequence(
               Cx.Call(() =>
               {
                   gameObject.SetActive(true);
               }),
               Cx.Parallel(
                   Cx.ValueTo((float f) => m_titleRect.anchoredPosition = new Vector2(f, startTitlePos.y), startTitlePos.x, 0f, m_arrivalSpeed),
                   Cx.ValueTo((float f) => m_portraitRect.anchoredPosition = new Vector2(f, startPortraitPos.y), startPortraitPos.x, portraitInPos.x, m_arrivalSpeed),
                   Cx.ValueTo((float f) => m_bgImage.color = new Color(m_bgImage.color.r, m_bgImage.color.g, m_bgImage.color.b, f), 0f, 0.5f, m_bgFadeTime)
                   ),
               Cx.ValueTo((float f) => m_portraitRect.anchoredPosition = new Vector2(f, startPortraitPos.y), portraitInPos.x, portraitInPos.x + m_portraitSlowDecalValue, m_ValidationRemainingTime),
               Cx.Call(() =>
               {
               resultGo.SetActive(true);
                   InGameScreen inGameScreen = Gui.GuiService.GetWindow<InGameScreen>();
                   PlanetManager.Instance.MufflePlanetMusic(2f);
                   inGameScreen.AudioSource.PlayOneShot(asWon ? inGameScreen.SuccessAudioClip : inGameScreen.FailAudioClip);
               }),
               Cx.Delay(2f),
               Cx.Parallel(
                  Cx.ValueTo((float f) => m_titleRect.anchoredPosition = new Vector2(f, startTitlePos.y), 0f, Screen.currentResolution.width * 0.5f + m_titleRect.rect.width * 0.5f, m_arrivalSpeed),
                  Cx.ValueTo((float f) => m_portraitRect.anchoredPosition = new Vector2(f, startPortraitPos.y), portraitInPos.x + m_portraitSlowDecalValue, -Screen.currentResolution.width * 0.8f - m_portraitRect.rect.width * 0.5f, m_arrivalSpeed),
                  Cx.ValueTo((float f) => m_bgImage.color = new Color(m_bgImage.color.r, m_bgImage.color.g, m_bgImage.color.b, f), 1f, 0f, m_bgFadeTime)

                  ),
               Cx.Call(() =>
               {
                   resultGo.SetActive(false);
                   gameObject.SetActive(false);
               }));
    }
}
