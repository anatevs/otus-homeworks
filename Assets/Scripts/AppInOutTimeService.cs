using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class AppInOutTimeService
    {
        private AppInOutStruct _info = new()
        {
            AppIn = new(),
            AppOut = new()
        };

        public TimeSpan GetOfflineTime()
        {
            if (_info.AppOut.Count == 0)
            {
                return new TimeSpan(0, 0, 0);
            }
            else
            {
                var logOut = DateTime.Parse(_info.AppOut[^1]);

                var logIn = DateTime.Parse(_info.AppIn[^1]);

                return logIn - logOut;
            }
        }
    }

    [Serializable]
    public struct AppInOutStruct
    {
        public List<string> AppIn; //utc

        public List<string> AppOut; //utc
    }
}