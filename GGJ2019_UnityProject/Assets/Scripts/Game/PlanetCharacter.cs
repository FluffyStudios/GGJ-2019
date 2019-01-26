using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCharacter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    public SpriteRenderer spriteRenderer { get { return m_spriteRenderer; } }
    private PlanetCharacterDescriptor m_descriptor;
    public PlanetCharacterDescriptor descriptor { get { return m_descriptor; } }

    public void InitializeCharacter(PlanetCharacterDescriptor charDescriptor)
    {
        m_descriptor = charDescriptor;
        if (m_descriptor.entitySprite != null)
            m_spriteRenderer.sprite = m_descriptor.entitySprite;
    }
    
    void OnMouseDown()
    {
        // check si enquête en cours ou denonciation
        // si enquête en cours
        Debug.Log("ce personnage dit : " + m_descriptor.speech);
        
    }

}
