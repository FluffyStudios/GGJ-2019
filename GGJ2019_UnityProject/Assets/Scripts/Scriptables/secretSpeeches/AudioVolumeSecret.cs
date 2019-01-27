using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeSecret : HiddenSpeechParams
{
    [SerializeField]
    [Range(0f, 1f)]
    private float m_volumeValueToUnlock;
    public int volumeValueToUnlock { get { return volumeValueToUnlock; } }
}
