using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyCooldownSpawner :
        IStartGame,
        IUpdate,
        IPausedUpdate
    {
        private EnemyManager _enemyManager;

        private float _cooldownTime = 1f;

        private float _prevTime;

        public EnemyCooldownSpawner(EnemyManager enemyManager, float cooldownTime)
        {
            _enemyManager = enemyManager;
            _cooldownTime = cooldownTime;
        }

        public void OnStart()
        {
            ResetCounter();
        }

        public void OnUpdate()
        {
            CooldownCounter();
        }

        public void OnPausedUpdate()
        {
            return;
        }

        private void CooldownCounter()
        {
            if (Time.time - _prevTime >= _cooldownTime)
            {
                ResetCounter();
            }
        }

        private void ResetCounter()
        {
            _prevTime = Time.time;
            _enemyManager.SpawnEnemy();
        }
    }
}