using UnityEngine;

namespace Scripts
{
    [CreateAssetMenu(fileName = "TimeServiceConfig",
        menuName = "Configs/New TimeServiceConfig")]
    public class TimeServiceConfig : ScriptableObject
    {
        public float RequestPeriod_Sec;

        public int AllowedDiscrapancy_Sec;
    }
}