using UnityEngine;

namespace UnityLogic
{
    public abstract class Manager : MonoBehaviour
    {
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