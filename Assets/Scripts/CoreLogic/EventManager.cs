using System;
using System.Collections.Generic;
using CoreLogic.UI;

namespace CoreLogic
{
    public interface IEvent{ }
    public sealed class EventManager
    {
        private class ContainerCollection
        {
            private readonly Dictionary<int, UnityEventHandler> _handlersMap = new Dictionary<int, UnityEventHandler>();

            public void Subscribe(in int senderHash, in UnityEventHandler eventHandler)
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
            public void Invoke(in IEventData eventData)
            {
                foreach (var handler in _handlersMap.Values)
                {
                    handler?.Invoke(eventData);
                }
            }
        }
        private readonly Dictionary<Type, ContainerCollection> _actionsContainer;

        public EventManager()
        {
            _actionsContainer = new Dictionary<Type, ContainerCollection>();
        }
        public void Subscribe<TEvent>(in object sender, in UnityEventHandler eventHandler) 
            where TEvent : struct, IEvent
        {
            var cacheType = typeof(TEvent);
            var hash = sender.GetHashCode();
            if (!_actionsContainer.ContainsKey(cacheType))
            {
                _actionsContainer[cacheType] = new ContainerCollection();
            }
            var containerCollection = _actionsContainer[cacheType];
            if (!containerCollection.IsSubscribed(hash))
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
                var containerCollection = _actionsContainer[cacheType];
                containerCollection.Unsubscribe(hash);
            }
        }
        public void Push<T>(IEventData eventData) where T: struct, IEvent
        {
            var cacheType = typeof(T);
            if (_actionsContainer.TryGetValue(cacheType, out var container))
            {
                var containerCollection = container;
                containerCollection.Invoke(eventData);
            }
        }
    }
}