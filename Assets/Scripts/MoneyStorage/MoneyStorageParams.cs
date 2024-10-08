using System;

namespace Scripts.MoneyNamespace
{
    [Serializable]
    public struct MoneyStorageParams
    {
        public string Currency;

        public int Value;
    }
}