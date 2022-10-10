using System.Collections.Generic;
using System.Linq;
using CoreLogic;
using CoreLogic.UI;

namespace UnityLogic.UI
{
    public class UIController : MonoSingleton<UIController>
    {
        private EventManager EventManager => GameCore.Instance.EventManager;

        private List<IWindow> _windows;

        public void Register()
        {
            _windows = new List<IWindow>();
            _windows = GetComponentsInChildren<IWindow>().ToList();
           
            Subscribe();
        }
        private void Subscribe()
        {
            EventManager.Subscribe<ShowWindowEvent>(this, ShowWindow);
            EventManager.Subscribe<HideWindowEvent>(this, HideWindow);
        }

        public void Unregister()
        {
            Unsubscribe();
        }
        private void Unsubscribe()
        {
            EventManager.Unsubscribe<ShowWindowEvent>(this);
            EventManager.Unsubscribe<HideWindowEvent>(this);
        }
        private void ShowWindow(in ShowWindowEvent eventData)
        {
            foreach (var window in _windows)
            {
                if (window.WindowType == eventData.WindowData.GetType())
                {
                    window.Show(eventData.WindowData);
                }
            }
        }
        private void HideWindow(in HideWindowEvent eventData)
        {
            if (eventData is IWindowEvent windowData)
            {
                foreach (var window in _windows)
                {
                    if (window.WindowType == windowData.WindowData.GetType())
                    {
                        window.Hide();
                    }
                }
            }
        }
    }
}