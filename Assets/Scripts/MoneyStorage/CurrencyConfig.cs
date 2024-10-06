using UnityEngine;

namespace Scripts.MoneyNamespace
{
    [CreateAssetMenu(fileName = "CurrencyNames",
        menuName = "Configs/New CurrencyNames"
        )]

    public class CurrencyConfig : ScriptableObject
    {
        public string[] Names;
    }
}