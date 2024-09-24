using System;

namespace Scripts
{
    [Serializable]
    public struct TimeServiceConfig
    {
        public float RequestPeriod_Sec;

        public int AllowedDiscrapancy_Sec;
    }
}