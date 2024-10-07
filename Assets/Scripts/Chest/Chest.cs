using Scripts.Chest.Reward;
using Scripts.MoneyNamespace;
using Scripts.SaveLoadNamespace;
using System;
using UnityEngine;
using VContainer;

namespace Scripts.Chest
{
    public class Chest : MonoBehaviour
    {
        public ChestParams ChestData => _chestsParams;

        [SerializeField]
        private ChestTimer _chestTimer;

        [SerializeField]
        private ChestConfig _chestConfig;

        [SerializeField]
        private RewardConfig _rewardConfig;

        private string _chestID;

        private SaveLoadChests _saveLoadChests;

        private MoneyStoragesRepository _moneyStorages;

        private MoneyReward _reward;

        private ChestParams _chestsParams;

        [Inject]
        public void Construct(SaveLoadChests saveLoadChests,
            MoneyStoragesRepository moneyStorages)
        {
            _saveLoadChests = saveLoadChests;

            _moneyStorages = moneyStorages;

            _chestID = _chestConfig.Params.ChestID;

            _chestsParams = _saveLoadChests.GetData().GetChestParams(_chestID);
        }

        private void OnEnable()
        {
            _reward = new MoneyReward(_chestsParams.RewardType, _chestsParams.RewardValue, _moneyStorages);
        }

        private void Update()
        {
            _chestsParams.RemainingTime = TimeSpanToStruct(_chestTimer.RemainderSpan);
            _saveLoadChests.SetChestData(_chestID, _chestsParams);
        }

        public void GoToNextReward()
        {
            //Debug.Log($"rewarded with {_chestsParams.RewardValue} {_chestsParams.RewardType} from chest {_chestID}");

            _reward.MakeReward();

            var rewardParams = _rewardConfig.GetRewardInfo();

            Debug.Log($"next reward is {rewardParams.Value} {rewardParams.Currency} for chest {_chestID}");
            _reward = new MoneyReward(rewardParams.Currency, rewardParams.Value, _moneyStorages);

            _chestsParams.RewardType = _reward.Currency;
            _chestsParams.RewardValue = _reward.Value;

            _saveLoadChests.SetChestData(_chestID, _chestsParams);
        }

        private TimeStruct TimeSpanToStruct(TimeSpan timeSpan)
        {
            return new TimeStruct()
            {
                Hours = timeSpan.Hours,
                Minutes = timeSpan.Minutes,
                Seconds = timeSpan.Seconds
            };
        }
    }
}