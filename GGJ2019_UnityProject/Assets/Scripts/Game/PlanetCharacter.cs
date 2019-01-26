using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCharacter : PlanetEntity
{
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
