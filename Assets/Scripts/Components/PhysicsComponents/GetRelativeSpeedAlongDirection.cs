using System;
using DCFApixels.DragonECS;
using UnityEngine;
using UnityEngine.Serialization;

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
        [FormerlySerializedAs("result")] public float relativeSpeed;
    }
}