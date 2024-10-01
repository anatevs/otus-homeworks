using System;
using UnityEngine;
using VContainer;

namespace Scripts.Chest
{
    public class ChestTimer : MonoBehaviour
    {
        public event Action OnCounted;

        public TimeSpan RemainderSpan => _remainderSpan;

        //[SerializeField]
        //private TimeStruct _awaitingTime; //config

        private TimeService _timeService;

        private AppInOutTimeService _appInOutTimeService;

        private ChestsData _chestsData;

        private ChestParams _chestsParams;

        private DateTime _startTime;

        private TimeSpan _awaitingSpan;

        private TimeSpan _currentSpan;

        private TimeSpan _remainderSpan;

        private bool _isCounted;

        [Inject]
        public void Construct(TimeService timeService,
            AppInOutTimeService inOutTimeService,
            ChestsData chestsData
            )
        {
            Debug.Log("timer ctor");


            _timeService = timeService;

            _appInOutTimeService = inOutTimeService;

            _chestsData = chestsData;

            _chestsParams = _chestsData.GetChestParams(
                gameObject.GetComponent<Chest>().ChestID);
        }

        private void Awake()
        {
            _awaitingSpan = StructToTimeSpan(_chestsParams.TimeToOpen);

            ResetCounter();

            Debug.Log($"start {_startTime}");

            _currentSpan = _awaitingSpan - _appInOutTimeService.GetOfflineTime();

            CheckAndMakeOnCounted();
        }

        private void Update()
        {
            _currentSpan = _timeService.CurrentTime - _startTime;

            CheckAndMakeOnCounted();

            if (!_isCounted)
            {
                _remainderSpan = _awaitingSpan - _currentSpan;
            }
        }

        public void ResetCounter()
        {
            _isCounted = false;
            _startTime = _timeService.CurrentTime;
        }

        private void CheckAndMakeOnCounted()
        {
            if (_currentSpan > _awaitingSpan && !_isCounted)
            {
                Debug.Log($"chest {gameObject.name} is ready to open!");
                _isCounted = true;
                _remainderSpan = TimeSpan.Zero;

                OnCounted?.Invoke();
            }
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
}