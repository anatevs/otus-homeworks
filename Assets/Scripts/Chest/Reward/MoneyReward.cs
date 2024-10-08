using Scripts.MoneyNamespace;

namespace Scripts.Chest.Reward
{
    public sealed class MoneyReward : IReward
    {
        public string Currency => _currency;

        public int Value => _value;

        private readonly string _currency;

        private readonly int _value;

        private readonly MoneyStoragesData _moneyStorages;

        public MoneyReward(string currency, int value, MoneyStoragesData moneyStorages)
        {
            _value = value;

            _moneyStorages = moneyStorages;
            _currency = currency;
        }

        public void MakeReward()
        {
            _moneyStorages.GetStorage(_currency).Change(_value);
        }
    }
}