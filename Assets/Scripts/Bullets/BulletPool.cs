using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletPool : MonoBehaviour
    {
        [Header("Spawn")]
        [SerializeField]
        private Transform _worldTransform;

        [Header("Pool")]
        [SerializeField]
        private Transform _container;
        
        [SerializeField]
        private Bullet _prefab;

        [SerializeField]
        private int _initialCount = 50;

        [SerializeField]
        private GameManager _gameManager;

        private readonly Queue<Bullet> _bulletPool = new();

        private void Awake()
        {
            for (var i = 0; i < _initialCount; i++)
            {
                var bullet = Instantiate(_prefab, _container);
                _bulletPool.Enqueue(bullet);
            }
        }

        public Bullet SpawnBullet()
        {
            if (_bulletPool.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(_worldTransform);
            }
            else
            {
                bullet = Instantiate(_prefab, _worldTransform);
                _gameManager.AddListeners(bullet.gameObject);
            }
            return bullet;
        }

        public void UnspawnBullet(Bullet bullet)
        {
            bullet.transform.SetParent(_container);
            _bulletPool.Enqueue(bullet);
        }
    }
}
