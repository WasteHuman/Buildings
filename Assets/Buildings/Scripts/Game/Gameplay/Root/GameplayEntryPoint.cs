using Core.DI;
using Game.Gameplay.View;
using Game.GameRoot;
using Game.MainMenu.Root;
using R3;
using UnityEngine;

namespace Game.Gameplay.Root
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIGamePlayRootBinder _uiSceneRootPrefab;

        public Observable<GameplayExitParams> Run(DIContainer gameplayContainer, GameplayEnterParams enterParams = null)
        {
            GameplayRegistrations.Register(gameplayContainer, enterParams);
            DIContainer gameplayViewModelsContainer = new(gameplayContainer);
            GameplayViewModelsRegistrations.Register(gameplayViewModelsContainer);

            gameplayViewModelsContainer.Resolve<UIGameplayRootViewModel>();
            gameplayViewModelsContainer.Resolve<WorldGameplayRootViewModel>();

            UIRootView uiRoot = gameplayContainer.Resolve<UIRootView>();
            UIGamePlayRootBinder uiScene = Instantiate(_uiSceneRootPrefab);
            uiRoot.AttachSceneUI(uiScene.gameObject);

            var exitSceneSignalSubj = new Subject<Unit>();
            uiScene.Bind(exitSceneSignalSubj);

            MainMenuEnterParams mainMenuEnterParams = new("Some string value");
            GameplayExitParams exitParams = new(mainMenuEnterParams);

            var exitToMainMenuSceneSignal = exitSceneSignalSubj.Select(_ => exitParams);

            Debug.Log($"GAMEPLAY ENTRY POINT: save file name: {enterParams?.SaveFileName} \n level number = {enterParams?.LevelNumber}");

            return exitToMainMenuSceneSignal;
        }
    }
}