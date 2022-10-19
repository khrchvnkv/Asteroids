using CoreLogic;
using UnityEngine;
using UnityLogic.GamePlay.Enemy;

namespace UnityLogic.GamePlay.Pool
{
    public class BulletController : MonoBehaviour, IPoolObject
    {
        [SerializeField] private float movingSpeed;
        
        private new Transform transform;
        private GamePlayManager.ScreenSizeData _screenSizeData;
        
        private EventManager EventManager => GameCore.Instance.EventManager;

        private void Awake()
        {
            transform = GetComponent<Transform>();
            _screenSizeData = GameCore.Instance.GetManager<GamePlayManager>().Screen;
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
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent(out EnemyBase enemy))
            {
                DeleteToPool();
                enemy.Kill();
                EventManager.Push(new OnEnemyKilledEvent());
            }
        }
        public void DeleteToPool()
        {
            EventManager.Push(new ReturnBulletToPoolEvent(this));
        }
    }
}