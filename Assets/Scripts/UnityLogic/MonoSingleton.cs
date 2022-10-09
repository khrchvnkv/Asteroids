using UnityEngine;

namespace UnityLogic
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        #region Lazy Initialization
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject($"[{typeof(T)}]").AddComponent<T>();
                    DontDestroyOnLoad(_instance);
                }
                return _instance;
            }
        }
        #endregion
    }
}