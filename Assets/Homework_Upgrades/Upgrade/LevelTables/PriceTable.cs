using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace Assets.Homework_Upgrades.Upgrade
{
    [CreateAssetMenu(fileName = "PriceTable",
        menuName = "Configs/Upgrade Level Tables/New PriceTable"
        )]
    public class PriceTable : ScriptableObject
    {
        [SerializeField]
        private float _basePrice;

        private readonly int _levelMin = 1;

        [ShowInInspector, ReadOnly]
        private float[] _table;

        public void Init(int levelMax)
        {
            var length = levelMax - _levelMin + 1;
            _table = new float[length];
            _table[0] = 0;

            for (int i = 1; i < length; i++)
            {
                _table[i] = EvaluatePrice(i + _levelMin);
            }
        }

        public float GetPrice(int level)
        {
            return _table[level - _levelMin];
        }

        private float EvaluatePrice(int level)
        {
            return _basePrice * Mathf.Pow( _basePrice, level - 2);
        }
    }
}