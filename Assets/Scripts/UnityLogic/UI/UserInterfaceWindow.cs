using CoreLogic;
using CoreLogic.UI;
using UnityEngine;

namespace UnityLogic.UI
{
    public interface IWindow
    {
        IWindowData WindowData { get; }
        void Show();
        void Hide();
    }
    public abstract class UserInterfaceWindow<TData> : MonoBehaviour, IWindow
        where TData : IWindowData
    {
        [SerializeField] private GameObject container;

        protected EventManager EventManager;
        protected TData _windowData;

        public IWindowData WindowData => _windowData;
        
        private void Awake()
        {
            EventManager = GameCore.Instance.EventManager;
            InitWindowData();
        }
        protected abstract void InitWindowData();
        public virtual void Show()
        {
            container.SetActive(true);
        }
        public virtual void Hide()
        {
            container.SetActive(false);
        }
    }
}