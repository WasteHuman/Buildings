using Core.DI;
using Game.Gameplay.Root;
using Game.GameRoot.Services;
using Game.MainMenu.Root;
using R3;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Game.GameRoot
{
    public class GameEntryPoint
    {
        private static GameEntryPoint _instance;

        private Coroutines _coroutines;
        private UIRootView _uiRoot;
        private readonly DIContainer _rootContainer = new();
        private DIContainer _cachedSceneContainer;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutoStartGame()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            _instance = new GameEntryPoint();
            _instance.RunGame();
        }

        private GameEntryPoint()
        {
            _coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(_coroutines.gameObject);

            UIRootView prefabUIRoot = Resources.Load<UIRootView>("UIRoot");
            _uiRoot = Object.Instantiate(prefabUIRoot);
            Object.DontDestroyOnLoad(_uiRoot.gameObject);
            _rootContainer.RegisterInstance(_uiRoot);
            _rootContainer.RegisterFactory(c => new SomeCommonService()).AsSingle();
        }

        private void RunGame()
        {
#if UNITY_EDITOR
            var sceneName = SceneManager.GetActiveScene().name;

            if(sceneName == Scenes.GAMEPLAY)
            {
                GameplayEnterParams enterParams = new("somesave", 1);
                _coroutines.StartCoroutine(LoadAndStartGameplay(enterParams));
                return;
            }

            if (sceneName == Scenes.MAIN_MENU)
            {
                _coroutines.StartCoroutine(LoadAndStartMainMenu());
                return;
            }

            if (sceneName != Scenes.BOOT)
                return;
#endif

            _coroutines.StartCoroutine(LoadAndStartMainMenu());
        }

        private IEnumerator LoadAndStartGameplay(GameplayEnterParams enterParams = null)
        {
            _uiRoot.ShowLoadingScreen();
            _cachedSceneContainer?.Dispose();

            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.GAMEPLAY);

            yield return new WaitForSeconds(0.5f);

            GameplayEntryPoint sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
            DIContainer gameplayContainer = _cachedSceneContainer = new DIContainer(_rootContainer);

            sceneEntryPoint.Run(gameplayContainer, enterParams).Subscribe(gameplayExitParams => 
            {
                _coroutines.StartCoroutine(LoadAndStartMainMenu(gameplayExitParams.MainMenuEnterParams));
            });

            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadAndStartMainMenu(MainMenuEnterParams enterParams = null)
        {
            _uiRoot.ShowLoadingScreen();
            _cachedSceneContainer?.Dispose();

            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.MAIN_MENU);

            yield return new WaitForSeconds(0.5f);

            MainMenuEntryPoint sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();
            DIContainer mainMenuContainer = _cachedSceneContainer = new DIContainer(_rootContainer);

            sceneEntryPoint.Run(mainMenuContainer, enterParams).Subscribe(mainMenuExitParams =>
            {
                string targetSceneName = mainMenuExitParams.TargetSceneEnterParams.SceneName;

                if (targetSceneName == Scenes.GAMEPLAY)
                    _coroutines.StartCoroutine(LoadAndStartGameplay(mainMenuExitParams.TargetSceneEnterParams.As<GameplayEnterParams>()));
            });

            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}