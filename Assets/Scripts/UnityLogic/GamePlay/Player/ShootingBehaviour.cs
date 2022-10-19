using UnityEngine;

namespace UnityLogic.GamePlay.Player
{
    public class ShootingBehaviour : ICharacterBehaviour
    {
        private readonly Transform parentTransform;
        private readonly GamePlayManager _gamePlayManager;

        public ShootingBehaviour(Transform transform, GamePlayManager manager)
        {
            parentTransform = transform;
            _gamePlayManager = manager;
        }
        void ICharacterBehaviour.Reset()
        {
            
        }
        void ICharacterBehaviour.UpdateAction()
        {
            // Instantiate bullet
            var bullet = _gamePlayManager.GetBulletFromPool();
            var bulletTransform = bullet.transform;
            bulletTransform.position = parentTransform.position;
            bulletTransform.rotation = parentTransform.rotation;
            bullet.gameObject.SetActive(true);
        }
    }
}