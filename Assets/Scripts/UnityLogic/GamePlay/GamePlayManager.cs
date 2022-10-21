using CoreLogic.UI;
using UnityEngine;
using UnityLogic.GamePlay.Enemy;
using UnityLogic.GamePlay.Player;
using UnityLogic.GamePlay.Pool;
using UnityLogic.UI.FailMenu;
using UnityLogic.UI.GameHUD;

namespace UnityLogic.GamePlay
{
    public class GamePlayManager : Manager
    {
        [SerializeField] private GameObject gamePlayContainer;
        [SerializeField] private PlayerController playerController;

        [Header("Prefabs")] 
        [SerializeField] private BulletController bulletPrefab;

        [Header("Enemies")] 
        [SerializeField] private EnemySpawner enemySpawner;
        
        private Pool<BulletController> _bulletPool;
        
        public ScoreController ScoreController { get; private set; }
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
            ScoreController = new ScoreController();
        }
        protected override void Subscribe()
        {
            base.Subscribe();
            EventManager.Subscribe<ReturnBulletToPoolEvent>(this, OnBulletAddToPool);
            EventManager.Subscribe<OnPlayerDiedEvent>(this, OnPlayerDied);
        }
        protected override void Unsubscribe()
        {
            base.Unsubscribe();
            EventManager.Unsubscribe<ReturnBulletToPoolEvent>(this);
            EventManager.Unsubscribe<OnPlayerDiedEvent>(this);
        }
        public void StartGame()
        {
            playerController.ResetData();
            ScoreController.ResetScore();
            gamePlayContainer.SetActive(true);
            playerController.SetBehaviourActivity(true);
            enemySpawner.StartSpawn();
            EventManager.Push(new ShowWindowEvent(new GameHUD_Data(playerController.MovingBehaviour,
                playerController.LaserBehaviour)));
        }
        private void StopGame()
        {
            enemySpawner.StopSpawn();
            playerController.SetBehaviourActivity(false);
            gamePlayContainer.SetActive(false);
        }
        private void OnPlayerDied(in OnPlayerDiedEvent args)
        {
            StopGame();
            // Return to pool all obj
            EventManager.Push(new HideWindowEvent(new GameHUD_Data()));
            EventManager.Push(new ShowWindowEvent(new FailMenuWindowData(ScoreController.Score)));
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