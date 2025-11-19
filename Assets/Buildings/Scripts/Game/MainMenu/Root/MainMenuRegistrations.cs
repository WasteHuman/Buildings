using Core.DI;
using Game.GameRoot.Services;
using Game.MainMenu.Services;

namespace Game.MainMenu.Root
{
    public static class MainMenuRegistrations
    {
        public static void Register(DIContainer container, MainMenuEnterParams enterParams)
        {
            container.RegisterFactory(c => new MainMenuService(c.Resolve<SomeCommonService>())).AsSingle();
        }
    }
}
