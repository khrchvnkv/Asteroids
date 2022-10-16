using CoreLogic;
using UnityLogic.GamePlay.Pool;

namespace UnityLogic.GamePlay
{
    public struct ReturnBulletToPoolEvent : IEvent
    {
        public readonly BulletController Bullet;

        public ReturnBulletToPoolEvent(BulletController bullet)
        {
            Bullet = bullet;
        }
    }
}
