using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FluffyTools
{
    public static class MathHelpers
    {
        public static Vector2 FromPolar(float radius, float angle)
        {
            float radAngle = Mathf.Deg2Rad * -(angle - 90);
            return new Vector2(radius * Mathf.Cos(radAngle), radius * Mathf.Sin(radAngle));
        }
    }
}
