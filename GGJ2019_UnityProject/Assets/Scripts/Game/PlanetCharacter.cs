using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCharacter : PlanetEntity
{
    private bool m_isSecretActivated;
    public bool isSecretActivated { get { return m_isSecretActivated; } }

    public void RevealSecret()
    {
        if(GetCharacterDescriptor().secretSpeech!=null)
            m_isSecretActivated = true;
    }
    public void HideSecret()
    {
        m_isSecretActivated = false;
    }

    public void PlayAccusedSound()
    {
        PlanetCharacterDescriptor CharDescriptor = GetCharacterDescriptor();
        if (CharDescriptor.accusedSound != null)
        {
            audioSource.clip = CharDescriptor.accusedSound;
            audioSource.Play();
        }      
    }

    public PlanetCharacterDescriptor GetCharacterDescriptor()
    {
        return descriptor as PlanetCharacterDescriptor;
    }
}
