namespace UnityLogic.GamePlay.Enemy
{
    public class SimpleEnemy : EnemyBase
    {
        public override FollowingType FollowType => FollowingType.RandomPoint;
    }
}