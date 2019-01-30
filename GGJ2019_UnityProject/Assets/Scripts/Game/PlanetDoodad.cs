using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetDoodad : PlanetEntity
{
    public PlanetDoodadDescriptor GetSceneryDescriptor()
    {
        return descriptor as PlanetDoodadDescriptor;
    }
}
