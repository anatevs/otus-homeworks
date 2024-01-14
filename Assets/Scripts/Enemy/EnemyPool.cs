using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShootEmUp
{
    public sealed class EnemyPool
    {
        private Transform _worldTransform;

        private Transform _poolTransform;

        private int _poolSize;

        private GameObject _prefab;

        private GameManagerData _gameManagerData;

        private IObjectResolver _objectResolver;

        private readonly Queue<GameObject> _enemyPool = new();

        public EnemyPool(IObjectResolver objectResolver, GameManagerData gameManagerData, EnemyPoolParams enemyPoolParams)
        {
            _objectResolver = objectResolver;
            
            _worldTransform = enemyPoolParams.worldTransform;
            _poolTransform = enemyPoolParams.poolTransform;
            _poolSize = enemyPoolParams.poolSize;
            _prefab = enemyPoolParams.enemyPrefab;
            _gameManagerData = gameManagerData;

            for (var i = 0; i < _poolSize; i++)
            {
                var enemy = _objectResolver.Instantiate(_prefab, _poolTransform);
                _enemyPool.Enqueue(enemy);

                EnemyMoveController enemyMoveController = 
                    new EnemyMoveController(enemy.GetComponent<EnemyMoveAgent>());
                _gameManagerData.AddListener(enemyMoveController);

                EnemyAttackController enemyAttackController = 
                    new EnemyAttackController(enemy.GetComponent<EnemyAttackAgent>());
                _gameManagerData.AddListener(enemyAttackController);
            }
        }

        public bool TrySpawnEnemy(out GameObject resEnemy)
        {
            if (_enemyPool.TryDequeue(out resEnemy))
            {
                resEnemy.transform.SetParent(_worldTransform);
                return true;
            }
            return false;
        }

        public void UnSpawnEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(_poolTransform);
            _enemyPool.Enqueue(enemy);
        }
    }
}