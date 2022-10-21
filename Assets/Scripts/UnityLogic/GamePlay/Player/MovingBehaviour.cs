using System;
using UnityEngine;

namespace UnityLogic.GamePlay.Player
{
    public class MovingBehaviour : ICharacterBehaviour
    {
        private readonly IMovable _movable;
        private readonly Transform _transform;
        private readonly MovableCharacterData _movableData;

        private Vector3 _movingDirection;
        private MovableInfo _movableInfo;

        public event Action<MovableInfo> OnPositionChanged;

        public struct MovableInfo
        {
            public float X;
            public float Y;
            public float Rotation;
            public float Speed;
        }

        public MovingBehaviour(IMovable movable, Transform movingTransform)
        {
            _movable = movable;
            _transform = movingTransform;
            _movableData = movable.MovableData;
            _movableInfo = new MovableInfo();
        }
        void ICharacterBehaviour.Reset()
        {
            _movingDirection = Vector3.zero;
        }
        void ICharacterBehaviour.UpdateAction()
        {
            // Move
            if (_movable.Vertical == 0.0f)
            {
                // Stopping
                _movingDirection = Vector3.Lerp(_movingDirection, Vector3.zero, _movableData.StoppingSpeed * Time.deltaTime);
            }
            else
            {
                // Accelerating
                _movingDirection += _transform.up * _movableData.AcceleratingSpeed * _movable.Vertical;
                _movingDirection = Vector3.ClampMagnitude(_movingDirection, _movableData.MaxSpeed);
            }

            _transform.position += _movingDirection * Time.deltaTime;
            CheckCameraOutOfBounds();

            // Rotate
            _transform.Rotate(Vector3.forward, _movable.Horizontal * _movableData.RotateSpeed * Time.deltaTime);
            UpdateInfo();
        }
        void ICharacterBehaviour.DoAction() { }

        private void UpdateInfo()
        {
            var position = _transform.position;
            _movableInfo.X = position.x;
            _movableInfo.Y = position.y;
            _movableInfo.Rotation = _transform.rotation.eulerAngles.z;
            _movableInfo.Speed = _movingDirection.magnitude;
            OnPositionChanged?.Invoke(_movableInfo);
        }
        private void CheckCameraOutOfBounds()
        {
            var newPosition = _transform.position;
            if (_transform.position.x > _movableData.Screen.MaxX || _transform.position.x < -_movableData.Screen.MaxX)
            {
                newPosition.x *= -1;
            }
            if (_transform.position.y > _movableData.Screen.MaxY || _transform.position.y < -_movableData.Screen.MaxY)
            {
                newPosition.y *= -1;
            }
            _transform.position = newPosition;
        }
    }
}