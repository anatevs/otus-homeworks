using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveController :
        IFixedUpdate,
        IPausedFixedUpdate
    {
        private readonly EnemyMoveAgent _enemyMoveAgent;

        private readonly float _posSqrAccuracy = 0.25f * 0.25f;

        public EnemyMoveController(EnemyMoveAgent enemyMoveAgent)
        {
            _enemyMoveAgent = enemyMoveAgent;
        }

        public void OnFixedUpdate()
        {
            if (_enemyMoveAgent.IsReached)
            {
                return;
            }

            var vector = _enemyMoveAgent.Destination - (Vector2)_enemyMoveAgent.transform.position;
            if (vector.sqrMagnitude <= _posSqrAccuracy)
            {
                _enemyMoveAgent.IsReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            _enemyMoveAgent.MoveComponent.MoveByRigidbodyVelocity(direction);
        }
        public void OnPausedFixedUpdate()
        {
            return;
        }
    }
}