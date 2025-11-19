using Game.MainMenu.Root;

namespace Game.Gameplay.Root
{
    public class GameplayExitParams
    {
        public MainMenuEnterParams MainMenuEnterParams { get; }

        public GameplayExitParams(MainMenuEnterParams mainMenuEnterParams)
        {
            MainMenuEnterParams = mainMenuEnterParams;
        }
    }
}