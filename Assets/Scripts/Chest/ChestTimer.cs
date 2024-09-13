using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Chest
{
    public class ChestTimer : MonoBehaviour
    {
        [SerializeField]
        private TimeStruct _awaitingTime;

        [SerializeField]
        private Chest _chest;

        [SerializeField]
        private TimeService _timeService;

        private DateTime _startTime;

        private DateTime _currentTime;

        private TimeSpan _awaitingSpan;

        private TimeSpan _currentSpan;

        private void Start()
        {
            _startTime = _timeService.CurrentTime;

            _awaitingSpan = new TimeSpan(
                _awaitingTime.Hours,
                _awaitingTime.Minutes,
                _awaitingTime.Seconds);
        }

        private void Update()
        {
            _currentTime = _timeService.CurrentTime;

            _currentSpan = _currentTime - _startTime;

            if (_currentSpan > _awaitingSpan)
            {
                //Debug.Log($"chest {gameObject.name} is ready to open!");
                ResetCounter();
            }
        }

        private void ResetCounter()
        {
            _startTime = _timeService.CurrentTime;
        }
    }

    [Serializable]
    public struct TimeStruct
    {
        [field: SerializeField]
        public int Hours { get; set; }

        [field: SerializeField]
        public int Minutes { get; set; }

        [field: SerializeField]
        public int Seconds { get; set; }
    }
}