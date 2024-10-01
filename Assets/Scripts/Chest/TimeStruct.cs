using System;
using UnityEngine;

namespace Scripts.Chest
{
    [Serializable]
    public struct TimeStruct
    {
        [field: SerializeField]
        public int Hours { get; set; }

        [field: SerializeField]
        public int Minutes { get; set; }

        [field: SerializeField]
        public int Seconds { get; set; }
    }
}