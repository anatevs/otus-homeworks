using UnityEngine;
using VContainer;

namespace Scripts.MoneyNamespace
{
    [RequireComponent(typeof(MoneyView))]

    public class MoneyViewController : MonoBehaviour
    {
        private MoneyStoragesRepository _storagesRepository;

        private MoneyStorage _moneyStorage;

        private MoneyView _moneyView;

        [Inject]
        public void Construct(MoneyStoragesRepository moneyStoragesRepository)
        {
            _storagesRepository = moneyStoragesRepository;
        }

        private void Awake()
        {
            _moneyView = gameObject.GetComponent<MoneyView>();

            _moneyStorage = _storagesRepository.GetStorage(_moneyView.Currency);
        }

        private void OnEnable()
        {
            _moneyStorage.OnMoneyChanged += _moneyView.SetupMoneyView;
        }

        private void OnDisable()
        {
            _moneyStorage.OnMoneyChanged -= _moneyView.SetupMoneyView;
        }

    }
}