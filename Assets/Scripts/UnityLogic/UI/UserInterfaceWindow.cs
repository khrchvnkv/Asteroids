using System;
using CoreLogic.UI;
using UnityEngine;

namespace UnityLogic.UI
{
    public interface IWindow
    {
        Type WindowType { get; }
        void Show(IWindowData data);
        void Hide();
    }
    public abstract class UserInterfaceWindow<TData> : MonoBehaviour, IWindow
        where TData : IWindowData
    {
        [SerializeField] private GameObject container;

        protected TData WindowData;
        public Type WindowType => typeof(TData);
        
        private void Awake()
        {
            InitWindowData();
        }
        protected abstract void InitWindowData();
        public virtual void Show(IWindowData data)
        {
            WindowData = (TData)data;
            container.SetActive(true);
        }
        public virtual void Hide()
        {
            container.SetActive(false);
        }
    }
}