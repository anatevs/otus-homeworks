using Scripts.Chest.Reward;
using Scripts.MoneyNamespace;
using Scripts.SaveLoadNamespace;
using System;
using UnityEngine;
using VContainer;

namespace Scripts.Chest
{
    [RequireComponent(typeof(ChestTimer))]
    public class Chest : MonoBehaviour
    {
        public ChestParams ChestData => _chestsParams;

        public MoneyReward Reward => _reward;

        [SerializeField]
        private ChestConfig _chestConfig;

        [SerializeField]
        private RewardConfig _rewardConfig;

        private ChestTimer _chestTimer;

        private string _chestID;

        private SaveLoadChests _saveLoadChests;

        private MoneyStoragesData _moneyStorages;

        private MoneyReward _reward;

        private ChestParams _chestsParams;

        [Inject]
        public void Construct(SaveLoadChests saveLoadChests,
            SaveLoadMoney saveLoadMoney)
        {
            _saveLoadChests = saveLoadChests;

            _moneyStorages = saveLoadMoney.GetData();

            _chestID = _chestConfig.Params.ChestID;

            _chestsParams = _saveLoadChests.GetData().GetChestParams(_chestID);
        }

        private void Awake()
        {
            _chestTimer = gameObject.GetComponent<ChestTimer>();

            _reward = new MoneyReward(_chestsParams.RewardType, _chestsParams.RewardValue, _moneyStorages);
        }

        private void Update()
        {
            _chestsParams.RemainingTime = TimeSpanToStruct(_chestTimer.RemainderSpan);
            _saveLoadChests.SetChestData(_chestID, _chestsParams);
        }

        public void GoToNextReward()
        {
            _reward.MakeReward();

            var (Currency, Value) = _rewardConfig.GetRewardInfo();

            _reward = new MoneyReward(Currency, Value, _moneyStorages);

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