using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PlanetEntityDescriptor
{
    [SerializeField]
    private Sprite m_entitySprite;
    public Sprite entitySprite { get { return m_entitySprite; } }

    [SerializeField]
    private float m_entityPos;
    public float entityPos { get { return m_entityPos; } }

    [SerializeField]
    private AudioClip m_entitySound;
    public AudioClip entitySound { get { return m_entitySound; } }

}
