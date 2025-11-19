using Core.DI;
using Game.Gameplay.Root.Services;
using Game.GameRoot.Services;

namespace Game.Gameplay.Root
{
    public static class GameplayRegistrations
    {
        public static void Register(DIContainer container, GameplayEnterParams enterParams)
        {
            container.RegisterFactory(c => new GameplayService(c.Resolve<SomeCommonService>())).AsSingle();
        }
    }
}