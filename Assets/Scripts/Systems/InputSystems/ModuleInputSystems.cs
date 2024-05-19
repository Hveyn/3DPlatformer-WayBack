using DCFApixels.DragonECS;

namespace Client
{
    public class ModuleInputSystems: IEcsModule
    {
        public void Import(EcsPipeline.Builder b)
        {
            b.Add(new CursorLockSystem());
            b.Add(new PlayerInputInitSystem());
            b.Add(new PlayerInputDestroySystem());
        }
    }
}