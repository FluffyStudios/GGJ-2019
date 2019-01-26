using System.Collections;
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
    [SerializeField] private float m_arrivalSpeed;
    [SerializeField] private float m_remainingTime;
    [SerializeField] private float m_portraitSlowDecalValue;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    public Cx.Routine AccusationPopupAnim()
    {
        Vector2 startTitlePos = new Vector2(Screen.currentResolution.width * 0.5f + m_titleRect.rect.width * 0.5f, m_titleRect.position.y);
        Vector2 startPortraitPos = new Vector2(-Screen.currentResolution.width * 0.5f - m_portraitRect.rect.width * 0.5f, m_portraitRect.position.y);
        Vector2 portraitInPos = new Vector2(0f/*-Screen.currentResolution.width * 0.5f + m_portraitRect.rect.width*0.5f*/, m_portraitRect.position.y);
        m_titleRect.anchoredPosition = startTitlePos;
        m_portraitRect.anchoredPosition = startTitlePos;
        return Cx.Sequence(
               Cx.Call(() =>
               {
                   gameObject.SetActive(true);
               }),
               Cx.Parallel(
                   Cx.ValueTo((float f) => m_titleRect.anchoredPosition = new Vector2(f, startTitlePos.y), startTitlePos.x, 0f, m_arrivalSpeed),
                   Cx.ValueTo((float f) => m_portraitRect.anchoredPosition = new Vector2(f, startPortraitPos.y), startPortraitPos.x, portraitInPos.x, m_arrivalSpeed)
                   ),
               Cx.ValueTo((float f) => m_portraitRect.anchoredPosition = new Vector2(f, startPortraitPos.y), portraitInPos.x, portraitInPos.x + m_portraitSlowDecalValue, m_remainingTime),
               Cx.Parallel(
                  Cx.ValueTo((float f) => m_titleRect.anchoredPosition = new Vector2(f, startTitlePos.y), 0f, Screen.currentResolution.width * 0.5f + m_titleRect.rect.width * 0.5f, m_arrivalSpeed),
                  Cx.ValueTo((float f) => m_portraitRect.anchoredPosition = new Vector2(f, startPortraitPos.y), portraitInPos.x + m_portraitSlowDecalValue, -Screen.currentResolution.width * 0.8f - m_portraitRect.rect.width * 0.5f, m_arrivalSpeed)
                   ),
               Cx.Call(() =>
               {
                   gameObject.SetActive(false);
               }));
    }
}
