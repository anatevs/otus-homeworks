using Scripts.MoneyNamespace;
using UnityEngine;

namespace Scripts.Chest.Reward
{
    public sealed class MoneyReward : IReward
    {
        public string Currency => _currency;

        public int Value => _value;

        private readonly int _value;

        private readonly MoneyStorage _storage;


        private MoneyStoragesRepository _moneyStorages;
        private readonly string _currency;

        public MoneyReward(string currency, int value, MoneyStoragesRepository moneyStorages)
        {
            _value = value;

            _storage = moneyStorages.GetStorage(currency);


            _moneyStorages = moneyStorages;
            _currency = currency;
        }

        public void MakeReward()
        {
            _storage.Change(_value);

            Debug.Log($"{_currency} storage: {_moneyStorages.GetStorage(_currency).Amount}");
        }
    }
}