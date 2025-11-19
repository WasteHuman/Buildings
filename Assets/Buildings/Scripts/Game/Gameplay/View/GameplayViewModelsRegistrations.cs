using Core.DI;
using Game.Gameplay.Root;
using Game.Gameplay.Root.Services;

namespace Game.Gameplay.View
{
    public static class GameplayViewModelsRegistrations
    {
        public static void Register(DIContainer container)
        {
            container.RegisterFactory(c => new UIGameplayRootViewModel(c.Resolve<GameplayService>())).AsSingle();
            container.RegisterFactory(c => new WorldGameplayRootViewModel()).AsSingle();
        }
    }
}