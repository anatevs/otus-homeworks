using System;
using UnityEngine;
using Utils;

namespace Sample
{
    public class RotationComponent : MonoBehaviour
    {
        [SerializeField]
        private Transform _rotationRoot;
        
        [SerializeField]
        private Vector3 _rotateDirection;
        
        [SerializeField]
        private float _rotateRate;

        private readonly AndCondition _condition = new();

        private void FixedUpdate()
        {
            if (!_condition.IsTrue())
            {
                return;
            }

            if (_rotateDirection == Vector3.zero)
            {
                return;
            }

            Quaternion targetRotation = Quaternion.LookRotation(_rotateDirection, Vector3.up);
            Quaternion nextRotation = Quaternion.Lerp(_rotationRoot.rotation, targetRotation, _rotateRate);
            _rotationRoot.rotation = nextRotation;
        }

        public void Rotate(Vector3 direction)
        {
            _rotateDirection = direction;
        }

        public void AppendCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }
    }
}