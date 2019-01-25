using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PlanetEntity
{
    [SerializeField]
    private Sprite m_entitySprite;
    public Sprite entitySprite { get { return m_entitySprite; } }

    [SerializeField]
    private float m_entityPos;
    public float entityPos { get { return m_entityPos; } }
    
}
