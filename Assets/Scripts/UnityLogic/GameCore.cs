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

        private readonly Dictionary<Type, Manager> _managers = new Dictionary<Type, Manager>();

        public EventManager EventManager { get; private set; }

        private SceneLoader _sceneLoader;
        private EventSystemController _eventSystem;
        
        private void Awake()
        {
            InitializeCoreLogic();
        }
        private async UniTask InitializeCoreLogic()
        {
            const int delay = 3000;
            EventManager = new EventManager();
            _sceneLoader = new SceneLoader();
            await _sceneLoader.LoadSceneAsync();
            await UniTask.WaitWhile(() => _eventSystem == null);
            await UniTask.Delay(delay);
            
            // Hide Splash screen
            EventManager.Push(new HideWindowEvent(new SplashScreenWindowData()));
            EventManager.Push(new ShowWindowEvent(new MainMenuWindowData()));
        }
        public void RegisterManager<T>(T manager) where T : Manager
        {
            var type = manager.GetType();
            if (_managers.ContainsKey(type))
            {
                Debug.LogWarning($"There are several managers of type {type} on the stage");
            }
            else
            {
                _managers[type] = manager;
            }
        }
        public T GetManager<T>() where T : Manager
        {
            var managerType = typeof(T);
            if (_managers.TryGetValue(managerType, out var result))
            {
                return (T)result;
            }

            throw new NullReferenceException($"Error: {managerType} is not contains in dictionary");
        }
        public void RegisterEventSystem(in EventSystemController eventSystem)
        {
            _eventSystem = eventSystem;
        }
    }
}