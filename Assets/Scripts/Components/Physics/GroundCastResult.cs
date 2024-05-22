using System;
using DCFApixels.DragonECS;
using UnityEngine;

namespace Components.Physics
{
    [Serializable]
    public struct GroundCastResult: IEcsComponent
    {
        public float maxDistance;
        public float castRadius;
        public RaycastHit hit;
        public bool resultCast;
    }
    
    class GroundCastResultTemplate: ComponentTemplate<GroundCastResult> { }
}