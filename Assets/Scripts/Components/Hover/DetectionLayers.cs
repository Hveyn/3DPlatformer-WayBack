using System;
using DCFApixels.DragonECS;
using UnityEngine;

namespace Components.Hover
{
    [Serializable]
    public struct DetectionLayers: IEcsComponent
    {
        public LayerMask detectionLayers;
    }
    
    class DetectionLayersTemplate: ComponentTemplate<DetectionLayers> { }
}