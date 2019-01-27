using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private int m_volume;
    [SerializeField] private AudioListener m_audioListener;
    [SerializeField] private Image m_disabledImage;
    // Start is called before the first frame update
    void Start()
    {
        m_volume = 1;  
    }
    
    public void OnSoundVolumeChanged()
    {
        if (m_volume == 1)
            m_volume = 0;
        else
            m_volume = 1;
        AudioListener.volume = m_volume;
        m_disabledImage.gameObject.SetActive(!m_disabledImage.gameObject.activeSelf);
        PlanetManager.Instance.currentPlanet.CheckSoundModifiedSecrets(m_volume);
    }
}
