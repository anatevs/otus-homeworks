using Newtonsoft.Json;
using System;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System.Threading;
using VContainer.Unity;

namespace Scripts
{
    public class TimeService : ITickable //MonoBehaviour
    {
        public DateTime CurrentTime => _currentTime;

        private TimeServiceConfig _config;

        //[SerializeField]
        //private float _requestPeriod_Sec;

        //[SerializeField]
        //private int _allowedDiscrapancy_Sec = 5;

        private const string SERVICE_UTC_URL = "http://worldtimeapi.org/api/timezone/UTC";

        private DateTime _currentTime;

        private DateTime _currentUTCTime;

        private TimeSpan _startSpan;

        private TimeSpan _currentSpan;

        private TimeSpan _allowedDiscrepancy;

        private bool _isDeviceTimeCorrect;

        public TimeService(TimeServiceConfig config)
        {
            _config = config;
        }

        public async UniTask InitAsync()
        {
            _isDeviceTimeCorrect = true;
            _allowedDiscrepancy =
                TimeSpan.FromSeconds(_config.AllowedDiscrapancy_Sec);

            _startSpan = await GetLocalUTCDiffAsync();
            _currentTime = DateTime.Now;

            CheckDeviceTimeAsync().Forget();
        }

        //private void Update()
        void ITickable.Tick()
        {
            if (_isDeviceTimeCorrect)
            {
                _currentTime = DateTime.Now;
            }
        }

        private async UniTaskVoid CheckDeviceTimeAsync()
        {
            await UniTask.WaitForSeconds(_config.RequestPeriod_Sec);
            _currentSpan = await GetLocalUTCDiffAsync();

            if (TimeSpan.Compare(
                (_startSpan - _currentSpan).Duration(),
                _allowedDiscrepancy) == 1
                )
            {
                _isDeviceTimeCorrect = false;
                throw new Exception(
                    $"a system time has been changed for more than" +
                    $" {_allowedDiscrepancy.TotalMinutes} minutes");
            }
            else
            {
                CheckDeviceTimeAsync().Forget();
            }
        }

        private async UniTask<TimeSpan> GetLocalUTCDiffAsync()
        {
            _currentUTCTime = await RequestServerUTCTimeAsync();

            return (DateTime.Now - _currentUTCTime);
        }

        private async UniTask<DateTime> RequestServerUTCTimeAsync()
        {
            using (UnityWebRequest request = UnityWebRequest.Get(SERVICE_UTC_URL))
            {
                var operation = request.SendWebRequest();

                while (!operation.isDone)
                {
                    await UniTask.Yield();
                }

                if (request.result == UnityWebRequest.Result.Success)
                {
                    string timeJSON = request.downloadHandler.text;

                    var serverTime = JsonConvert.DeserializeObject<ServerTimeData>(timeJSON);

                    return DateTime.Parse(serverTime.datetime,
                        null,
                        System.Globalization.DateTimeStyles.AdjustToUniversal);
                }

                else
                {
                    return await RequestServerUTCTimeAsync();
                }
            }
        }
    }

    public class ServerTimeData
    {
        public string datetime;
    }
}