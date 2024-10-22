using UnityEngine;

namespace Game.Engine
{
    public class Waypoints : MonoBehaviour
    {
        public float StopDistance => _stopDistance;

        [SerializeField]
        private Transform[] _waypoints;

        [SerializeField]
        private float _stopDistance;

        private int _currentIndex;

        public Transform GetAndSetNextWaypoint()
        {
            _currentIndex = (_currentIndex + 1) % _waypoints.Length;
            return _waypoints[_currentIndex];
        }

        public Transform GetAndSetNearestWaypoint(Transform target)
        {
            int index = 0;
            var direction = _waypoints[index].position - target.position;
            var minDistance = direction.sqrMagnitude;
            Transform result = _waypoints[index];

            for (int i = 1; i < _waypoints.Length; i++)
            {
                direction = _waypoints[i].position - target.position;

                if (direction.sqrMagnitude < minDistance)
                {
                    index = i;
                    minDistance = direction.sqrMagnitude;
                    result = _waypoints[index];
                }
            }

            _currentIndex = index;
            return result;
        }

        public bool TargetIsWaypoint(Transform target)
        {
            foreach (var waypoint in _waypoints)
            {
                if (waypoint == target)
                {
                    return true;
                }
            }

            return false;
        }
    }
}