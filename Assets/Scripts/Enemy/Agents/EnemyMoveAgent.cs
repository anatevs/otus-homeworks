using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour,
        GameListeners.IFixedUpdate,
        GameListeners.IStartGame,
        GameListeners.IPauseGame,
        GameListeners.IResumeGame
    {
        public bool IsReached => _isReached;

        public bool Enabled { get; private set; }

        [SerializeField] private MoveComponent _moveComponent;

        private Vector2 _destination;

        private bool _isReached;
        private readonly float _posSqrAccurancy = 0.25f * 0.25f;

        private void Awake()
        {
            Enabled = true;
        }

        public void SetDestination(Vector2 endPoint)
        {
            this._destination = endPoint;
            this._isReached = false;
        }

        public void OnFixedUpdate()
        {
            if (this._isReached)
            {
                return;
            }

            var vector = this._destination - (Vector2)this.transform.position;
            if (vector.sqrMagnitude <= this._posSqrAccurancy)
            {
                this._isReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            this._moveComponent.MoveByRigidbodyVelocity(direction);
        }

        public void OnStart()
        {
            Enabled = true;
        }

        public void OnPause()
        {
            Enabled = false;
        }
        public void OnResume()
        {
            Enabled = true;
        }
    }
}