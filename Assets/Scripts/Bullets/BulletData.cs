using UnityEngine;

namespace ShootEmUp
{
    public struct BulletData
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public Color Color;
        public PhysicsLayer PhysicsLayer;
        public int Damage;
        public bool IsPlayer;
    }
}
