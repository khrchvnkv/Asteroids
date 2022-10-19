namespace UnityLogic.GamePlay.Enemy
{
    public class HardEnemy : EnemyBase
    {
        public override FollowingType FollowType => FollowingType.PlayerFollower;

        public override void Kill()
        {
            base.Kill();
            Divide();
        }
        private void Divide()
        {
        
        }
    }
}