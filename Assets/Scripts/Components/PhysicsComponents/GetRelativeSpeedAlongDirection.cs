using System;
using DCFApixels.DragonECS;
using UnityEngine;

namespace Components.PhysicsComponents
{
    [Serializable]
    public struct GetRelativeSpeedAlongDirection: IEcsComponent
    {
        public Rigidbody targetBody;
        public Rigidbody frameBody;
        public Vector3 direction;
        
        /// <summary>
        /// Output
        /// </summary>
        public float relativeSpeed;
    }
}