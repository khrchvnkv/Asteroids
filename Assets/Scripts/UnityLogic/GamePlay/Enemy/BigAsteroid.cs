namespace UnityLogic.GamePlay.Enemy
{
    public class BigAsteroid : EnemyBase
    {
        private GamePlayManager _gamePlayManager;
        
        public override FollowingType FollowType => FollowingType.RandomPoint;

        protected override void Awake()
        {
            base.Awake();
            _gamePlayManager = GameCore.Instance.GetManager<GamePlayManager>();
        }
        public override void Kill()
        {
            base.Kill();
            Divide();
        }
        private void Divide()
        {
            _gamePlayManager.SpawnSmallAsteroids(transform.position);
        }
    }
}