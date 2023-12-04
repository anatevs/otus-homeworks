using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        public bool IsReached
        {
            get { return this._isReached; }
        }

        [SerializeField] private MoveComponent _moveComponent;

        private Vector2 _destination;

        private bool _isReached;
        private readonly float _posSqrAccurancy = 0.25f * 0.25f;

        public void SetDestination(Vector2 endPoint)
        {
            this._destination = endPoint;
            this._isReached = false;
        }

        private void FixedUpdate()
        {
            if (this._isReached)
            {
                return;
            }
            
            var vector = this._destination - (Vector2) this.transform.position;
            if (vector.sqrMagnitude <= this._posSqrAccurancy)
            {
                this._isReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            this._moveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}