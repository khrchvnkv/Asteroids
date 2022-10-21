using System;
using System.Collections.Generic;

namespace CoreLogic
{
    public interface IEvent{ }
    public sealed class EventManager
    {
        private interface IContainerCollection
        {
            void Unsubscribe(in int senderHash);
            bool IsSubscribed(in int senderHash);
        }
        private class ContainerCollection<TEvent> : IContainerCollection where TEvent : struct, IEvent
        {
            private readonly Dictionary<int, UnityEventHandler<TEvent>> _handlersMap;

            public ContainerCollection()
            {
                _handlersMap = new Dictionary<int, UnityEventHandler<TEvent>>();
            }
            public void Subscribe(in int senderHash, in UnityEventHandler<TEvent> eventHandler)
            {
                _handlersMap[senderHash] = eventHandler;
            }
            public void Unsubscribe(in int senderHash)
            {
                _handlersMap.Remove(senderHash);
            }
            public bool IsSubscribed(in int senderHash)
            {
                return _handlersMap.ContainsKey(senderHash);
            }
            public void Invoke(in TEvent eventData)
            {
                var handlers = new List<UnityEventHandler<TEvent>>(_handlersMap.Values);
                foreach (var handler in handlers)
                {
                    handler?.Invoke(eventData);
                }
            }
        }
        private readonly Dictionary<Type, IContainerCollection> _actionsContainer;

        public EventManager()
        {
            _actionsContainer = new Dictionary<Type, IContainerCollection>();
        }
        public void Subscribe<TEvent>(in object sender, in UnityEventHandler<TEvent> eventHandler) 
            where TEvent : struct, IEvent
        {
            var cacheType = typeof(TEvent);
            var hash = sender.GetHashCode();
            if (!_actionsContainer.ContainsKey(cacheType))
            {
                _actionsContainer[cacheType] = new ContainerCollection<TEvent>();
            }
            if (!_actionsContainer[cacheType].IsSubscribed(hash) &&
                _actionsContainer[cacheType] is ContainerCollection<TEvent> containerCollection)
            {
                containerCollection.Subscribe(hash, eventHandler);
            }
        }
        public void Unsubscribe<TEvent>(in object sender) 
            where TEvent : struct, IEvent
        {
            var cacheType = typeof(TEvent);
            if (_actionsContainer.ContainsKey(cacheType))
            {
                var hash = sender.GetHashCode();
                _actionsContainer[cacheType].Unsubscribe(hash);
            }
        }
        public void Push<T>(T eventData) where T: struct, IEvent
        {
            var cacheType = typeof(T);
            if (_actionsContainer.TryGetValue(cacheType, out var container) &&
                container is ContainerCollection<T> containerCollection)
            {
                containerCollection.Invoke(eventData);
            }
        }
    }
}