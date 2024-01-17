using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet> OnCollisionEntered;

        public bool IsFromPlayer { get; set; }

        public int Damage { get; set; }

        [SerializeField]
        private Rigidbody2D _rigidbody2D;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private GameObject _collisionObject;

        public Vector2 GetCurrentVelocity()
        {
            return _rigidbody2D.velocity;
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
            _collisionObject = collision.gameObject;
            OnCollisionEntered?.Invoke(this);
        }

        public GameObject GetCollisionObject()
        {
            return _collisionObject;
        }
    }
}