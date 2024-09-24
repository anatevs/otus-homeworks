using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class PatrolPoints : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _points;

        [SerializeField]
        private float _distancePrecision;

        private int _currentIndex = 0;

        private Vector3[] _positions;

        private void Awake()
        {
            _positions = new Vector3[_points.Length];

            for (int i = 0;  i < _points.Length; i++)
            {
                _positions[i] = _points[i].position;
            }

            _distancePrecision *= _distancePrecision;
        }

        public Vector3 GetPointDirection(Vector3 characterPos)
        {
            var currentDirection = GetCurrentDirection(characterPos);

            if (currentDirection.sqrMagnitude <= _distancePrecision)
            {
                ChangePoint();

                var newDirection = GetCurrentDirection(characterPos);
                newDirection.y = 0;

                return newDirection.normalized;
            }

            return currentDirection;
        }

        private Vector3 GetCurrentDirection(Vector3 characterPos)
        {
            var direction = _positions[_currentIndex] - characterPos;
            direction.y = 0;

            return direction;
        }

        private void ChangePoint()
        {
            _currentIndex = (_currentIndex + 1) % _positions.Length;
        }
    }
}