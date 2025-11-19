using Core.DI;
using Game.Gameplay.Root;
using Game.Gameplay.View;
using Game.GameRoot;
using Game.MainMenu.Root.View;
using R3;
using UnityEngine;

namespace Game.MainMenu.Root
{
    public class MainMenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIMainMenuRootBinder _uiSceneRootPrefab;

        public Observable<MainMenuExitParams> Run(DIContainer mainMenuContainer, MainMenuEnterParams enterParams = null)
        {
            MainMenuRegistrations.Register(mainMenuContainer, enterParams);
            DIContainer mainMenuViewModelsContainer = new(mainMenuContainer);
            MainMenuViewModelsRegistrations.Register(mainMenuViewModelsContainer);

            mainMenuViewModelsContainer.Resolve<UIMainMenuRootViewModel>();

            UIRootView uiRoot = mainMenuContainer.Resolve<UIRootView>();
            UIMainMenuRootBinder uiScene = Instantiate(_uiSceneRootPrefab);
            uiRoot.AttachSceneUI(uiScene.gameObject);

            var exitSceneSignalSubj = new Subject<Unit>();
            uiScene.Bind(exitSceneSignalSubj);

            Debug.Log($"MAIN MENU ENTRY POINT: Run main menu scene. Results: {enterParams?.Result}");

            string saveFileName = "some.save";
            int levelNumber = Random.Range(0, 300);

            GameplayEnterParams gameplayEnterParams = new(saveFileName, levelNumber);
            MainMenuExitParams mainMenuExitParams = new(gameplayEnterParams);

            var exitToMainMenuSceneSignal = exitSceneSignalSubj.Select(_ => mainMenuExitParams);

            return exitToMainMenuSceneSignal;
        }
    }
}