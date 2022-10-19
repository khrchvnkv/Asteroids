using CoreLogic;
using UnityLogic.GamePlay.Pool;

namespace UnityLogic.GamePlay
{
    public readonly struct OnPlayerDiedEvent : IEvent
    {
        public readonly string Source;

        public OnPlayerDiedEvent(string source)
        {
            Source = source;
        }
    }
    public readonly struct OnEnemyKilledEvent : IEvent { }
    public readonly struct ReturnBulletToPoolEvent : IEvent
    {
        public readonly BulletController Bullet;

        public ReturnBulletToPoolEvent(BulletController bullet)
        {
            Bullet = bullet;
        }
    }
}
