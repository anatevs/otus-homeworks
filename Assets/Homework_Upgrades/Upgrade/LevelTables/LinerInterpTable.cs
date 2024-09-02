using Sirenix.OdinInspector;
using UnityEngine;

namespace LevelTables
{
    [CreateAssetMenu(fileName = "LinerInterpTable",
        menuName = "Configs/Upgrade Level Tables/New LinerIntepTable"
        )]

    public class LinerInterpTable : ScriptableObject
    {
        [SerializeField]
        private bool _isIntValues;

        [SerializeField]
        private float _startValue;

        [SerializeField]
        private float _endValue;

        private readonly int _levelMin = 1;

        [ShowInInspector, ReadOnly]
        private int _levelMax = 2;

        [ShowInInspector, ReadOnly]
        private float[] _table;

        private int[] _intTable;

        private float _slopeTangent;

        private void OnValidate()
        {
            Init(_levelMax);
        }

        public void Init(int levelMax)
        {
            _levelMax = levelMax;

            _slopeTangent = (_endValue - _startValue) / (float)(levelMax - _levelMin);

            var length = levelMax - _levelMin + 1;

            _table = new float[length];

            for (int i = 0; i < length; i++)
            {
                var value = EvaluateValue(i + 1);

                if (_isIntValues)
                {
                    value = (int)value;
                }

                _table[i] = value;
            }

            if (_isIntValues)
            {
                _intTable = new int[length];

                for (int i = 0; i < length; i++)
                {
                    _intTable[i] = (int)_table[i];
                }
            }
        }

        public float GetValue(int level)
        {
            if (_isIntValues)
            {
                throw new System.Exception("you request a float value from the table that suppose to contain int values");
            }
            return _table[GetLevelIndex(level)];
        }

        public int GetValueInt(int level)
        {
            return _intTable[GetLevelIndex(level)];
        }

        private int GetLevelIndex(int level)
        {
            return level - _levelMin;
        }

        private float EvaluateValue(int level)
        {
            return _slopeTangent * (float)(level - _levelMin) + _startValue;
        }
    }
}