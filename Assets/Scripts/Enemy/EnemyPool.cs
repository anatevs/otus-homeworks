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
        private int _poolSize = 7;
        private GameObject _prefab;

        private IObjectResolver _objectResolver;

        private readonly Queue<GameObject> _enemyPool = new();

        public EnemyPool(IObjectResolver objectResolver, EnemyPoolParams enemyPoolParams)
        {
            _objectResolver = objectResolver;
            
            _worldTransform = enemyPoolParams.worldTransform;
            _poolTransform = enemyPoolParams.poolTransform;
            _poolSize = enemyPoolParams.poolSize;
            _prefab = enemyPoolParams.enemyPrefab;

            for (var i = 0; i < _poolSize; i++)
            {
                var enemy = _objectResolver.Instantiate(_prefab, _poolTransform);
                _enemyPool.Enqueue(enemy);
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

        public void UnspawnEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(_poolTransform);
            _enemyPool.Enqueue(enemy);
        }
    }
}