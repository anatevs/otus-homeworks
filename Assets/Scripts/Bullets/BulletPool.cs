using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShootEmUp
{
    public sealed class BulletPool
    {
        private Transform _worldTransform;

        private Transform _poolTransform;

        private int _initialCount;

        private Bullet _prefab;

        private GameManagerData _gameManagerData;

        private IObjectResolver _objectResolver;

        private readonly Queue<Bullet> _bulletPool = new();

        private Dictionary<Bullet, BulletController> _bulletControllers = new();

        public BulletPool(IObjectResolver objectResolver, GameManagerData gameManagerData, BulletPoolParams bulletParams)
        {
            _objectResolver = objectResolver;

            _gameManagerData = gameManagerData;
            _worldTransform = bulletParams.worldTransform;
            _poolTransform = bulletParams.poolTransform;
            _initialCount = bulletParams.initialCount;
            _prefab = bulletParams.bulletPrefab;

            for (var i = 0; i < _initialCount; i++)
            {
                var bullet = _objectResolver.Instantiate(_prefab, _poolTransform);
                _bulletPool.Enqueue(bullet);

                BulletController bulletController = new BulletController(bullet);
                _bulletControllers[bullet] = bulletController;
            }
        }

        public Bullet SpawnBullet()
        {
            if (_bulletPool.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(_worldTransform);
                _gameManagerData.AddListener(_bulletControllers[bullet]);
            }
            else
            {
                bullet = _objectResolver.Instantiate(_prefab, _worldTransform);
                _bulletPool.Enqueue(bullet);
                _gameManagerData.AddListeners(bullet.gameObject);

                BulletController bulletController = new BulletController(bullet);
                _bulletControllers[bullet] = bulletController;
                _gameManagerData.AddListener(_bulletControllers[bullet]);
            }
            return bullet;
        }

        public void UnSpawnBullet(Bullet bullet)
        {
            bullet.transform.SetParent(_poolTransform);
            _gameManagerData.RemoveListener(_bulletControllers[bullet]);
            _bulletPool.Enqueue(bullet);
        }
    }
}