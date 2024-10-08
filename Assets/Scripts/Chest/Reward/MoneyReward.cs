using Scripts.MoneyNamespace;
using Scripts.SaveLoadNamespace;
using UnityEngine;

namespace Scripts.Chest.Reward
{
    public sealed class MoneyReward : IReward
    {
        public string Currency => _currency;

        public int Value => _value;

        private readonly int _value;

        private SaveLoadMoney _saveLoadMoney;
        //private readonly MoneyStorage _storage;


        private readonly MoneyStoragesData _moneyStorages;
        private readonly string _currency;

        public MoneyReward(string currency, int value, SaveLoadMoney saveLoadMoney)
        {
            _value = value;

            _saveLoadMoney = saveLoadMoney;
            //_storage = moneyStorages.GetStorage(currency);


            _moneyStorages = _saveLoadMoney.GetData();
            _currency = currency;
        }

        public void MakeReward()
        {
            _saveLoadMoney.GetData().GetStorage(_currency).Change(_value);

            Debug.Log($"{_currency} storage: {_moneyStorages.GetStorage(_currency).Value}");
        }
    }
}