using System;

namespace CoreLogic.UI
{
    public interface IEventData { }
    public interface IWindowData : IEventData { }
    public readonly struct ShowWindowEvent : IEvent
    {
        public readonly IWindowData Window;
        
        public ShowWindowEvent(IWindowData window)
        {
            Window = window;
        }
    }
    public readonly struct HideWindowEvent : IEvent
    {
        public readonly IWindowData Window;
        
        public HideWindowEvent(IWindowData window)
        {
            Window = window;
        }
    }
}