using UnityEngine;
using InputActions;

namespace UnityLogic.GamePlay.Player
{
    public class PlayerController : MonoBehaviour, IMovable
    {
        private PlayerInputActions _inputActions;

        public MovingBehaviour MovingBehaviour { get; private set; }
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }
        public MovableCharacterData MovableData { get; private set; }

        public void Initialize()
        {
            _inputActions = new PlayerInputActions();
            MovableData = new MovableCharacterData()
            {
                AcceleratingSpeed = 0.25f,
                MaxSpeed = 1.0f,
                StoppingSpeed = 1.0f,
                MaxX = 9.0f,
                MaxY = 4.5f
            };
            MovingBehaviour = new MovingBehaviour(this, transform);
            _inputActions.Player.Shoot.performed += context => Shoot();
        }
        public void SetBehaviourActivity(in bool isActivity)
        {
            if (isActivity)
            {
                _inputActions.Enable();
            }
            else
            {
                _inputActions.Disable();
            }
        }
        private void Shoot() { }
        private void Update()
        {
            if (_inputActions.Player.enabled)
            {
                Vector2 direction = _inputActions.Player.Move.ReadValue<Vector2>();
                Horizontal = direction.x;
                Vertical = direction.y;
                MovingBehaviour.UpdateAction();
            }
        }
    }
}