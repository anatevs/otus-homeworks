using System;

namespace Scripts.MoneyNamespace
{
    public interface IMoneyStorage
    {
        public event Action<int, int> OnMoneyChanged;

        public string Currency { get; }

        public int Amount { get; }

        public void Change(int diffAmount);
    }
}