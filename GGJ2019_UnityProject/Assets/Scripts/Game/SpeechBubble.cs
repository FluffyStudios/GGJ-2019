using System.Collections;
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
        if (state == LevelState.Accusing)
        {
            m_bubbleText.text = target.descriptor.accusedSpeech;
        }
        else if (state == LevelState.Investigating)
        {
            m_bubbleText.text = target.descriptor.speech;
        }
        transform.position = new Vector2(m_target.transform.position.x, m_target.transform.position.y + 1.5f);
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
