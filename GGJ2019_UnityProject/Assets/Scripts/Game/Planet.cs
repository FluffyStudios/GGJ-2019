using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private float m_backRadius;
    [SerializeField] private float m_frontRadius;

    public void Generate(PlanetDescriptor planetDescriptor)
    {
        m_descriptor = planetDescriptor;
        m_guiltyCount = 0;
        foreach(PlanetCharacterDescriptor characterDescriptor in m_descriptor.planetCharacters)
        {
            PlanetCharacter character = Instantiate(PlanetManager.Instance.characterPrefab, m_entitiesParent, false) as PlanetCharacter;
            character.InitializeCharacter(characterDescriptor);
            character.gameObject.transform.localPosition = PlanetMathHelper.FromPolar(m_frontRadius, character.descriptor.entityPos);
            character.gameObject.transform.localRotation = Quaternion.Euler(0f,0f, -character.descriptor.entityPos);
            if (characterDescriptor.isGuilty)
                m_guiltyCount += 1;
        }
        // x = r cos alpha
        // y = r sin Alpha
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
