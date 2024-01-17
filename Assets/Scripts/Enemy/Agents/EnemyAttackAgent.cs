using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        public float Countdown { get => _countdown; }

        public WeaponComponent WeaponComponent { get => _weaponComponent; }

        public BulletConfig BulletConfig { get => _bulletConfig; }

        public GameObject Target { get => _target; set { _target = value; } }

        public bool IsReached => _moveAgent.IsReached;

        [SerializeField]
        private WeaponComponent _weaponComponent;

        [SerializeField]
        private EnemyMoveAgent _moveAgent;

        [SerializeField]
        private float _countdown;

        [SerializeField]
        private BulletConfig _bulletConfig;

        private GameObject _target;
    }
}