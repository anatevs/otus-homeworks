using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackController :
        IFixedUpdate,
        IPausedFixedUpdate
    {
        private readonly EnemyAttackAgent _enemyAttackData;

        private readonly BulletSystem _bulletSystem;

        private float _currentTime;

        public EnemyAttackController(EnemyAttackAgent enemyAttackData, BulletSystem bulletSystem)
        {
            _enemyAttackData = enemyAttackData;
            _bulletSystem = bulletSystem;
        }

        public void OnFixedUpdate()
        {
            if (!_enemyAttackData.IsReached)
            {
                return;
            }

            if ((_currentTime -= Time.fixedDeltaTime) <= 0)
            {
                Fire();
                ResetTimer();
            }
        }

        public void OnPausedFixedUpdate()
        {
            return;
        }

        private void ResetTimer()
        {
            _currentTime = _enemyAttackData.Countdown;
        }

        private void Fire()
        {
            if (_enemyAttackData.Target != null)
            {
                var startPosition = _enemyAttackData.WeaponComponent.Position;
                var vector = (Vector2)_enemyAttackData.Target.transform.position - startPosition;
                var direction = vector.normalized;
                _bulletSystem.FlyBulletByArgs(new BulletArgs
                (
                    startPosition,
                    direction * _enemyAttackData.BulletConfig.speed,
                    _enemyAttackData.BulletConfig.color,
                    (int)_enemyAttackData.BulletConfig.physicsLayer,
                    _enemyAttackData.BulletConfig.damage,
                    false
                ));
            }
        }
    }
}