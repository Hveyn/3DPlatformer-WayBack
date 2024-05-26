using Client.LocomotionSystems;
using DCFApixels.DragonECS;

namespace Client
{
    public class ModuleLocomotionSystems: IEcsModule
    {
        public void Import(EcsPipeline.Builder b)
        {
            b.Add(new CharacterLocomotionInitSystem());
            b.Add(new CharacterLocomotionSystem());
        }
    }
}