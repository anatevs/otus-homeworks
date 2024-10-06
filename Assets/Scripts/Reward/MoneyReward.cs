using Scripts.MoneyNamespace;

namespace Scripts.Reward
{
    public class MoneyReward : IReward
    {
        public int RewardAmount => _amount;

        private readonly int _amount;

        private readonly string _currency;

        private readonly MoneyStorage _storage;

        public MoneyReward(string currency, int amount, MoneyStoragesRepository moneyStorages)
        {
            _currency = currency;
            _amount = amount;

            _storage = moneyStorages.GetStorage(currency);
        }

        public void MakeReward()
        {
            _storage.Change(_amount);
        }
    }
}