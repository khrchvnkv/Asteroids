using UnityEngine;
using InputActions;

namespace UnityLogic.GamePlay.Player
{
    public class PlayerController : MonoBehaviour, IMovable
    {
        [SerializeField] private Transform gunTransform;
        
        private PlayerInputActions _inputActions;
        private ICharacterBehaviour _movingBehaviour;
        private ICharacterBehaviour _shootingBehaviour;

        public ICharacterBehaviour MovingBehaviour => _movingBehaviour;
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }
        public MovableCharacterData MovableData { get; private set; }

        public void Initialize()
        {
            _inputActions = new PlayerInputActions();
            var gamePlayController = GameCore.Instance.GetController<GamePlayController>();
            MovableData = new MovableCharacterData()
            {
                AcceleratingSpeed = 0.35f,
                MaxSpeed = 7.0f,
                StoppingSpeed = 1.2f,
                RotateSpeed = -200.0f,
                Screen = gamePlayController.Screen
            };
            _movingBehaviour = new MovingBehaviour(this, transform);
            _shootingBehaviour = new ShootingBehaviour(gunTransform, gamePlayController);
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
        private void Shoot()
        {
            _shootingBehaviour.UpdateAction();
        }
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