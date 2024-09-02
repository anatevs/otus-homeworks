using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Upgrades
{
    public class MoneyStorage : MonoBehaviour, IMoneyStorage
    {
        public event Action<int> OnMoneyChanged;
        public event Action<int> OnMoneyEarned;
        public event Action<int> OnMoneySpent;

        public int Money => _money;

        [ShowInInspector, ReadOnly]
        private int _money;
        
        public void EarnMoney(int amount)
        {
            _money += amount;

            OnMoneyEarned(_money);
            OnMoneyChanged(_money);
        }

        public void SpendMoney(int amount)
        {
            _money -= amount;

            OnMoneySpent(_money);
            OnMoneyChanged(_money);
        }

        public bool CanSpendMoney(int amount)
        {
            var result = amount <= _money;
            Debug.Log($"Can spend {amount} of all money {_money} = {result}");
            return result;
        }
    }
}