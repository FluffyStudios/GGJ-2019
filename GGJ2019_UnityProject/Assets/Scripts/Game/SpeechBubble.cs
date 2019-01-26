﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour
{
    [SerializeField] private Text m_bubbleText;
    [SerializeField] private RectTransform m_rect;
    private GameObject m_target;
    

    public void ActivateBubble(PlanetCharacter target, LevelState state)
    {
        m_target = target.gameObject;
        string newText = string.Empty;
        if (state == LevelState.Accusing)
        {
            newText = target.GetCharacterDescriptor().accusedSpeech;
        }
        else if (state == LevelState.Investigating)
        {
            newText = target.GetCharacterDescriptor().speech;
        }

        TextGenerator textGen = new TextGenerator();
        TextGenerationSettings generationSettings = m_bubbleText.GetGenerationSettings(m_bubbleText.rectTransform.rect.size);
        float textHeight = textGen.GetPreferredHeight(newText, generationSettings);
        m_bubbleText.text = newText;
        m_bubbleText.rectTransform.sizeDelta = new Vector2(m_bubbleText.rectTransform.sizeDelta.x, textHeight);
        m_rect.sizeDelta = new Vector2(m_rect.sizeDelta.x, textHeight + Mathf.Abs(m_bubbleText.rectTransform.anchoredPosition.y) * 2f);

        transform.position = new Vector2(m_target.transform.position.x, m_target.transform.position.y + 2.5f);
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if(m_target != null)
        {
            transform.position = new Vector2(m_target.transform.position.x, m_target.transform.position.y + 1.5f);
        }
    }
}
