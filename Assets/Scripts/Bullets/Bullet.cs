using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour,
        IPauseGame,
        IResumeGame
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
            _currVelocity = _rigidbody2D.velocity;
            _rigidbody2D.velocity = Vector2.zero;
        }
        public void OnResume()
        {
            _rigidbody2D.velocity = _currVelocity;
        }

        public void SetVelocity(Vector2 velocity)
        {
            _rigidbody2D.velocity = velocity;
        }

        public void SetPhysicsLayer(int physicsLayer)
        {
            gameObject.layer = physicsLayer;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            DealDamage(collision.gameObject);
            OnCollisionEntered?.Invoke(this);

        }

        private void DealDamage(GameObject other)
        {
            if (!other.TryGetComponent(out TeamComponent team))
            {
                return;
            }

            if (IsPlayer == team.IsPlayer)
            {
                return;
            }

            if (other.TryGetComponent(out HitPointsComponent hitPoints))
            {
                hitPoints.TakeDamage(Damage);
            }
        }

    }
}