using UnityEngine;

namespace UnityLogic.GamePlay.Player
{
    public class GunShootingBehaviour : ICharacterBehaviour
    {
        private readonly Transform parentTransform;
        private readonly GamePlayManager _gamePlayManager;

        public GunShootingBehaviour(Transform transform, GamePlayManager manager)
        {
            parentTransform = transform;
            _gamePlayManager = manager;
        }
        void ICharacterBehaviour.Reset() { }
        void ICharacterBehaviour.UpdateAction() { }
        void ICharacterBehaviour.DoAction()
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