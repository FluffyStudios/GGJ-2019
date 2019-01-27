using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamRevealedSecret : HiddenSpeechParams
{
    [SerializeField]
    private int m_spamCountToUnlock;
    public int spamCountToUnlock { get { return m_spamCountToUnlock; } }

    [SerializeField]
    private int m_targetId;
    public int targetId { get { return m_targetId; } }
}
