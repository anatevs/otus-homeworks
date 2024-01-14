using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        [SerializeField]
        private WeaponComponent _weaponComponent;

        [SerializeField]
        private EnemyMoveAgent _moveAgent;

        [SerializeField]
        private float _countdown;

        [SerializeField]
        private BulletConfig _bulletConfig;

        private BulletSystem _bulletSystem;

        private GameObject _target;

        private float _currentTime;

        public void SetBulletSystem(BulletSystem bulletSystem)
        {
            _bulletSystem = bulletSystem;
        }

        public void SetTarget(GameObject target)
        {
            _target = target;
        }

        public void FixedUpdateAttack()
        {
            if (_moveAgent == null)
            {
                Debug.Log("there is no EnemyMoveAgent assigned in EnemyAttackAgent!");
                return;
            }

            if (!_moveAgent.IsReached)
            {
                return;
            }

            if ((_currentTime -= Time.fixedDeltaTime) <= 0)
            {
                Fire();
                ResetTimer();
            }
        }

        private void ResetTimer()
        {
            _currentTime = _countdown;
        }

        private void Fire()
        {
            if (_weaponComponent != null && _target != null)
            {
                var startPosition = _weaponComponent.Position;
                var vector = (Vector2)_target.transform.position - startPosition;
                var direction = vector.normalized;
                _bulletSystem.FlyBulletByArgs(new BulletArgs
                {
                    isPlayer = false,
                    physicsLayer = (int)_bulletConfig.physicsLayer,
                    color = _bulletConfig.color,
                    damage = _bulletConfig.damage,
                    position = startPosition,
                    velocity = direction * _bulletConfig.speed
                });
            }
        }
    }
}