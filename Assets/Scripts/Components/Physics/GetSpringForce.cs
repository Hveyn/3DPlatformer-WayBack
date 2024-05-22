using System;
using DCFApixels.DragonECS;
using UnityEngine;

namespace Components.Physics
{
    [Serializable]
    public struct GetSpringForce: IEcsComponent
    {
        public float springDelta;
        public float springSpeed;
        public float springStrength;
        public float dampStrength;
        public Vector3 direction;
        public Vector3 force;
    }
}