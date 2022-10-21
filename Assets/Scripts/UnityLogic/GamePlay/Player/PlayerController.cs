using CoreLogic;
using UnityEngine;
using InputActions;
using UnityLogic.GamePlay.Enemy;

namespace UnityLogic.GamePlay.Player
{
    public class PlayerController : MonoBehaviour, IMovable
    {
        [SerializeField] private Transform gunTransform;
        [SerializeField] private LineRenderer lineRenderer;
        
        private PlayerInputActions _inputActions;
        private ICharacterBehaviour _movingBehaviour;
        private ICharacterBehaviour _gunShootingBehaviour;
        private ICharacterBehaviour _laserShootingBehaviour;
        
        private EventManager _eventManager;
        private new Transform transform;

        public MovingBehaviour MovingBehaviour => _movingBehaviour as MovingBehaviour;
        public LaserShootingBehaviour LaserBehaviour => _laserShootingBehaviour as LaserShootingBehaviour;
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }
        public MovableCharacterData MovableData { get; private set; }

        public void Initialize()
        {
            transform = GetComponent<Transform>();
            _inputActions = new PlayerInputActions();
            var gamePlayController = GameCore.Instance.GetManager<GamePlayManager>();
            MovableData = new MovableCharacterData()
            {
                AcceleratingSpeed = 0.35f,
                MaxSpeed = 7.0f,
                StoppingSpeed = 1.2f,
                RotateSpeed = -200.0f,
                Screen = gamePlayController.Screen
            };
            _movingBehaviour = new MovingBehaviour(this, transform);
            _gunShootingBehaviour = new GunShootingBehaviour(gunTransform, gamePlayController);
            _laserShootingBehaviour = new LaserShootingBehaviour(gunTransform, lineRenderer);
            _inputActions.Player.GunShoot.performed += context => GunShoot();
            _inputActions.Player.LaserShoot.performed += context => LaserShoot();
            _eventManager = GameCore.Instance.EventManager;
        }
        public void ResetData()
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            _movingBehaviour.Reset();
            _gunShootingBehaviour.Reset();
            _laserShootingBehaviour.Reset();
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
        private void GunShoot()
        {
            _gunShootingBehaviour.DoAction();
        }
        private void LaserShoot()
        {
            _laserShootingBehaviour.DoAction();
        }
        private void Update()
        {
            if (_inputActions.Player.enabled)
            {
                Vector2 direction = _inputActions.Player.Move.ReadValue<Vector2>();
                Horizontal = direction.x;
                Vertical = direction.y;
                _movingBehaviour.UpdateAction();
                _laserShootingBehaviour.UpdateAction();
            }
        }
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent(out EnemyBase enemy))
            {
                enemy.Kill();
                _eventManager.Push(new OnPlayerDiedEvent(enemy.gameObject.name));
            }
        }
    }
}