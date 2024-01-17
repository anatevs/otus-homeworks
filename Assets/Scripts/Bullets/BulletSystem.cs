using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem :
        IFixedUpdate,
        IPausedFixedUpdate
    {
        private BulletPool _bulletPool;

        private LevelBounds _levelBounds;

        public BulletSystem(BulletPool bulletPool, LevelBounds levelBounds)
        {
            _bulletPool = bulletPool;
            _levelBounds = levelBounds;
        }

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
            bullet.SetPosition(args.position);
            bullet.SetColor(args.color);
            bullet.SetPhysicsLayer(args.physicsLayer);
            bullet.Damage = args.damage;
            bullet.IsFromPlayer = args.isFromPlayer;
            bullet.SetVelocity(args.velocity);

            if (_activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += RemoveBullet;
                bullet.OnCollisionEntered += DealDamage;
            }
        }

        private void DealDamage(Bullet bullet)
        {
            GameObject other = bullet.GetCollisionObject();

            if (!other.TryGetComponent(out TeamComponent team))
            {
                return;
            }

            if (bullet.IsFromPlayer == team.IsPlayer)
            {
                return;
            }

            if (other.TryGetComponent(out HitPointsComponent hitPoints))
            {
                hitPoints.TakeDamage(bullet.Damage);
            }
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (_activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= RemoveBullet;
                bullet.OnCollisionEntered -= DealDamage;
                _bulletPool.UnSpawnBullet(bullet);
            }
        }
    }
}