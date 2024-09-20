using System;
using UnityEngine;

namespace Scripts.Chest
{
    public class ChestTimer : MonoBehaviour
    {
        public TimeSpan CurrentSpan
        {
            get => _currentSpan;
            set { _currentSpan = value; }
        }

        [SerializeField]
        private TimeStruct _awaitingTime;

        [SerializeField]
        private ChestAnim _chestAnim;

        [SerializeField]
        private TimeService _timeService;

       private string _name;

        private DateTime _startTime;

        private DateTime _currentTime;

        private TimeSpan _awaitingSpan;

        private TimeSpan _currentSpan;

        private void Awake()
        {
            _awaitingSpan = new TimeSpan(
                _awaitingTime.Hours,
                _awaitingTime.Minutes,
                _awaitingTime.Seconds);
        }

        private void Start()
        {
            _startTime = _timeService.CurrentTime;
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

        private TimeSpan StructToTimeSpan(TimeStruct timeStruct)
        {
            return new TimeSpan(
                    timeStruct.Hours,
                    timeStruct.Minutes,
                    timeStruct.Seconds);
        }

        private TimeStruct TimeSpanToStruct(TimeSpan timeSpan)
        {
            return new TimeStruct()
            {
                Hours = timeSpan.Hours,
                Minutes = timeSpan.Minutes,
                Seconds = timeSpan.Seconds
            };
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