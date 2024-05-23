using System;
using DCFApixels.DragonECS;
using UnityEngine;
using Client;
using Client.MovementSystems;
using Client.Physics;
using Components;
using Mono.InputControl;
using SOData;


sealed class EcsStartup : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler inputHandler;
    private EcsDefaultWorld _world;        
    private EcsPipeline _pipeline;
    private EcsPipeline _fixedUpdatePipeline;

    public void Start () {
        _world = EcsDefaultWorldSingletonProvider.Instance.Get();
        _pipeline = EcsPipeline.New()
            .AddModule(new ModuleInputSystems())
            .Add(new DebugPrintDevices())
            .Inject(_world, inputHandler)
            .AutoInject()
            .AddUnityDebug(_world)
            .BuildAndInit();
        
        _fixedUpdatePipeline =  EcsPipeline.New()
            .Add(new PhysicsInitSystem())
            .Add(new GroundCastSystem())
            .Add(new PrepareHoverSystem())
            .Add(new RelativeSpeedAlongDirectionSystem())
            .Add(new GetSpringForceSystem())
            .Add(new ApplyHoverForceSystem())
            .Inject(_world)
            .AutoInject()
            .BuildAndInit();
    }

    public void Update () {
        
        _pipeline?.Run ();
    }

    public void FixedUpdate()
    {
        _fixedUpdatePipeline?.FixedRun();
    }

    private void OnDestroy () {
        if (_pipeline != null) {
            // list of custom worlds will be cleared
            // during IEcsSystems.Destroy(). so, you
            // need to save it here if you need.
            _pipeline.Destroy();
            _pipeline = null;
        }
        
        if (_fixedUpdatePipeline != null) {
            // list of custom worlds will be cleared
            // during IEcsSystems.Destroy(). so, you
            // need to save it here if you need.
            _fixedUpdatePipeline.Destroy();
            _fixedUpdatePipeline = null;
        }
        
        // cleanup custom worlds here.
            
        // cleanup default world.
        if (_world != null) {
            _world.Destroy();
            _world = null;
        }
    }
}