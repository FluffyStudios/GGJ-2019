using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PlanetMathHelper
{
    public static Vector2 FromPolar(float radius, float angle)
    {
        float radAngle = Mathf.Deg2Rad*-(angle-90);
        return new Vector2(radius * Mathf.Cos(radAngle), radius * Mathf.Sin(radAngle));
    }
    public static Vector2 FromCartesian(float xpos, float ypos)
    {
        // à remplir si besoin
        return new Vector2(0f,0f);
    }
}

public class Planet : MonoBehaviour
{
    private PlanetDescriptor m_descriptor;
    public PlanetDescriptor descriptor { get { return m_descriptor; } }
    private int m_guiltyCount;
    public int guiltyCount { get { return m_guiltyCount; } }
    [SerializeField] private Transform m_entitiesParent;
    [SerializeField] private SpriteRenderer m_planetSceneryLayer;
    [SerializeField] private SpriteRenderer m_planetGroundLayer;
    [SerializeField] private SpriteRenderer m_planetFrontLayer;
    [SerializeField] private float m_frontRadius;
    public AudioSource AudioSource { get { return m_audioSource; } }
    [SerializeField] private AudioSource m_audioSource;
    private PlanetCharacter m_planetLeader;
    public PlanetCharacter planetLeader { get { return m_planetLeader; } }
    private List<PlanetCharacter> m_planetCitizens;
    private int spamCount = 0;
    private PlanetCharacter m_lastSpeaker;
    private Dictionary<PlanetCharacter, PlanetCharacter> m_spamSecretLinks; // secretOwner, spamTarget
    private List<PlanetCharacter> m_soundSecretOwners;

    public void Generate(PlanetDescriptor planetDescriptor)
    {
        PlanetManager.OnCharSelectedEvent += OnCharSelected;
        m_planetCitizens = new List<PlanetCharacter>();
        m_spamSecretLinks = new Dictionary<PlanetCharacter, PlanetCharacter>();
        m_soundSecretOwners = new List<PlanetCharacter>();
        m_descriptor = planetDescriptor;
        m_guiltyCount = 0;
        foreach (PlanetDoodadDescriptor doodadDescriptor in m_descriptor.planetDoodads)
        {
            InstanciateDoodad(doodadDescriptor);
        }

        foreach (PlanetCharacterDescriptor characterDescriptor in m_descriptor.planetCharacters)
        {
            PlanetCharacter citizen = InstanciateCharacter(characterDescriptor);
            m_planetCitizens.Add(citizen);
        }

        RegisterSecretsLinks();

        if (m_descriptor.planetSceneryLayerTexture != null)
        {
            m_planetSceneryLayer.sprite = m_descriptor.planetSceneryLayerTexture;
        }

        if (m_descriptor.planetFrontLayerTexture != null)
        {
            m_planetFrontLayer.sprite = m_descriptor.planetFrontLayerTexture;
        }

        if (m_descriptor.planetGroundLayerTexture != null)
        {
            m_planetGroundLayer.sprite = m_descriptor.planetGroundLayerTexture;
        }

        if (m_descriptor.planetLeader != null)
        {
            m_planetLeader = InstanciateCharacter(m_descriptor.planetLeader);
        }

        if (m_descriptor.planetMusic != null)
        {
            m_audioSource.clip = m_descriptor.planetMusic;
            this.m_audioSource.volume = FluffyBox.Application.Instance.MusicVolume;
        }
    }

    public void PlayMusic()
    {
        m_audioSource.Play();
    }

    private PlanetCharacter InstanciateCharacter(PlanetCharacterDescriptor characterDescriptor)
    {
        if (characterDescriptor.isGuilty)
            m_guiltyCount += 1;
        PlanetCharacter character = Instantiate(PlanetManager.Instance.characterPrefab, m_entitiesParent, false) as PlanetCharacter;
        character.InitializeCharacter(characterDescriptor);
        character.gameObject.transform.localPosition = PlanetMathHelper.FromPolar(m_frontRadius, character.descriptor.entityPos);
        character.gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, -character.descriptor.entityPos);
        return character;
    }

    private PlanetSceneryElement InstanciateDoodad(PlanetDoodadDescriptor doodadDescriptor)
    {

        PlanetSceneryElement doodad = Instantiate(PlanetManager.Instance.doodadPrefab, m_entitiesParent, false) as PlanetSceneryElement;
        doodad.InitializeCharacter(doodadDescriptor);
        doodad.gameObject.transform.localPosition = PlanetMathHelper.FromPolar(m_frontRadius, doodad.descriptor.entityPos);
        doodad.gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, -doodad.descriptor.entityPos);
        return doodad;
    }

    private void OnCharSelected(PlanetCharacter character, LevelState state)
    {
        if (Game.CameraService.CurrentState != CameraManager.CameraState.InGame)
        {
            return;
        }

        CheckSpamSecretReveal(character, m_lastSpeaker);
        m_lastSpeaker = character;

    }

    private void RegisterSecretsLinks()
    {
        foreach (PlanetCharacter citizen in m_planetCitizens)
        {
            HiddenSpeechParams secret = citizen.GetCharacterDescriptor().secretSpeech;
            SpamRevealedSecret spamSecret = secret as SpamRevealedSecret;
            AudioVolumeSecret soundSecret = secret as AudioVolumeSecret;
            if (spamSecret != null)
            {
                int index = spamSecret.targetId;
                if (index < m_planetCitizens.Count)
                    m_spamSecretLinks.Add(citizen, m_planetCitizens[index]);
                // le citoyen a une phrase cachée à révélée
            }
            if (soundSecret != null)
            {
                m_soundSecretOwners.Add(citizen);
            }
        }
    }

    public void CheckSoundModifiedSecrets(float volume)
    {
        if (m_soundSecretOwners.Count == 0)
        {
            return;
        }
            

        foreach(PlanetCharacter character in m_soundSecretOwners)
        {
            AudioVolumeSecret secret = character.GetCharacterDescriptor().secretSpeech as AudioVolumeSecret;
            if (secret != null && secret.volumeValueToUnlock == volume)
                character.RevealSecret();
            else
                character.HideSecret();
            
        }
    }

    private void CheckSpamSecretReveal(PlanetCharacter newSpeaker, PlanetCharacter lastSpeaker)
    {
        if (m_lastSpeaker != null && m_lastSpeaker != newSpeaker)
        {
            spamCount = 1;
            return;
        }
        foreach (KeyValuePair<PlanetCharacter, PlanetCharacter> kv in m_spamSecretLinks)
        {
            
            if(newSpeaker == kv.Value)
            {
                SpamRevealedSecret secret = kv.Key.GetCharacterDescriptor().secretSpeech as SpamRevealedSecret;
                if (secret == null)
                    return;
                spamCount += 1;

                if(spamCount >= secret.spamCountToUnlock)
                {
                    kv.Value.RevealSecret();
                }
            }
        }
    }
}
