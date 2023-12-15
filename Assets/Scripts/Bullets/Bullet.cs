using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour,
        GameListeners.IPauseGame,
        GameListeners.IResumeGame
    {
        public event Action<Bullet> OnCollisionEntered;

        public bool IsPlayer { get; set; }
        public int Damage { get; set; }

        [SerializeField]
        private new Rigidbody2D _rigidbody2D;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private Vector2 _currVelocity;

        public void OnPause()
        {
            _currVelocity = this._rigidbody2D.velocity;
            this._rigidbody2D.velocity = Vector2.zero;
        }
        public void OnResume()
        {
            this._rigidbody2D.velocity = this._currVelocity;
        }

        public void SetVelocity(Vector2 velocity)
        {
            this._rigidbody2D.velocity = velocity;
        }

        public void SetPhysicsLayer(int physicsLayer)
        {
            this.gameObject.layer = physicsLayer;
        }

        public void SetPosition(Vector3 position)
        {
            this.transform.position = position;
        }

        public void SetColor(Color color)
        {
            this._spriteRenderer.color = color;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            DealDamage(collision.gameObject);
            this.OnCollisionEntered?.Invoke(this);

        }

        private void DealDamage(GameObject other)
        {
            if (!other.TryGetComponent(out TeamComponent team))
            {
                return;
            }

            if (this.IsPlayer == team.IsPlayer)
            {
                return;
            }

            if (other.TryGetComponent(out HitPointsComponent hitPoints))
            {
                hitPoints.TakeDamage(this.Damage);
            }
        }

    }
}