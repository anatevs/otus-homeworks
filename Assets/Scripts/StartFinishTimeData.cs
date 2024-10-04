using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Scripts
{
    public sealed class StartFinishTimeData
    {
        [JsonProperty]
        private AppStartFinishTimeStruct _info = new()
        {
            AppStart = new(),
            AppFinish = new()
        };

        public void AddStartTime(DateTime time)
        {
            var utcStr = GetUTCString(time);

            _info.AppStart.Add(utcStr);
        }

        public void AddFinishTime(DateTime time)
        {
            var utcStr = GetUTCString(time);

            _info.AppFinish.Add(utcStr);
        }

        public AppStartFinishTimeStruct GetLocalTimeStrings()
        {
            var res = new AppStartFinishTimeStruct();

            foreach (var item in _info.AppStart)
            {
                res.AppStart.Add(GetLocalFromUTCString(item));
            }

            foreach (var item in _info.AppFinish)
            {
                res.AppFinish.Add(GetLocalFromUTCString(item));
            }
            return res;
        }

        public List<string> GetStartLocalTimeStrings()
        {
            var res = new List<string>();

            foreach (var item in _info.AppStart)
            {
                res.Add(GetLocalFromUTCString(item));
            }

            return res;
        }

        public List<string> GetFinishLocalTimeStrings()
        {
            var res = new List<string>();

            foreach (var item in _info.AppFinish)
            {
                res.Add(GetLocalFromUTCString(item));
            }

            return res;
        }

        public TimeSpan GetOfflineTime()
        {
            if (_info.AppFinish.Count == 0)
            {
                return new TimeSpan(0, 0, 0);
            }
            else
            {
                var finishTime = DateTime.Parse(_info.AppFinish[^1]);

                var startTime = DateTime.Parse(_info.AppStart[^1]);

                return startTime - finishTime;
            }
        }

        private string GetLocalFromUTCString(string utcStr)
        {
            return GetLocalTime(utcStr).ToString();
        }

        private string GetUTCString(DateTime time)
        {
            return time.ToUniversalTime().ToString();
        }

        private DateTime GetLocalTime(string utcStr)
        {
            return DateTime.Parse(utcStr).ToLocalTime();
        }
    }

    [Serializable]
    public struct AppStartFinishTimeStruct
    {
        public List<string> AppStart; //utc

        public List<string> AppFinish; //utc
    }
}