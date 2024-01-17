using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        public bool IsReached
        {
            get => _isReached;
            set => _isReached = value;
        }

        public Vector2 Destination
        {
            get => _destination;
        }

        public MoveComponent MoveComponent
        { 
            get => _moveComponent;
        }

        [SerializeField]
        private MoveComponent _moveComponent;

        private Vector2 _destination;

        private bool _isReached;

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isReached = false;
        }
    }
}