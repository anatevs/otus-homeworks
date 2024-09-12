using System;
using UnityEngine;

namespace Scripts
{
    public class TimeService : MonoBehaviour
    {
        public DateTime CurrentTime => _currentTime;

        public float RequestPeriod_Sec => _requestPeriod_Sec;

        [SerializeField]
        private float _requestPeriod_Sec = 1;

        private DateTime _currentTime;

        private float _periodCounter;

        private void Awake()
        {
            _currentTime = DateTime.Now;
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
    }
}