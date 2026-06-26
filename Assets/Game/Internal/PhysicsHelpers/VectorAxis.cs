using System;
using UnityEngine;

namespace Game.Internal.PhysicsHelpers
{
    public class VectorAxis
    {
        public static Vector3 GetRelativeForwardVector2D(Vector3 pointA, Vector3 pointB)
        {
            Vector2 distanceVector = new Vector2(pointB.x - pointA.x, pointB.z - pointA.z);

            if (Math.Abs(distanceVector.x) > Math.Abs(distanceVector.y))
            {
                return Vector3.right * distanceVector.normalized.x;
            }
            else
            {
                return Vector3.forward * distanceVector.normalized.y;
            }
        }
    }
}
