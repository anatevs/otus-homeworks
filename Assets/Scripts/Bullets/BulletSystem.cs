using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField] BulletPool bulletPool;
        [SerializeField] private LevelBounds levelBounds;

        private readonly HashSet<Bullet> m_activeBullets = new();
        private readonly List<Bullet> m_cache = new();


        private void FixedUpdate()
        {
            this.m_cache.Clear();
            this.m_cache.AddRange(this.m_activeBullets);

            for (int i = 0, count = this.m_cache.Count; i < count; i++)
            {
                var bullet = this.m_cache[i];
                if (!this.levelBounds.InBounds(bullet.transform.position))
                {
                    this.RemoveBullet(bullet);
                }
            }
        }

        public void FlyBulletByArgs(Args args)
        {
            var bullet = this.bulletPool.SpawnBullet();
            if (bullet != null)
            {
                bullet.SetPosition(args.position);
                bullet.SetColor(args.color);
                bullet.SetPhysicsLayer(args.physicsLayer);
                bullet.damage = args.damage;
                bullet.isPlayer = args.isPlayer;
                bullet.SetVelocity(args.velocity);

                if (this.m_activeBullets.Add(bullet))
                {
                    bullet.OnCollisionEntered += this.OnBulletCollision;
                }
            }
        }
        
        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            BulletUtils.DealDamage(bullet, collision.gameObject);
            this.RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (this.m_activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= this.OnBulletCollision;
                this.bulletPool.UnspawnBullet(bullet);
            }
        }
        
        public struct Args
        {
            public Vector2 position;
            public Vector2 velocity;
            public Color color;
            public int physicsLayer;
            public int damage;
            public bool isPlayer;
        }
    }
}