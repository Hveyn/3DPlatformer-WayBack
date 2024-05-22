using DCFApixels.DragonECS;
using UnityEngine;
using Client;
using Components;
using Mono.InputControl;
using SOData;


sealed class EcsStartup : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler inputHandler;
    private EcsDefaultWorld _world;        
    private EcsPipeline _pipeline;

    public void Start () {
        _world = EcsDefaultWorldSingletonProvider.Instance.Get();
        _pipeline = EcsPipeline.New()
            .AddModule(new ModuleInputSystems())
            .Add(new DebugPrintDevices())
            .Inject(_world, inputHandler)
            .AutoInject()
            .AddUnityDebug(_world)
            .BuildAndInit();
    }

    public void Update () {
        
        _pipeline?.Run ();
    }

    private void OnDestroy () {
        if (_pipeline != null) {
            // list of custom worlds will be cleared
            // during IEcsSystems.Destroy(). so, you
            // need to save it here if you need.
            _pipeline.Destroy();
            _pipeline = null;
        }
        
        // cleanup custom worlds here.
            
        // cleanup default world.
        if (_world != null) {
            _world.Destroy();
            _world = null;
        }
    }
}