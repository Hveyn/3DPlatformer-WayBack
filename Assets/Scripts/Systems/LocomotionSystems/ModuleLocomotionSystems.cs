using DCFApixels.DragonECS;

namespace Systems.LocomotionSystems
{
    /// <summary>
    /// Модуль систем анимации персонажа
    /// </summary>
    public class ModuleLocomotionSystems: IEcsModule
    {
        public void Import(EcsPipeline.Builder b)
        {
            b.Add(new CharacterLocomotionInitSystem());
            b.Add(new CharacterLocomotionSystem());
        }
    }
}