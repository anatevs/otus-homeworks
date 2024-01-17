using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager
    {
        private EnemyPool _enemyPool;

        private EnemyPositions _enemyPositions;

        private GameObject _character;

        private readonly HashSet<GameObject> _activeEnemies = new();

        public EnemyManager(EnemyPool enemyPool, EnemyPositions enemyPositions, GameObject target)
        {
            _enemyPool = enemyPool;
            _enemyPositions = enemyPositions;
            _character = target;
        }

        public void SpawnEnemy()
        {
            if (_enemyPool.TrySpawnEnemy(out var enemy))
            {
                if (_activeEnemies.Add(enemy))
                {
                    var spawnPosition = _enemyPositions.RandomSpawnPosition();
                    enemy.transform.position = spawnPosition.position;

                    var attackPosition = _enemyPositions.RandomAttackPosition();
                    enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);

                    enemy.GetComponent<EnemyAttackAgent>().Target = _character;

                    enemy.GetComponent<HitPointsComponent>().OnHPZero += OnDestroyed;
                }
                else
                {
                    _enemyPool.UnSpawnEnemy(enemy);
                }
            }
        }

        private void OnDestroyed(HitPointsComponent enemyHP)
        {
            if (_activeEnemies.Remove(enemyHP.gameObject))
            {
                enemyHP.OnHPZero -= OnDestroyed;
                _enemyPool.UnSpawnEnemy(enemyHP.gameObject);
            }
        }
    }
}