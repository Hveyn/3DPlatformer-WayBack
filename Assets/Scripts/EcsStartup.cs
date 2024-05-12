using DCFApixels.DragonECS;
using UnityEngine;
using Views;
using Client;


sealed class EcsStartup : MonoBehaviour
{
    private EcsDefaultWorld _world;        
    private EcsPipeline _pipeline;
    private EcsPipeline _testpipeline;
    
    [SerializeField] private PlayerInputSettingsView playerInputSettingsView;
    
    void Start () {
        _world = new EcsDefaultWorld();
        _pipeline = EcsPipeline.New()
            .AddModule(new ModuleInputSystems())
            .Add(new DebugPrintDevices())
            .Inject(_world)
            .Inject(playerInputSettingsView)
            .AutoInject()
            .AddUnityDebug()
            .BuildAndInit();
    }

    void Update () {
        // process systems here.
        _pipeline?.Run ();
    }

    void OnDestroy () {
        if (_pipeline != null) {
            // list of custom worlds will be cleared
            // during IEcsSystems.Destroy(). so, you
            // need to save it here if you need.
            _pipeline.Destroy ();
            _pipeline = null;
        }
          
        // cleanup custom worlds here.
            
        // cleanup default world.
        if (_world != null) {
            _world.Destroy ();
            _world = null;
        }
    }
}