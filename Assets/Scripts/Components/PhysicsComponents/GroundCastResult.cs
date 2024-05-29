using System;
using DCFApixels.DragonECS;
using UnityEngine;

namespace Components.PhysicsComponents
{
    [Serializable]
    public struct GroundCastResult: IEcsComponent
    {
        public float maxDistance;
        public float castRadius;
        public LayerMask layers;
        
        public RaycastHit Hit;
        public bool resultCast;
    }
    
    class GroundCastResultTemplate: ComponentTemplate<GroundCastResult> { }
}