using CoreLogic;
using UnityEngine;

namespace UnityLogic.GamePlay.Pool
{
    public class BulletController : MonoBehaviour, IPoolObject
    {
        [SerializeField] private float movingSpeed;
        
        private new Transform transform;
        private GamePlayController.ScreenSizeData _screenSizeData;
        
        private EventManager EventManager => GameCore.Instance.EventManager;

        private void Awake()
        {
            transform = GetComponent<Transform>();
            _screenSizeData = GameCore.Instance.GetController<GamePlayController>().Screen;
        }
        private void Update()
        {
            transform.position += transform.up * movingSpeed * Time.deltaTime;

            if (transform.position.x > _screenSizeData.MaxX || transform.position.x < -_screenSizeData.MaxX ||
                transform.position.y > _screenSizeData.MaxY || transform.position.y < -_screenSizeData.MaxY)
            {
                DeleteToPool();
            }
        }
        public void DeleteToPool()
        {
            EventManager.Push(new ReturnBulletToPoolEvent(this));
        }
    }
}