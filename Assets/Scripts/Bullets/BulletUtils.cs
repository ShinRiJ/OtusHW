using UnityEngine;

namespace ShootEmUp
{
    internal static class BulletUtils
    {
        internal static void TryDealDamage(Bullet bullet, GameObject other)
        {
            if(other.TryGetComponent(out UnitFacade unitFacade))
            {
                if (bullet.IsPlayer == unitFacade.TeamComponent.IsPlayer)
                {
                    return;
                }

                unitFacade.HitPointsComponent.TakeDamage(bullet.Damage);
            }
        }
    }
}