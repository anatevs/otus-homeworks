using Scripts.SaveLoadNamespace;
using UnityEngine;
using VContainer;

namespace Scripts.MoneyNamespace
{
    [RequireComponent(typeof(MoneyView))]

    public class MoneyViewController : MonoBehaviour
    {
        private SaveLoadMoney _saveLoadMoney;

        private MoneyStorage _moneyStorage;

        private MoneyView _moneyView;

        [Inject]
        public void Construct(SaveLoadMoney saveLoadMoney)
        {
            _saveLoadMoney = saveLoadMoney;
        }

        private void Awake()
        {
            _moneyView = gameObject.GetComponent<MoneyView>();

            _moneyStorage = _saveLoadMoney.GetData().GetStorage(_moneyView.Currency);
        }

        private void OnEnable()
        {
            _moneyStorage.OnMoneyChanged += _moneyView.SetupMoneyView;

            _moneyStorage.Change(0);
        }

        private void OnDisable()
        {
            _moneyStorage.OnMoneyChanged -= _moneyView.SetupMoneyView;
        }

    }
}