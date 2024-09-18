using Newtonsoft.Json;
using System;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Scripts
{
    public class TimeService : MonoBehaviour
    {
        public DateTime CurrentTime => _currentTime;

        public float RequestPeriod_Sec => _requestPeriod_Sec;

        [SerializeField]
        private float _requestPeriod_Sec;

        [SerializeField]
        private int _allowedDiscrapancy_Sec = 5;

        private const string SERVICE_UTC_URL = "http://worldtimeapi.org/api/timezone/UTC";

        private DateTime _currentTime;

        private DateTime _currentUTCTime;

        private TimeSpan _startSpan;

        private TimeSpan _currentSpan;

        private TimeSpan _allowedDiscrepancy;

        private bool _isDeviceTimeCorrect;

        private CancellationTokenSource _cancellation = new();

        private void Awake()
        {
            _isDeviceTimeCorrect = true;
            _allowedDiscrepancy = 
                TimeSpan.FromMinutes(_allowedDiscrapancy_Sec);
        }

        private async void Start()
        {
            _startSpan = await GetSpanLocalUTC();
            _currentTime = DateTime.Now;

            CheckDeviceTime().Forget();
        }

        private void Update()
        {
            if (_isDeviceTimeCorrect)
            {
                _currentTime = DateTime.Now;
            }
        }

        private async UniTaskVoid CheckDeviceTime()
        {
            await UniTask.WaitForSeconds(_requestPeriod_Sec);
            _currentSpan = await GetSpanLocalUTC();

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
                CheckDeviceTime().Forget();
            }
        }

        private async UniTask<TimeSpan> GetSpanLocalUTC()
        {
            _currentUTCTime = await RequestServerTime();

            return (DateTime.Now - _currentUTCTime);
        }

        private async UniTask<DateTime> RequestServerTime()
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

                    //Debug.Log($"server: {serverTime.datetime}");

                    return DateTime.Parse(serverTime.datetime,
                        null,
                        System.Globalization.DateTimeStyles.AdjustToUniversal);
                }

                else
                {
                    return await RequestServerTime();
                }
            }
        }
    }

    public class ServerTimeData
    {
        public string datetime;
    }
}