using System;
using DCFApixels.DragonECS;
using UnityEngine;

namespace Components.Physics
{
    [Serializable]
    public struct GetSpringForce: IEcsComponent
    {
        public float height;
        public float mass;
        public float dampFrequency;
        public float dampFactor;
        public Vector3 direction;
        
        /// <summary>
        /// Output
        /// </summary>
        public Vector3 force;
    }
}