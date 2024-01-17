using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        public Vector2 Position
        {
            get => _firePoint.position;
        }

        public Quaternion Rotation
        {
            get => _firePoint.rotation;
        }

        [SerializeField]
        private Transform _firePoint;
    }
}