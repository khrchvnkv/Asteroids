using System;
using CoreLogic;
using UnityEngine;
using UnityLogic.GamePlay.Pool;

namespace UnityLogic.GamePlay.Enemy
{
    public abstract class EnemyBase : MonoBehaviour, IPoolObject
    {
        public enum FollowingType
        {
            RandomPoint,
            PlayerFollower
        }
        
        [SerializeField] private float movingSpeed, acceleratingSpeed;

        protected new Transform transform;
        private EventManager _eventManager;
        private Transform _target;
        private Vector3 _movingDirection;
        
        private event Action<EnemyBase> ReturnToPoolCallback;

        public abstract FollowingType FollowType { get; }
        
        protected virtual void Awake()
        {
            transform = GetComponent<Transform>();
            _eventManager = GameCore.Instance.EventManager;
        }
        public void SetTarget(in Transform target, in Action<EnemyBase> returnToPool)
        {
            _target = target;
            ReturnToPoolCallback = returnToPool;
        }
        private void Update()
        {
            var position = transform.position;
            var direction = (_target.position - position).normalized;
            _movingDirection += direction * acceleratingSpeed;
            _movingDirection = Vector3.ClampMagnitude(_movingDirection, movingSpeed);
            position += _movingDirection * Time.deltaTime;
            transform.position = position;

            if (Vector3.Distance(position, _target.position) <= 0.1f)
            {
                DeleteToPool();
            }
        }
        public virtual void Kill()
        {
            DeleteToPool();
        }
        public void DeleteToPool()
        {
            ReturnToPoolCallback?.Invoke(this);
        }
    }
}