using TMPro;
using UnityEngine;

namespace Scripts.MoneyNamespace
{
    public class MoneyView : MonoBehaviour
    {
        public string Currency => _currency;

        [SerializeField]
        private TMP_Text _text;

        [SerializeField]
        private string _currency;

        public void SetupMoneyView(int prevValue, int newValue)
        {
            _text.text = $"{_currency}: {newValue}";
        }
    }
}