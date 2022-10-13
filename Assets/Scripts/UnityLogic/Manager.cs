using CoreLogic;
using UnityEngine;

namespace UnityLogic
{
    public abstract class Manager : MonoBehaviour
    {
        protected EventManager EventManager => GameCore.Instance.EventManager;
        
        private void Awake()
        {
            Initialize();
        }
        protected virtual void Initialize()
        {
            GameCore.Instance.RegisterManager(this);
        }
        public virtual void Unregister() { }
    }
}