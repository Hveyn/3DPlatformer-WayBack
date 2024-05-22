using System;
using DCFApixels.DragonECS;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components.Physics
{
    [Serializable]
    public struct GetRelativeSpeedAlongDirection: IEcsComponent
    {
        public Rigidbody targetBody;
        public Rigidbody frameBody;
        public Vector3 direction;
        public float result;
    }
}