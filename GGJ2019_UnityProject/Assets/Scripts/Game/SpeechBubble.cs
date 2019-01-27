using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour
{
    [SerializeField] private RectTransform m_characterNameGroup;
    [SerializeField] private Text m_characterNameText;
    [SerializeField] private Text m_bubbleText;
    [SerializeField] private RectTransform m_rect;
    private GameObject m_target;
    

    public void ActivateBubble(PlanetCharacter target, LevelState state)
    {
        m_target = target.gameObject;
        PlanetCharacterDescriptor targetDescriptor = target.GetCharacterDescriptor();
        string newText = string.Empty;
        if (state == LevelState.Accusing)
        {
            newText = targetDescriptor.accusedSpeech;
        }
        else if (state == LevelState.Investigating || state == LevelState.Starting)
        {
            if(!target.isSecretActivated)
                newText = targetDescriptor.speech;
            else
            {
                newText = targetDescriptor.secretSpeech.secretSpeechString;
            }
            
        }

        m_bubbleText.text = newText;
        float textHeight = m_bubbleText.preferredHeight;
        m_bubbleText.rectTransform.sizeDelta = new Vector2(m_bubbleText.rectTransform.sizeDelta.x, textHeight);
        m_rect.sizeDelta = new Vector2(m_rect.sizeDelta.x, textHeight + Mathf.Abs(m_bubbleText.rectTransform.anchoredPosition.y) + m_bubbleText.rectTransform.offsetMin.x);

        m_characterNameText.text = targetDescriptor.characterName;
        float nameWidth = m_characterNameText.preferredWidth;
        m_characterNameGroup.sizeDelta = new Vector2(
            Mathf.Abs(m_characterNameText.rectTransform.offsetMin.x * 2) + nameWidth,
            m_characterNameGroup.sizeDelta.y
        );

        this.Update();
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if(m_target != null)
        {
            transform.position = new Vector2(m_target.transform.position.x, m_target.transform.position.y + 0.5f);
        }
    }
}
