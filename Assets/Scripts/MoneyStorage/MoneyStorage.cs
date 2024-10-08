using System;

namespace Scripts.MoneyNamespace
{
    public sealed class MoneyStorage : IMoneyStorage
    {
        public event Action<int, int> OnMoneyChanged;

        public int Value => _value;

        public string Currency { get; }

        private int _value;

        private string _currency;

        public MoneyStorage(string currency)
        {
            _currency = currency;
        }

        public void Change(int diffAmount)
        {
            var prevValue = _value;
            _value += diffAmount;

            OnMoneyChanged?.Invoke(prevValue, _value);
        }
    }
}