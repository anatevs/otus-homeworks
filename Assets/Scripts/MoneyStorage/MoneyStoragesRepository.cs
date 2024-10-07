using System.Collections.Generic;
using UnityEngine;

namespace Scripts.MoneyNamespace
{
    public sealed class MoneyStoragesRepository
    {
        public string[] Currencies => _currensies;

        private readonly Dictionary<string, MoneyStorage> _storages = new();

        private readonly string[] _currensies;

        public MoneyStoragesRepository(CurrencyConfig config)
        {
            _currensies = config.Names;

            foreach (var currency in _currensies)
            {
                var storage = new MoneyStorage(currency);
                _storages.Add(currency, storage);
            }
        }

        public MoneyStorage GetStorage(string currency)
        {
            return _storages[currency];
        }
    }
}