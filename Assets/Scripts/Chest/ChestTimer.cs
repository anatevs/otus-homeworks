using Scripts.SaveLoadNamespace;
using System;
using UnityEngine;
using VContainer;

namespace Scripts.Chest
{
    public sealed class ChestTimer : MonoBehaviour
    {
        public event Action OnCounted;

        public TimeSpan RemainderSpan => _remainderSpan;

        private TimeService _timeService;

        private SaveLoadStartFinishTime _startFinishTime;

        private DateTime _startTime;

        private TimeSpan _awaitingSpan;

        private TimeSpan _currentSpan;

        private TimeSpan _remainderSpan;

        private bool _isCounted;

        [Inject]
        public void Construct(TimeService timeService,
            SaveLoadStartFinishTime startFinishTime
            )
        {
            _timeService = timeService;

            _startFinishTime = startFinishTime;
        }

        private void Start()
        {
            var chest = gameObject.GetComponent<Chest>();

            _awaitingSpan = StructToTimeSpan(chest.ChestData.AwaitingTime);
            _remainderSpan = StructToTimeSpan(chest.ChestData.RemainingTime);

            var offlineTime = _startFinishTime.GetData().GetOfflineTime();

            _startTime = _timeService.CurrentTime - (offlineTime + _awaitingSpan - _remainderSpan);
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
            _currentSpan = _timeService.CurrentTime - _startTime;

            //new chest params... (reward type and amount, upd timeToOpen)
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
    }
}