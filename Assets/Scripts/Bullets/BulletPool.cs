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

        private readonly Queue<Bullet> m_bulletPool = new();

        private void Awake()
        {
            for (var i = 0; i < this._initialCount; i++)
            {
                var bullet = Instantiate(this._prefab, this._container);
                this.m_bulletPool.Enqueue(bullet);
            }
        }

        public Bullet SpawnBullet()
        {
            if (this.m_bulletPool.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(this._worldTransform);
            }
            else
            {
                bullet = Instantiate(this._prefab, this._worldTransform);
            }
            return bullet;
        }

        public void UnspawnBullet(Bullet bullet)
        {
            bullet.transform.SetParent(this._container);
            this.m_bulletPool.Enqueue(bullet);
        }
    }
}
