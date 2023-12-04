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

        private readonly Queue<GameObject> _enemyPool = new();

        private int _poolSize = 7;
        
        private void Awake()
        {
            for (var i = 0; i < this._poolSize; i++)
            {
                var enemy = Instantiate(this._prefab, this._container);
                this._enemyPool.Enqueue(enemy);
            }
        }

        public GameObject SpawnEnemy()
        {
            if (!this._enemyPool.TryDequeue(out var enemy))
            {
                return null;
            }
            enemy.transform.SetParent(this._worldTransform);

            return enemy;
        }

        public void UnspawnEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(this._container);
            this._enemyPool.Enqueue(enemy);
        }
    }
}