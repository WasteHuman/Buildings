using Game.Gameplay.Root.Services;

namespace Game.Gameplay.Root
{
    public class UIGameplayRootViewModel
    {
        private readonly GameplayService _gameplayService;

        public UIGameplayRootViewModel(GameplayService gameplayService)
        {
            this._gameplayService = gameplayService;
        }
    }
}