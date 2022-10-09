namespace CoreLogic
{
    public delegate void EventHandler<T>(in T eventData) where T : IEvent;
}