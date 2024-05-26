using Components.Movement;
using DCFApixels.DragonECS;
using UnityEngine;

namespace Systems.MovementSystems.JumpSystems
{
    sealed  class JumpInitSystem: IEcsInit
    {
        class Aspect : EcsAspectAuto
        {
            [Inc] public EcsPool<JumpComponent> JumpSettings;
        }
        
        [EcsInject] private EcsDefaultWorld _world;
        
        public void Init()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
                UnityDebugService.Activate();;
                
                float jumpHeight = a.JumpSettings.Get(e).settings.jumpHeight;
                float minJumpHeight = a.JumpSettings.Get(e).settings.minJumpHeight;
                float timeToApex = a.JumpSettings.Get(e).settings.timeToApex;
    
                float gravity = (2 * jumpHeight) / (timeToApex * timeToApex);
        
                Physics.gravity = new Vector3(0, -gravity, 0);

                float initJumpVelocity = Mathf.Sqrt(2 * gravity * jumpHeight);
                
                //termVelocity = math.sqrt(initJumpVelocity^2 + 2*g*(jumpHeight - minJumpHeight))
                float termVelocity = Mathf.Sqrt((initJumpVelocity * initJumpVelocity) + 2 * -gravity * (jumpHeight - minJumpHeight));
                EcsDebug.Print($"_testVelocity: {termVelocity}");
        
                //termTime = timeToApex - (2*(jumpHeight - minJumpHeight)/(initJumpVelocity + termVelocity))
                float termTime = timeToApex - (2 * (jumpHeight - minJumpHeight) / (initJumpVelocity + termVelocity));
                EcsDebug.Print($"_termTime: {termTime}");

                a.JumpSettings.Get(e).gravity = gravity;
                a.JumpSettings.Get(e).initJumpVelocity = initJumpVelocity;
                a.JumpSettings.Get(e).termVelocity = termVelocity;
                a.JumpSettings.Get(e).termTime = termTime;
            }
        }
    }
}