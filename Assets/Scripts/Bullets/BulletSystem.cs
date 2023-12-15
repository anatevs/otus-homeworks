using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed partial class BulletSystem : MonoBehaviour,
        GameListeners.IFixedUpdate
    {
        public bool Enabled { get { return true; } }

        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] private LevelBounds _levelBounds;

        private readonly HashSet<Bullet> _activeBullets = new();
        private readonly List<Bullet> _cache = new();

        public void OnFixedUpdate()
        {
            this._cache.Clear();
            this._cache.AddRange(this._activeBullets);

            for (int i = 0, count = this._cache.Count; i < count; i++)
            {
                var bullet = this._cache[i];
                if (!this._levelBounds.InBounds(bullet.transform.position))
                {
                    this.RemoveBullet(bullet);
                }
            }
        }

        public void FlyBulletByArgs(BulletArgs args)
        {
            var bullet = this._bulletPool.SpawnBullet();
            if (bullet != null)
            {
                bullet.SetPosition(args.position);
                bullet.SetColor(args.color);
                bullet.SetPhysicsLayer(args.physicsLayer);
                bullet.Damage = args.damage;
                bullet.IsPlayer = args.isPlayer;
                bullet.SetVelocity(args.velocity);

                if (this._activeBullets.Add(bullet))
                {
                    bullet.OnCollisionEntered += this.RemoveBullet;
                }
            }
        }
        
        private void RemoveBullet(Bullet bullet)
        {
            if (this._activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= this.RemoveBullet;
                this._bulletPool.UnspawnBullet(bullet);
            }
        }
    }
}