using UnityEngine;

namespace UnityLogic.GamePlay.Player
{
    public class MovingBehaviour : ICharacterBehaviour
    {
        private readonly IMovable _movable;
        private readonly Transform _transform;
        private readonly MovableCharacterData _movableData;

        private Vector3 _movingDirection;

        public MovingBehaviour(IMovable movable, Transform movingTransform)
        {
            _movable = movable;
            _transform = movingTransform;
            _movableData = movable.MovableData;
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
                // Acceerating
                _movingDirection += _transform.up * _movable.Vertical * Time.deltaTime;
            }

            _movingDirection = Vector3.ClampMagnitude(_movingDirection, _movableData.MaxSpeed);
            _transform.position += _movingDirection * _movableData.AcceleratingSpeed;
            CheckCameraOutOfBounds();

            // Rotate
            _transform.Rotate(Vector3.forward, _movable.Horizontal * -180.0f * Time.deltaTime);
        }
        private void CheckCameraOutOfBounds()
        {
            var newPosition = _transform.position;
            if (_transform.position.x > _movableData.MaxX || _transform.position.x < -_movableData.MaxX)
            {
                newPosition.x *= -1;
            }
            if (_transform.position.y > _movableData.MaxY || _transform.position.y < -_movableData.MaxY)
            {
                newPosition.y *= -1;
            }
            _transform.position = newPosition;
        }
    }
}