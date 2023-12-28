using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager
    {
        private EnemyPool _enemyPool;
        private EnemyPositions _enemyPositions;
        private GameObject _character;
        private BulletSystem _bulletSystem;

        private readonly HashSet<GameObject> _activeEnemies = new();

        public EnemyManager(EnemyPool enemyPool, EnemyPositions enemyPositions, GameObject target, BulletSystem bulletSystem)
        {
            _enemyPool = enemyPool;
            _enemyPositions = enemyPositions;
            _character = target;
            _bulletSystem = bulletSystem;
        }

        public void SpawnEnemy()
        {
            if (_enemyPool.TrySpawnEnemy(out var enemy))
            {
                if (_activeEnemies.Add(enemy))
                {
                    SetEnemyPositions(enemy, _enemyPositions);
                    
                    EnemyAttackAgent enemyAttackAgent = enemy.GetComponent<EnemyAttackAgent>();
                    enemyAttackAgent.SetBulletSystem(_bulletSystem);
                    enemyAttackAgent.SetTarget(_character);
                    
                    enemy.GetComponent<HitPointsComponent>().OnHPempty += OnDestroyed;
                }
            }
        }

        void SetEnemyPositions(GameObject enemy, EnemyPositions enemyPositions)
        {
            var spawnPosition = enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;

            var attackPosition = enemyPositions.RandomAttackPosition();
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);
        }

        private void OnDestroyed(GameObject enemy)
        {
            if (_activeEnemies.Remove(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().OnHPempty -= OnDestroyed;

                _enemyPool.UnspawnEnemy(enemy);
            }
        }
    }
}