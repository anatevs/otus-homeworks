using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed partial class BulletSystem : MonoBehaviour,
        IFixedUpdate,
        IPausedFixedUpdate
    {
        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] private LevelBounds _levelBounds;

        private readonly HashSet<Bullet> _activeBullets = new();
        private readonly List<Bullet> _cache = new();

        public void OnFixedUpdate()
        {
            _cache.Clear();
            _cache.AddRange(_activeBullets);

            for (int i = 0, count = _cache.Count; i < count; i++)
            {
                var bullet = _cache[i];
                if (!_levelBounds.InBounds(bullet.transform.position))
                {
                    RemoveBullet(bullet);
                }
            }
        }

        public void OnPausedFixedUpdate()
        {
            return;
        }

        public void FlyBulletByArgs(BulletArgs args)
        {
            var bullet = _bulletPool.SpawnBullet();
            if (bullet != null)
            {
                bullet.SetPosition(args.position);
                bullet.SetColor(args.color);
                bullet.SetPhysicsLayer(args.physicsLayer);
                bullet.Damage = args.damage;
                bullet.IsPlayer = args.isPlayer;
                bullet.SetVelocity(args.velocity);

                if (_activeBullets.Add(bullet))
                {
                    bullet.OnCollisionEntered += RemoveBullet;
                }
            }
        }
        
        private void RemoveBullet(Bullet bullet)
        {
            if (_activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= RemoveBullet;
                _bulletPool.UnspawnBullet(bullet);
            }
        }
    }
}