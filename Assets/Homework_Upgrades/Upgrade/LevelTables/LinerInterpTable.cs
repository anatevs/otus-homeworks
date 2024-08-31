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
        private float _startValue;

        [SerializeField]
        private float _endValue;

        private readonly int _levelMin = 1;

        [ShowInInspector, ReadOnly]
        private int _levelMax = 2;

        [ShowInInspector, ReadOnly]
        private float[] _table;

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
                _table[i] = (int)EvaluateValue(i + 1);
            }
        }

        public float GetValue(int level)
        {
            return _table[level - _levelMin];
        }

        public int GetValueInt(int level)
        {
            return (int)GetValue(level);
        }

        private float EvaluateValue(int level)
        {
            return _slopeTangent * (float)(level - _levelMin) + _startValue;
        }
    }
}