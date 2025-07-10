using UnityEngine;

namespace ShootEmUp
{
    internal static class BulletUtils
    {
        internal static void TryDealDamage(Bullet bullet, GameObject other)
        {
            if (!other.TryGetComponent(out TeamComponent team))
            {
                return;
            }

            if (bullet.IsPlayer == team.IsPlayer)
            {
                return;
            }

            if (other.TryGetComponent(out IHitPointDamageRecieve hitPoints))
            {
                hitPoints.TakeDamage(bullet.Damage);
            }
        }
    }
}