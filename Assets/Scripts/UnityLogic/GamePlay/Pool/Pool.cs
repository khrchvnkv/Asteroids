using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityLogic.GamePlay.Pool
{
    public interface IPoolObject
    {
        void DeleteToPool();
    }
    public sealed class Pool<T> where T : MonoBehaviour, IPoolObject
    {
        private readonly List<T> _poolList;
        private readonly T _poolPrefab;

        public Pool(T prefab)
        {
            _poolList = new List<T>();
            _poolPrefab = prefab;
        }

        public void Add(T poolObject)
        {
            _poolList.Add(poolObject);
            poolObject.gameObject.SetActive(false);
        }
        public T Get()
        {
            if (_poolList.Count == 0)
            {
                var tObject = Object.Instantiate(_poolPrefab, Vector3.zero, Quaternion.identity) as T;
                tObject.gameObject.SetActive(false);
                return tObject;
            }
            else
            {
                var result = _poolList[0];
                _poolList.RemoveAt(0);
                return result;
            }
        }
    }
}