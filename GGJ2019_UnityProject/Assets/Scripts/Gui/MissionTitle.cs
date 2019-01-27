using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FluffyTools;

public class MissionTitle : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Text m_titleText;
    [SerializeField] private Text m_planetNameText;
    [SerializeField] private RectTransform m_titleRect;
    [SerializeField] private RectTransform m_planetNameRect;
    [SerializeField] private float m_arrivalSpeed;
    [SerializeField] private float m_remainingTime;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    public Cx.Routine TitleAnimation(int missionCount, string missionName, string planetName)
    {
        Vector2 startTitlePos = new Vector2(-Screen.currentResolution.width * 0.5f - m_titleRect.rect.width*0.5f, m_titleRect.position.y);        
        Vector2 startPlanetNamePos = new Vector2(Screen.currentResolution.width * 0.5f + m_planetNameRect.rect.width * 0.5f, m_planetNameRect.position.y);
        m_titleRect.anchoredPosition = startTitlePos;
        m_planetNameRect.anchoredPosition = startTitlePos;
        return Cx.Sequence(
               Cx.Call(() =>
               {
                   
                   
                   Debug.Log("dans routine de title animation");
                   m_titleText.text = string.Format("Mission {0} : {1}", missionCount + 1, missionName);
                   m_planetNameText.text = string.Format("Planète {0}", planetName);
                   gameObject.SetActive(true);
               }),
               Cx.Parallel(
                   Cx.ValueTo((float f) => m_titleRect.anchoredPosition = new Vector2(f, startTitlePos.y), startTitlePos.x, 0f, m_arrivalSpeed),
                   Cx.ValueTo((float f) => m_planetNameRect.anchoredPosition = new Vector2(f, startPlanetNamePos.y), startPlanetNamePos.x, 0f, m_arrivalSpeed)
                   ),
               Cx.Delay(m_remainingTime),
               Cx.Parallel(
                  Cx.ValueTo((float f) => m_titleRect.anchoredPosition = new Vector2(f, startTitlePos.y), 0f, Screen.currentResolution.width * 0.5f + m_titleRect.rect.width * 0.5f, m_arrivalSpeed),
                  Cx.ValueTo((float f) => m_planetNameRect.anchoredPosition = new Vector2(f, startPlanetNamePos.y), 0f, -Screen.currentResolution.width * 0.5f - m_planetNameRect.rect.width * 0.5f, m_arrivalSpeed)
                   ),
               Cx.Call(() =>
               {
                   gameObject.SetActive(false);
               }));
    }
}

