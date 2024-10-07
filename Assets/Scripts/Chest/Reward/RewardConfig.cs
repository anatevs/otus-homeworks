using System;
using UnityEngine;

namespace Scripts.Chest.Reward
{
    [CreateAssetMenu(fileName = "RewardConfig",
        menuName = "Configs/New RewardConfig")]

    public class RewardConfig : ScriptableObject
    {
        [SerializeField]
        private RewardParams[] _rewardParams;

        public (string Currency, int Value) GetRewardInfo()
        {
            var index = UnityEngine.Random.Range(0, _rewardParams.Length);

            var value = _rewardParams[index].GetRewardValue();

            return (_rewardParams[index].Currency, value);
        }
    }

    [Serializable]
    public struct RewardParams
    {
        public string Currency;

        public int[] _rewardRange;

        public int _valueStep;

        public int GetRewardValue()
        {
            var value = UnityEngine.Random.Range(_rewardRange[0], _rewardRange[1]);

            return value - value % _valueStep;
        }
    }
}