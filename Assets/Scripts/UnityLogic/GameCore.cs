using System;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;

namespace UnityLogic
{
    public class GameCore : MonoBehaviour, IServiceProvider
    {
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
        private void Awake()
        {
            
        }
        object IServiceProvider.GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }
}