using DCFApixels.DragonECS;

namespace Systems.InputSystems 
{
    /// <summary>
    /// Модуль систем ввода
    /// </summary>
    public class ModuleInputSystems: IEcsModule
    {
        public void Import(EcsPipeline.Builder b)
        {
           // b.Add(new CursorLockSystem());
            b.Add(new PlayerInputRunSystem());
        }
    }
}