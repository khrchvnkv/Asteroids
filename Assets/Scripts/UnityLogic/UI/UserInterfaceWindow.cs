using System;
using CoreLogic;
using CoreLogic.UI;
using UnityEngine;

namespace UnityLogic.UI
{
    public interface IWindow
    {
        Type WindowType { get; }
        void ShowWindow(IWindowData data);
        void HideWindow();
    }
    public abstract class UserInterfaceWindow<TData> : MonoBehaviour, IWindow
        where TData : IWindowData
    {
        [SerializeField] private GameObject container;

        protected TData WindowData;
        public Type WindowType => typeof(TData);
        protected EventManager EventManager => GameCore.Instance.EventManager;
        
        private void Awake()
        {
            Inject();
        }
        protected virtual void Inject() { }
        public virtual void ShowWindow(IWindowData data)
        {
            WindowData = (TData)data;
            container.SetActive(true);
        }
        public virtual void HideWindow()
        {
            container.SetActive(false);
        }
    }
}