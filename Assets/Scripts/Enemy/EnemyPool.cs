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

        private BulletSystem _bulletSystem;

        private IObjectResolver _objectResolver;

        private readonly Queue<GameObject> _enemyPool = new();

        private readonly Dictionary<GameObject, IGameListener[]> _enemyControllers = new();

        public EnemyPool(IObjectResolver objectResolver, GameManagerData gameManagerData, BulletSystem bulletSystem, EnemyPoolParams enemyPoolParams)
        {
            _objectResolver = objectResolver;

            _worldTransform = enemyPoolParams.worldTransform;
            _poolTransform = enemyPoolParams.poolTransform;
            _poolSize = enemyPoolParams.poolSize;
            _prefab = enemyPoolParams.enemyPrefab;
            _gameManagerData = gameManagerData;
            _bulletSystem = bulletSystem;

            for (var i = 0; i < _poolSize; i++)
            {
                var enemy = _objectResolver.Instantiate(_prefab, _poolTransform);
                _enemyPool.Enqueue(enemy);

                EnemyMoveController enemyMoveController = 
                    new EnemyMoveController(enemy.GetComponent<EnemyMoveAgent>());

                EnemyAttackController enemyAttackController = 
                    new EnemyAttackController(enemy.GetComponent<EnemyAttackAgent>(), _bulletSystem);

                _enemyControllers[enemy] = new IGameListener[2] {enemyMoveController, enemyAttackController};
            }
        }

        public bool TrySpawnEnemy(out GameObject enemy)
        {
            if (_enemyPool.TryDequeue(out enemy))
            {
                enemy.transform.SetParent(_worldTransform);
                _gameManagerData.AddListeners(_enemyControllers[enemy]);
                return true;
            }
            return false;
        }

        public void UnSpawnEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(_poolTransform);
            _gameManagerData.RemoveListeners(_enemyControllers[enemy]);
            _enemyPool.Enqueue(enemy);
        }
    }
}