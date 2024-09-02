using Sirenix.OdinInspector;
using UnityEngine;

namespace Upgrades
{
    public class MoneyStorageHelper : MonoBehaviour
    {
        [SerializeField]
        private MoneyStorage _moneyStorage;

        private void Awake()
        {
            _moneyStorage.EarnMoney(1000);
        }

        [Button]
        public void EarnMoney(int amount)
        {
            _moneyStorage.EarnMoney(amount);
        }
    }
}