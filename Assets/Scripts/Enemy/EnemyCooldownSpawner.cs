using ShootEmUp;
using System.Collections;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemyCooldownSpawner : MonoBehaviour,
        GameListeners.IStartGame,
        GameListeners.IPauseGame,
        GameListeners.IResumeGame,
        GameListeners.IUpdate
    {

        public bool Enabled { get; private set; }

        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private float _cooldownTime = 1f;

        private float _prevTime;

        public void OnStart()
        {
            Enabled = true;
            RefreshCounter();
        }

        public void OnPause()
        {
            Enabled = false;
        }

        public void OnResume()
        {
            Enabled = true;
        }

        public void OnUpdate()
        {
            CooldownCounter();
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