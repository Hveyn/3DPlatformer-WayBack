using DCFApixels.DragonECS;
using UnityEngine;
using Views;
using Client;
using Components;


sealed class EcsStartup : MonoBehaviour
{
    [SerializeField] private InputSettingsScriptableObject inputSettings;
    private EcsDefaultWorld _world;        
    private EcsPipeline _pipeline;
    
    void Start () {
        _world = EcsDefaultWorldSingletonProvider.Instance.Get();
        _pipeline = EcsPipeline.New()
            .AddModule(new ModuleInputSystems())
            .Add(new DebugPrintDevices())
            .Inject(_world)
            .Inject(inputSettings)
            .AutoInject()
            .AddUnityDebug(_world)
            .BuildAndInit();
    }

    void Update () {
        
        _pipeline?.Run ();
    }

    void OnDestroy () {
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