using UnityEngine;

namespace GameObjectComponents
{
    public class Waypoints : MonoBehaviour
    {
        public float StopDistance => _stopDistance;

        [SerializeField]
        private Transform[] _waypoints;

        [SerializeField]
        private float _stopDistance;

        private int _currentIndex;

        public Transform GetNextWaypoint()
        {
            _currentIndex = (_currentIndex + 1) % _waypoints.Length;
            return _waypoints[_currentIndex];
        }
    }
}