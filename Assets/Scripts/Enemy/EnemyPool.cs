using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPool : MonoBehaviour
    {
        [Header("Spawn")]
        [SerializeField]
        private Transform _worldTransform;

        [Header("Pool")]
        [SerializeField]
        private Transform _container;

        [SerializeField]
        private GameObject _prefab;

        [SerializeField]
        private GameManager _gameManager;

        private readonly Queue<GameObject> _enemyPool = new();

        private readonly int _poolSize = 7;
        
        private void Awake()
        {
            for (var i = 0; i < _poolSize; i++)
            {
                var enemy = Instantiate(_prefab, _container);
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
            enemy.transform.SetParent(_container);
            _enemyPool.Enqueue(enemy);
        }
    }
}