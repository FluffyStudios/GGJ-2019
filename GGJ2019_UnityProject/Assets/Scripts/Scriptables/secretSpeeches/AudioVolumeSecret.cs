using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeSecret : HiddenSpeechParams
{
    [SerializeField]
    [Range(0, 1)]
    private int m_volumeValueToUnlock;
    public int volumeValueToUnlock { get { return m_volumeValueToUnlock; } }
}
