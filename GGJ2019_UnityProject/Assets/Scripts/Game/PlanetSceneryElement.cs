using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSceneryElement : PlanetEntity
{
    public PlanetDoodadDescriptor GetSceneryDescriptor()
    {
        return descriptor as PlanetDoodadDescriptor;
    }
}
