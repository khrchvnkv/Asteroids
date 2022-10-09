using CoreLogic;
using CoreLogic.UI;
using UnityEngine;

namespace UnityLogic.UI
{
    public abstract class UserInterfaceWindow<TData> : MonoBehaviour 
        where TData : IWindowData
    {
        [SerializeField] private GameObject container;

        protected EventManager EventManager;
        protected TData _windowData;

        private void Awake()
        {
            EventManager = GameCore.Instance.EventManager;
        }
        private void OnEnable()
        {
            Subscribe();
        }
        private void OnDisable()
        {
            Unsubscribe();
        }
        protected virtual void Subscribe()
        {
            EventManager.Subscribe(this, (in ShowWindowEvent data) => ShowWindow(_windowData));
            EventManager.Subscribe(this, (in HideWindowEvent data) => HideWindow(_windowData));
        }
        protected virtual void Unsubscribe()
        {
            EventManager.Unsubscribe<ShowWindowEvent>(this);
            EventManager.Unsubscribe<HideWindowEvent>(this);
        }
        private void ShowWindow(in TData data)
        {
            if (data.GetType() == _windowData.GetType())
            {
                container.SetActive(true);
            }
        }
        private void HideWindow(in TData data)
        {
            if (data.GetType() == _windowData.GetType())
            {
                container.SetActive(false);
            }
        }
    }
}