using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityLogic.GamePlay.Enemy;

namespace UnityLogic.GamePlay.Player
{
    public class LaserShootingBehaviour : ICharacterBehaviour
    {
        public const int MaxLaserShoots = 3;
        private const float ShootDuration = 0.1f;
        private const float ReloadTime = 4.5f;

        private readonly Transform _laserTransform;
        private readonly LineRenderer _lineRenderer;

        private int _availableShoots;
        private bool _isReloading;
        private float _reloadProgress;

        public event Action OnReloadStarted;
        public event Action OnReloadFinished;
        public event Action<int> OnLaserShootsCountChanged;
        public event Action<float> OnLaserReloadProgressChanged;

        public int AvailableShoots => _availableShoots;
        
        public LaserShootingBehaviour(Transform laserTransform, LineRenderer lineRenderer)
        {
            _laserTransform = laserTransform;
            _lineRenderer = lineRenderer;
        }
        void ICharacterBehaviour.Reset()
        {
            ClearLineRenderer();
            _availableShoots = MaxLaserShoots;
            _reloadProgress = 0.0f;
            _isReloading = false;
            UpdateLaserShootsCountView();
        }
        void ICharacterBehaviour.UpdateAction()
        {
            // Reload logic
            if (_isReloading)
            {
                _reloadProgress += (1.0f / ReloadTime) * Time.deltaTime;
                _reloadProgress = Mathf.Clamp01(_reloadProgress);
                OnLaserReloadProgressChanged?.Invoke(_reloadProgress);

                if (_reloadProgress == 1.0f)
                {
                    _isReloading = false;
                    _availableShoots = MaxLaserShoots;
                    OnReloadFinished?.Invoke();
                    UpdateLaserShootsCountView();
                }
            }
        }
        void ICharacterBehaviour.DoAction()
        {
            // Shoot logic
            if (_isReloading) return;
            if (_availableShoots > 0)
            {
                Shoot();
            }
        }
        private async UniTask Shoot()
        {
            _availableShoots--;
            UpdateLaserShootsCountView();
            if (_availableShoots == 0)
            {
                _reloadProgress = 0.0f;
                _isReloading = true;
                OnReloadStarted?.Invoke();
            }
            
            // Raycast 
            RaycastHit2D[] hits = Physics2D.RaycastAll(_laserTransform.position, _laserTransform.up);
            foreach (var hit in hits)
            {
                if (hit.collider.TryGetComponent(out EnemyBase enemy))
                {
                    enemy.Kill();
                }
            }

            _lineRenderer.enabled = true;
            await UniTask.Delay(TimeSpan.FromSeconds(ShootDuration), false);
            ClearLineRenderer();
        }
        private void ClearLineRenderer()
        {
            _lineRenderer.enabled = false;
        }
        private void UpdateLaserShootsCountView()
        {
            OnLaserShootsCountChanged?.Invoke(_availableShoots);
        }
    }
}