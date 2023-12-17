using UnityEngine;

namespace ShootEmUp
{
    public class EnemyCooldownSpawner : MonoBehaviour,
        IStartGame,
        IUpdate,
        IPausedUpdate
    {

        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private float _cooldownTime = 1f;

        private float _prevTime;

        public void OnStart()
        {
            RefreshCounter();
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
                RefreshCounter();
            }
        }

        private void RefreshCounter()
        {
            _prevTime = Time.time;
            _enemyManager.SpawnEnemy();
        }
    }
}