using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sample
{
    [CreateAssetMenu(fileName = "InitStats",
        menuName = "Sample/new InitStats")]
    public sealed class InitCharacterStats : ScriptableObject
    {
        [SerializeField]
        private StatStruct[] _statStructs;

        public KeyValuePair<string, float>[] GetInitStats()
        {
            KeyValuePair<string, float>[] res = new KeyValuePair<string, float>[_statStructs.Length];

            for (int i = 0; i < _statStructs.Length; i++)
            {
                string statName = _statStructs[i].Name;
                res[i] = new KeyValuePair<string, float>(statName, _statStructs[i].Value);
            }

            return res;
        }

#if UNITY_EDITOR
        [Button]
        public void SetStatNames()
        {
            EquipmentComponent.Stats = new string[_statStructs.Length];
            for (int i = 0; i < _statStructs.Length; i++)
            {
                EquipmentComponent.Stats[i] = _statStructs[i].Name;
            }
        }
#endif
    }

    [Serializable]
    public struct StatStruct
    {
        [field: SerializeField]
        public string Name { get; private set; }

        [field: SerializeField]
        public float Value { get; private set; }
    }
}