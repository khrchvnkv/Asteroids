namespace CoreLogic
{
    public delegate void UnityEventHandler<T>(in T eventData) where T : IEvent;
}