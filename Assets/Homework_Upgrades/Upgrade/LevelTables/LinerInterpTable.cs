using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace Assets.Homework_Upgrades.Upgrade.LevelTables
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

        [ShowInInspector, ReadOnly]
        private float[] _table;

        private readonly int _levelMin = 1;

        private float _slopeTangent;

        public void Init(int levelMax)
        {
            _slopeTangent = (_endValue - _startValue) / (float)(levelMax - _levelMin);

            var length = levelMax - _levelMin + 1;

            _table = new float[length];
            for (int i = 0; i < length; i++)
            {
                _table[i] = EvaluateValue(i + 1);
            }
        }

        public float GetValue(int level)
        {
            return _table[level - _levelMin];
        }

        private float EvaluateValue(int level)
        {
            return _slopeTangent * (float)(level - _levelMin) + _startValue;
        }
    }
}