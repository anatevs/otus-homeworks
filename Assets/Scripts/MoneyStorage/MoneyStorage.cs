using System;

namespace Scripts.MoneyNamespace
{
    public sealed class MoneyStorage : IMoneyStorage
    {
        public event Action<int, int> OnMoneyChanged;

        public int Value => _value;

        public string Currency => _currency;

        private int _value;

        private string _currency;

        public MoneyStorage(MoneyStorageParams storageParams)
        {
            _currency = storageParams.Currency;
            _value = storageParams.Value;
        }

        public void Change(int diffAmount)
        {
            var prevValue = _value;
            _value += diffAmount;

            OnMoneyChanged?.Invoke(prevValue, _value);
        }
    }
}