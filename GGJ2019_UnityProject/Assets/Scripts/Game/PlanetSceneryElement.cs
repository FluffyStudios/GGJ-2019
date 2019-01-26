using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planetsceneryElement : PlanetEntity
{
    public PlanetDoodadDescriptor GetSceneryDescriptor()
    {
        return descriptor as PlanetDoodadDescriptor;
    }
}
