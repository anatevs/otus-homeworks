using System;
using UnityEngine;
using Utils;

namespace Sample
{
    public class MoveComponent : MonoBehaviour
    {
        [SerializeField]
        private Transform _root;
        
        [SerializeField]
        private float _speed = 3f;
        
        [SerializeField]
        private Vector3 _moveDirection;

        private readonly AndCondition _condition = new();

        public Vector3 Direction => _moveDirection;

        private void FixedUpdate()
        {
            var isTrue = _condition.IsTrue();
            if (isTrue)
            {
                _root.position += _moveDirection * _speed * Time.fixedDeltaTime;
            }
        }
        
        public void SetDirection(Vector3 direction)
        {
            _moveDirection = direction;
        }

        public void AppendCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }
    }
}