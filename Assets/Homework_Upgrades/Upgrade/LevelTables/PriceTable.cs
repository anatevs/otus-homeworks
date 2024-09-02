using Sirenix.OdinInspector;
using UnityEngine;

namespace Upgrades
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
        private int _levelMax = 2;

        [ShowInInspector, ReadOnly]
        private float[] _table;

        private void OnValidate()
        {
            Init(_levelMax);
        }

        public void Init(int levelMax)
        {
            _levelMax = levelMax;

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