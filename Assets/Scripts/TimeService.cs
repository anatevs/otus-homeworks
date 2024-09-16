using Newtonsoft.Json;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Scripts
{
    public class TimeService : MonoBehaviour
    {
        public DateTime CurrentTime => _currentTime;

        public float RequestPeriod_Sec => _requestPeriod_Sec;

        [SerializeField]
        private float _requestPeriod_Sec = 1;

        private const string SERVICE_NAME = "http://worldtimeapi.org/api/ip";

        private DateTime _currentTime;

        private DateTime _currentServerTime;

        private ServerTimeData _serverTimeData;

        private TimeSpan _allowedDifference = TimeSpan.FromMinutes(5);

        private float _periodCounter;

        private void Awake()
        {
            _currentTime = DateTime.Now;
            _periodCounter = _requestPeriod_Sec;
        }

        private void Start()
        {
            StartCoroutine(RequestTime());
        }

        private void Update()
        {
            _periodCounter += Time.deltaTime;

            if (_periodCounter >= RequestPeriod_Sec )
            {
                _currentTime = DateTime.Now;
                _periodCounter = 0;
            }
        }

        private IEnumerator RequestTime()
        {
            UnityWebRequest request = UnityWebRequest.Get(SERVICE_NAME);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string timeJSON = request.downloadHandler.text;
                _serverTimeData = JsonConvert.DeserializeObject<ServerTimeData>(timeJSON);

                _currentServerTime = DateTime.Parse(_serverTimeData.datetime);
                Debug.Log(_currentServerTime);

                yield return new WaitForSeconds(RequestPeriod_Sec);
                StartCoroutine(RequestTime());
                yield break;
            }
            else
            {
                Debug.Log("server reading error");

                yield return new WaitForSeconds(RequestPeriod_Sec);
                StartCoroutine(RequestTime());
            }
        }
    }

    public class ServerTimeData
    {
        public string datetime;
    }
}