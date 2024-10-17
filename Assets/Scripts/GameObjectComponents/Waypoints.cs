using UnityEngine;

namespace GameObjectComponents
{
    public class Waypoints : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _waypoints;

        private int _currentIndex;

        public Transform GetNextWaypoint()
        {
            _currentIndex = (_currentIndex + 1) % _waypoints.Length;
            return _waypoints[_currentIndex];
        }
    }
}