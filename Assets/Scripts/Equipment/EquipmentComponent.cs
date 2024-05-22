using System;
using UnityEngine;

namespace Sample
{
    [Serializable]
    public class EquipmentComponent
    {
        [field: SerializeField]
        public EquipmentType Type { get; private set; }

        [field: SerializeField]
        public string StatName { get; private set; }

        [field: SerializeField]
        public float Value { get; private set; }
    }
}