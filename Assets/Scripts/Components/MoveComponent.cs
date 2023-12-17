using UnityEngine;

namespace ShootEmUp
{
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField]
        private new Rigidbody2D _rigidbody2D;

        [SerializeField]
        private float speed = 5.0f;
        
        public void MoveByRigidbodyVelocity(Vector2 vector)
        {
            var nextPosition = _rigidbody2D.position + vector * speed;
            _rigidbody2D.MovePosition(nextPosition);
        }
    }
}