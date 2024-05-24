
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Sample
{
    [Serializable]
    public class EquipmentComponent
    {
        public string StatName
        {
            get => _statName;
            private set => _ = _statName;
        }

        [field: SerializeField]
        public EquipmentType Type { get; private set; }

        [SerializeField, ValueDropdown("Stats")]
        private string _statName;

        [field: SerializeField]
        public float Value { get; private set; }

#if UNITY_EDITOR
        public static string[] Stats;
#endif
    }
}