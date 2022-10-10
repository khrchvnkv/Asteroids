using System;
using CoreLogic;
using CoreLogic.SceneLoader;
using CoreLogic.UI;
using Cysharp.Threading.Tasks;
using UnityLogic.UnityEventSystem;
using UnityEngine;
using UnityLogic.UI;
using UnityLogic.UI.MainMenu;
using UnityLogic.UI.SplashScreen;

namespace UnityLogic
{
    public sealed class GameCore : MonoBehaviour
    {
        #region Static
        public static GameCore Instance { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitializeCore()
        {
            CreateInstance();
        }
        private static void CreateInstance()
        {
            if (Instance != default)
            {
                throw new Exception("GameCore already created");
            }
            Instance = new GameObject("[GameCore]").AddComponent<GameCore>();
        }
        #endregion
        
        public EventManager EventManager { get; private set; }

        private GameSceneManager _sceneManager;
        private EventSystemController _eventSystem;
        
        private void Awake()
        {
            InitializeCoreLogic();
        }
        private async UniTask InitializeCoreLogic()
        {
            const int delay = 3000;
            EventManager = new EventManager();
            _sceneManager = new GameSceneManager();
            await _sceneManager.LoadSceneAsync();
            await UniTask.WaitWhile(() => _eventSystem == null);
            UIController.Instance.Register();
            await UniTask.Delay(delay);
            
            // Hide Splash screen
            EventManager.Push(new HideWindowEvent(new SplashScreenWindowData()));
            EventManager.Push(new ShowWindowEvent(new MainMenuWindowData()));
        }
        public void RegisterEventSystem(in EventSystemController eventSystem)
        {
            _eventSystem = eventSystem;
        }
        private void OnDestroy()
        {
            UIController.Instance.Unregister();
        }
    }
}