using System;
using System.Collections.Generic;

namespace CoreLogic
{
    public interface IEvent{ }

    public sealed class EventManager
    {
        private interface IContainerCollection { }
        private class ContainerCollection<T> : IContainerCollection where T : struct, IEvent
        {
            private readonly Dictionary<int, EventHandler<T>> _handlersMap = new Dictionary<int, EventHandler<T>>();
            private IContainerCollection _containerCollectionImplementation;

            public void Subscribe(in int senderHash, in EventHandler<T> eventHandler)
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
            public void Invoke(in T eventData)
            {
                foreach (var handler in _handlersMap.Values)
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
        public void Subscribe<T>(in object sender, in EventHandler<T> eventHandler) where T : struct, IEvent
        {
            var cacheType = typeof(T);
            var hash = sender.GetHashCode();
            if (!_actionsContainer.ContainsKey(cacheType))
            {
                _actionsContainer[cacheType] = new ContainerCollection<T>();
            }
            var containerCollection = (ContainerCollection<T>)_actionsContainer[cacheType];
            if (!containerCollection.IsSubscribed(hash))
            {
                containerCollection.Subscribe(hash, eventHandler);
            }
        }
        public void Unsubscribe<T>(in object sender) where T : struct, IEvent
        {
            var cacheType = typeof(T);
            if (_actionsContainer.ContainsKey(cacheType))
            {
                var hash = sender.GetHashCode();
                var containerCollection = (ContainerCollection<T>)_actionsContainer[cacheType];
                containerCollection.Unsubscribe(hash);
            }
        }
        public void Push<T>(T eventData) where T: struct, IEvent
        {
            var cacheType = typeof(T);
            if (_actionsContainer.TryGetValue(cacheType, out var container))
            {
                var containerCollection = (ContainerCollection<T>)container;
                containerCollection.Invoke(eventData);
            }
        }
    }
}