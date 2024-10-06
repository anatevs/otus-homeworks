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

        private string _chestID;

        private TimeService _timeService;

        private SaveLoadStartFinishTime _startFinishTime;

        private SaveLoadChests _saveLoadChests;

        private ChestParams _chestsParams;

        private DateTime _startTime;

        private TimeSpan _awaitingSpan;

        private TimeSpan _currentSpan;

        private TimeSpan _remainderSpan;

        private bool _isCounted;

        [Inject]
        public void Construct(TimeService timeService,
            SaveLoadChests saveLoadChests,
            SaveLoadStartFinishTime srartFinishTime
            )
        {
            _timeService = timeService;

            _startFinishTime = srartFinishTime;

            _saveLoadChests = saveLoadChests;

            _chestID = gameObject.GetComponent<Chest>().ChestID;

            _chestsParams = _saveLoadChests.GetData().GetChestParams(_chestID);
        }

        private void Start()
        {
            _awaitingSpan = StructToTimeSpan(_chestsParams.AwaitingTime);
            _remainderSpan = StructToTimeSpan(_chestsParams.RemainingTime);

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

            _chestsParams.RemainingTime = TimeSpanToStruct(_remainderSpan);
            _saveLoadChests.SetChestData(_chestID, _chestsParams);
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