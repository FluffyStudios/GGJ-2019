using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntitySortingLayer
{
    Default = 0,
    Scenery = 1,
    Ground = 2,
    Character = 3,
    UI = 4
}

public abstract class PlanetEntityDescriptor
{
    [SerializeField]
    private Sprite m_entitySprite;
    public Sprite entitySprite { get { return m_entitySprite; } }

    [SerializeField]
    private float m_entityPos;
    public float entityPos { get { return m_entityPos; } }

    [SerializeField]
    protected EntitySortingLayer m_sortingLayer = EntitySortingLayer.Scenery;
    public EntitySortingLayer sortingLayer { get { return m_sortingLayer; } }
    
    [SerializeField]
    protected int m_sortingOrder = 0;
    public int sortingOrder { get { return m_sortingOrder; } }

    [SerializeField]
    private AudioClip m_entitySound;
    public AudioClip entitySound { get { return m_entitySound; } }
}
