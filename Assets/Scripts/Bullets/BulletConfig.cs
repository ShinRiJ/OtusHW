using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "BulletConfig",
        menuName = "Bullets/New BulletConfig"
    )]

    //ѕрокл€тый класс
    //ѕри измененении названи€ полей этого класса пули станов€тьс€ невидимыми

    public sealed class BulletConfig : ScriptableObject
    {
        [SerializeField]
        public PhysicsLayer physicsLayer;

        [SerializeField]
        public Color color;

        [SerializeField]
        public int damage;

        [SerializeField]
        public float speed;
    }
}