using System;
using UnityEngine;
using VContainer;

namespace Scripts.Chest
{
    public class ChestTimer : MonoBehaviour
    {
        public event Action OnCounted;

        public TimeSpan CurrentSpan
        {
            get => _currentSpan;
            set { _currentSpan = value; }
        }

        [SerializeField]
        private TimeStruct _awaitingTime;

        [Inject]
        private TimeService _timeService;

        private DateTime _startTime;

        private DateTime _currentTime;

        private TimeSpan _awaitingSpan;

        private TimeSpan _currentSpan;

        private bool _isCounted;

        private void Awake()
        {
            _awaitingSpan = new TimeSpan(
                _awaitingTime.Hours,
                _awaitingTime.Minutes,
                _awaitingTime.Seconds);

            _currentSpan = new TimeSpan(0, 0, 0);
        }

        private void Start()
        {
            _startTime = _timeService.CurrentTime;

            Debug.Log($"start {_startTime}");
        }

        private void Update()
        {
            _currentTime = _timeService.CurrentTime;

            //Debug.Log($"current {_currentTime}");

            _currentSpan = _currentTime - _startTime;

            if (_currentSpan > _awaitingSpan && !_isCounted)
            {
                Debug.Log($"chest {gameObject.name} is ready to open!");
                _isCounted = true;

                OnCounted?.Invoke();

                //ResetCounter();
            }
        }

        public void ResetCounter()
        {
            _isCounted = false;
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