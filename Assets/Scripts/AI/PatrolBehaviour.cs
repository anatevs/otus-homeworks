using Atomic.AI;
using Sample;
using UnityEngine;

namespace Scripts.AI
{
    public class PatrolBehaviour : IAIUpdate
    {
        [SerializeField]
        private float _distancePrecision = 0.5f;

        private int _currentIndex = 0;

        public void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (blackboard.TryGetCharacter(out var character) &&
                blackboard.TryGetWaypoints(out var waypoints))
            {
                var direction = GetOnPointDirection(character.transform.position, waypoints);

                character.GetComponent<MoveComponent>().SetDirection(direction);
            }
        }

        private Vector3 GetOnPointDirection(Vector3 characterPos, Transform[] points)
        {
            var currentDirection = GetCurrentDirection(characterPos, points);

            if (currentDirection.sqrMagnitude <= _distancePrecision * _distancePrecision)
            {
                ChangePoint(points);

                var newDirection = GetCurrentDirection(characterPos, points);
                newDirection.y = 0;

                return newDirection.normalized;
            }

            return currentDirection;
        }

        private Vector3 GetCurrentDirection(Vector3 characterPos, Transform[] points)
        {
            var direction = points[_currentIndex].position - characterPos;
            direction.y = 0;

            return direction;
        }

        private void ChangePoint(Transform[] points)
        {
            _currentIndex = (_currentIndex + 1) % points.Length;
        }
    }
}