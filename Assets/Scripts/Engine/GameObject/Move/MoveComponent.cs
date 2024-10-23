using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Engine
{
    public sealed class MoveComponent : MonoBehaviour
    {
        public event Action OnMove;

        public bool IsMoving => _isMoving;
        public Vector3 MoveDirection => _moveDirection;

        [SerializeField]
        private NavMeshAgent _agent;

        [SerializeField]
        private float _moveSpeed = 3.0f;

        [ShowInInspector, ReadOnly]
        private Vector3 _moveDirection;

        [ShowInInspector, ReadOnly]
        private bool _isMoving;

        private void Start()
        {
            _agent.updateRotation = true;
        }

        public void MoveStepAgent(Vector3 target, float stoppingDistance)
        {
            _agent.SetDestination(target);
            _isMoving = true;

            _agent.stoppingDistance = stoppingDistance;
        }

        private void FixedUpdate()
        {
            if (_isMoving)
            {
                transform.position += _moveSpeed * Time.fixedDeltaTime * _moveDirection;
                OnMove?.Invoke();
                _isMoving = false;
            }

            if (_agent.pathStatus == NavMeshPathStatus.PathPartial)
            {
                _isMoving = true;
            }
            else if (_agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                _isMoving = false;
            }
        }
    }
}