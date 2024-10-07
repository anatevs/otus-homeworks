using System;

namespace Scripts.MoneyNamespace
{
    public sealed class MoneyStorage : IMoneyStorage
    {
        public event Action<int, int> OnMoneyChanged;

        public int Amount => _amount;

        public string Currency { get; }

        private int _amount;

        private string _currency;

        public MoneyStorage(string currency)
        {
            _currency = currency;
        }

        public void Change(int diffAmount)
        {
            var oldAmount = _amount;
            _amount += diffAmount;

            OnMoneyChanged?.Invoke(oldAmount, _amount);
        }
    }
}