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
        private GameManagerInstaller _installerManager;

        private readonly Queue<Bullet> _bulletPool = new();

        private void Awake()
        {
            for (var i = 0; i < this._initialCount; i++)
            {
                var bullet = Instantiate(this._prefab, this._container);
                this._bulletPool.Enqueue(bullet);
            }
        }

        public Bullet SpawnBullet()
        {
            if (this._bulletPool.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(this._worldTransform);
            }
            else
            {
                bullet = Instantiate(this._prefab, this._worldTransform);
            }
            _installerManager.AddObjectGameListeners(bullet.gameObject, true);
            return bullet;
        }

        public void UnspawnBullet(Bullet bullet)
        {
            bullet.transform.SetParent(this._container);
            this._bulletPool.Enqueue(bullet);
            _installerManager.RemoveObjectGameListeners(bullet.gameObject);
        }
    }
}
