using CoreLogic.UI;
using UnityEngine;
using UnityLogic.GamePlay.Enemy;
using UnityLogic.GamePlay.Player;
using UnityLogic.GamePlay.Pool;
using UnityLogic.UI.GameHUD;

namespace UnityLogic.GamePlay
{
    public class GamePlayController : Controller
    {
        [SerializeField] private GameObject gamePlayController;
        [SerializeField] private PlayerController playerController;

        [Header("Prefabs")] 
        [SerializeField] private BulletController bulletPrefab;

        [Header("Enemies")] 
        [SerializeField] private EnemySpawner enemySpawner;
        
        private Pool<BulletController> _bulletPool;
        
        public ScreenSizeData Screen { get; private set; }
        
        public struct ScreenSizeData
        {
            public float MaxX;
            public float MaxY;
        }
        
        protected override void Inject()
        {
            base.Inject();
            _bulletPool = new Pool<BulletController>(bulletPrefab);
            Screen = new ScreenSizeData()
            {
                MaxX = 9.0f,
                MaxY = 5.0f
            };
            playerController.Initialize();
        }
        protected override void Subscribe()
        {
            base.Subscribe();
            EventManager.Subscribe<ReturnBulletToPoolEvent>(this, OnBulletAddToPool);
        }
        protected override void Unsubscribe()
        {
            base.Unsubscribe();
            EventManager.Unsubscribe<ReturnBulletToPoolEvent>(this);
        }
        public void StartGame()
        {
            gamePlayController.SetActive(true);
            playerController.SetBehaviourActivity(true);
            enemySpawner.StartSpawn();
            EventManager.Push(new ShowWindowEvent(new GameHUD_Data(playerController.MovingBehaviour as MovingBehaviour)));
        }
        public void StopGame()
        {
            playerController.SetBehaviourActivity(false);
            enemySpawner.StopSpawn();
        }
        private void OnBulletAddToPool(in ReturnBulletToPoolEvent args)
        {
            _bulletPool.Add(args.Bullet);
        }
        public BulletController GetBulletFromPool()
        {
            return _bulletPool.Get();
        }
        public Transform GetPlayerTransform()
        {
            return playerController.transform;
        }
    }
}