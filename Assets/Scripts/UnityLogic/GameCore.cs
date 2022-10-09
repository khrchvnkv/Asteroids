using System;
using CoreLogic;
using CoreLogic.SceneLoader;
using CoreLogic.UI;
using Cysharp.Threading.Tasks;
using UnityLogic.UnityEventSystem;
using UnityEngine;
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
            EventManager = new EventManager();
            _sceneManager = new GameSceneManager();
            await _sceneManager.LoadSceneAsync();
            
            // Hide Splash screen
            EventManager.Push<HideWindowEvent>(new SplashScreenWindowData());
        }
        public void RegisterEventSystem(in EventSystemController eventSystem)
        {
            _eventSystem = eventSystem;
        }
    }
}