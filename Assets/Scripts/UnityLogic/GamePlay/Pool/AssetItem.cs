using System;
using UnityEngine;

namespace UnityLogic.GamePlay.Pool
{
    public class AssetItem : MonoBehaviour
    {
        [SerializeField] private string id;

        private IPoolObject _poolObject;

        private void Awake()
        {
            if (!gameObject.TryGetComponent(out _poolObject))
            {
                throw new Exception($"{gameObject.name} doesn't contains component of type {typeof(IPoolObject)}");
            }
        }
    }
}