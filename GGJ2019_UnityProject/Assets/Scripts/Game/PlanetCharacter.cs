using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCharacter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private AudioSource m_audioSource;
    public SpriteRenderer spriteRenderer { get { return m_spriteRenderer; } }
    private PlanetCharacterDescriptor m_descriptor;
    public PlanetCharacterDescriptor descriptor { get { return m_descriptor; } }

    public void InitializeCharacter(PlanetCharacterDescriptor charDescriptor)
    {
        m_descriptor = charDescriptor;
        if (m_descriptor.entitySprite != null)
            m_spriteRenderer.sprite = m_descriptor.entitySprite;
    }
    
    public void PlaySelectionSound()
    {
        if (m_descriptor.accusedSound != null)
        {
            m_audioSource.clip = m_descriptor.characterSound;
            m_audioSource.Play();
        }
    }

    public void PlayAccusedSound()
    {
        if (m_descriptor.accusedSound != null)
        {
            m_audioSource.clip = m_descriptor.accusedSound;
            m_audioSource.Play();
        }
        
    }

    void OnMouseDown()
    {
        // check si enquête en cours ou denonciation
        // si enquête en cours
        Debug.Log("ce personnage dit : " + m_descriptor.speech);
        
    }

}
