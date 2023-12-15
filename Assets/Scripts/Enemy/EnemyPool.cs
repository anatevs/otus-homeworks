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
        private GameManagerInstaller _installerManager;

        private readonly Queue<GameObject> _enemyPool = new();

        private readonly int _poolSize = 7;
        
        private void Awake()
        {
            for (var i = 0; i < this._poolSize; i++)
            {
                var enemy = Instantiate(this._prefab, this._container);
                this._enemyPool.Enqueue(enemy);
            }
        }

        public bool TrySpawnEnemy(out GameObject resEnemy)
        {
            if (this._enemyPool.TryDequeue(out resEnemy))
            {
                _installerManager.AddObjectGameListeners(resEnemy.gameObject, true);
                resEnemy.transform.SetParent(this._worldTransform);
                return true;
            }
            return false;
        }

        public void UnspawnEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(this._container);
            this._enemyPool.Enqueue(enemy);
            _installerManager.RemoveObjectGameListeners(enemy.gameObject);
        }
    }
}