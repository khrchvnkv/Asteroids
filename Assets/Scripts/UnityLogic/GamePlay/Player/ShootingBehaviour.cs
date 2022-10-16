using UnityEngine;

namespace UnityLogic.GamePlay.Player
{
    public class ShootingBehaviour : ICharacterBehaviour
    {
        private readonly Transform parentTransform;
        private readonly GamePlayController gamePlayController;

        public ShootingBehaviour(Transform transform, GamePlayController controller)
        {
            parentTransform = transform;
            gamePlayController = controller;
        }
        public void UpdateAction()
        {
            // Instantiate bullet
            var bullet = gamePlayController.GetBulletFromPool();
            var bulletTransform = bullet.transform;
            bulletTransform.position = parentTransform.position;
            bulletTransform.rotation = parentTransform.rotation;
            bullet.gameObject.SetActive(true);
        }
    }
}