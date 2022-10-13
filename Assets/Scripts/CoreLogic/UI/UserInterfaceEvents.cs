namespace CoreLogic.UI
{
    public interface IWindowEvent : IEvent
    {
        IWindowData WindowData { get; }
    }
    public interface IWindowData { }
    
    public readonly struct ShowWindowEvent : IWindowEvent
    {
        public IWindowData WindowData { get; }
        
        public ShowWindowEvent(IWindowData window)
        {
            WindowData = window;
        }
    }
    public readonly struct HideWindowEvent : IWindowEvent
    {
        public IWindowData WindowData { get; }
        
        public HideWindowEvent(IWindowData window)
        {
            WindowData = window;
        }
    }
}