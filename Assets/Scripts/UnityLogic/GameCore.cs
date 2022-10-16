using System;
using System.Collections.Generic;
using CoreLogic;
using CoreLogic.SceneLoader;
using CoreLogic.UI;
using Cysharp.Threading.Tasks;
using UnityLogic.UnityEventSystem;
using UnityEngine;
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

        private readonly Dictionary<Type, Controller> _controllers = new Dictionary<Type, Controller>();

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
            await UniTask.Delay(delay);
            
            // Hide Splash screen
            EventManager.Push(new HideWindowEvent(new SplashScreenWindowData()));
            EventManager.Push(new ShowWindowEvent(new MainMenuWindowData()));
        }
        public void RegisterController<T>(T controller) where T : Controller
        {
            var type = controller.GetType();
            if (_controllers.ContainsKey(type))
            {
                Debug.LogWarning($"There are several managers of type {type} on the stage");
            }
            else
            {
                _controllers[type] = controller;
            }
        }
        public T GetController<T>() where T : Controller
        {
            var controllerType = typeof(T);
            if (_controllers.TryGetValue(controllerType, out var result))
            {
                return (T)result;
            }

            throw new NullReferenceException($"Error: {controllerType} is not contains in dictionary");
        }
        public void RegisterEventSystem(in EventSystemController eventSystem)
        {
            _eventSystem = eventSystem;
        }
    }
}