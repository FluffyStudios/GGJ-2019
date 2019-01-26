using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlanetEntity : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer m_spriteRenderer;
    [SerializeField] protected AudioSource m_audioSource;
    public AudioSource audioSource { get { return m_audioSource; } }
    public SpriteRenderer spriteRenderer { get { return m_spriteRenderer; } }
    private PlanetEntityDescriptor m_descriptor;
    public PlanetEntityDescriptor descriptor { get { return m_descriptor; } }
    // Start is called before the first frame update
    public void InitializeCharacter(PlanetEntityDescriptor charDescriptor)
    {
        m_descriptor = charDescriptor;
        if (m_descriptor.entitySprite != null)
            m_spriteRenderer.sprite = m_descriptor.entitySprite;

        int sortingLayerIndex = (int)m_descriptor.sortingLayer;
        if (sortingLayerIndex > 0 && sortingLayerIndex < SortingLayer.layers.Length)
        {
            m_spriteRenderer.sortingLayerID = SortingLayer.layers[sortingLayerIndex].id;
        }

        m_spriteRenderer.sortingOrder = m_descriptor.sortingOrder;
    }

    public void PlaySelectionSound()
    {
        if (m_descriptor.entitySound != null)
        {
            m_audioSource.clip = m_descriptor.entitySound;
            m_audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
