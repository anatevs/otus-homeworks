using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField]
        private EnemyPool _enemyPool;

        [SerializeField]
        private EnemyPositions _enemyPositions;

        [SerializeField]
        private GameObject _character;

        [SerializeField]
        private BulletSystem _bulletSystem;

        private readonly HashSet<GameObject> _activeEnemies = new();


        public void SpawnEnemy()
        {
            if (this._enemyPool.TrySpawnEnemy(out var enemy))
            {
                if (this._activeEnemies.Add(enemy))
                {
                    SetEnemyPositions(enemy, this._enemyPositions);
                    
                    EnemyAttackAgent enemyAttackAgent = enemy.GetComponent<EnemyAttackAgent>();
                    enemyAttackAgent.SetBulletSystem(this._bulletSystem);
                    enemyAttackAgent.SetTarget(this._character);
                    
                    enemy.GetComponent<HitPointsComponent>().OnHPempty += this.OnDestroyed;
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
                enemy.GetComponent<HitPointsComponent>().OnHPempty -= this.OnDestroyed;

                this._enemyPool.UnspawnEnemy(enemy);
            }
        }
    }
}