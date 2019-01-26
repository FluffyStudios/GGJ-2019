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
    [SerializeField] private float m_animSpeed;
    [SerializeField] private float m_remainingTime;
    void Start()
    {
        gameObject.SetActive(false);
    }

    public Cx.Routine TitleAnimation(int missionCount, string missionName, string planetName)
    {        
        /*m_titleRect.rect. = new Vector2(-Screen.width * 0.5f, m_titleRect.position.y);
        m_planetNameRect.position = new Vector2(-Screen.width * 0.5f, m_planetNameRect.position.y);*/

        return Cx.Sequence(
               Cx.Call(() =>
               {
                   m_titleText.text = string.Format("Mission {0} : {1}", missionCount, missionName);
                   m_planetNameText.text = string.Format("Destination : {0}", planetName);
                   gameObject.SetActive(true);
               }),
               Cx.Delay(m_remainingTime),
               Cx.Call(() =>
               {
                   gameObject.SetActive(false);
               }));
    }
}

