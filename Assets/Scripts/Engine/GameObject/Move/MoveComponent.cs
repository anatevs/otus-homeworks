using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Engine
{
    public sealed class MoveComponent : MonoBehaviour
    {
        public event Action OnMove;

        public bool IsMoving => _isMoving;
        public Vector3 MoveDirection => _moveDirection;

        [SerializeField]
        private float _moveSpeed = 3.0f;

        [ShowInInspector, ReadOnly]
        private Vector3 _moveDirection;

        [ShowInInspector, ReadOnly]
        private bool _isMoving;

        public void MoveStep(Vector3 direction)
        {
            _moveDirection = direction;
            _isMoving = true;
        }

        private void FixedUpdate()
        {
            if (_isMoving)
            {
                transform.position += _moveSpeed * Time.fixedDeltaTime * _moveDirection;
                OnMove?.Invoke();
                _isMoving = false;
            }
        }
    }
}