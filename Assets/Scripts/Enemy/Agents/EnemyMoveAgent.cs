using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        public bool IsReached => _isReached;

        public bool Enabled { get; private set; }

        [SerializeField]
        private MoveComponent _moveComponent;

        private Vector2 _destination;

        private bool _isReached;

        private readonly float _posSqrAccuracy = 0.25f * 0.25f;

        private void Awake()
        {
            Enabled = true;
        }

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isReached = false;
        }

        public void FixedUpdateMove()
        {
            if (_isReached)
            {
                return;
            }

            var vector = _destination - (Vector2)transform.position;
            if (vector.sqrMagnitude <= _posSqrAccuracy)
            {
                _isReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            _moveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}