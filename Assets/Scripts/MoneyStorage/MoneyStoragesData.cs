using Newtonsoft.Json;
using System.Collections.Generic;

namespace Scripts.MoneyNamespace
{
    public sealed class MoneyStoragesData
    {
        [JsonProperty]
        private readonly Dictionary<string, MoneyStorageParams> _moneyStoragesParams = new();

        private readonly Dictionary<string, MoneyStorage> _storages = new();

        public void SetupData()
        {
            _storages.Clear();

            foreach (var currency in _moneyStoragesParams.Keys)
            {
                var storage = new MoneyStorage(_moneyStoragesParams[currency]);
                _storages.Add(currency, storage);
            }
        }

        public void AddParamsData(MoneyStorageParams[] storagesParams)
        {
            _storages.Clear();

            for (int i = 0;  i < storagesParams.Length; i++)
            {
                _moneyStoragesParams.Add(storagesParams[i].Currency, storagesParams[i]);
            }
        }

        public void PrepareToSave()
        {
            foreach (var storage in _storages.Values)
            {
                var storageParams = new MoneyStorageParams
                {
                    Currency = storage.Currency,
                    Value = storage.Value
                };

                _moneyStoragesParams[storage.Currency] = storageParams;
            }
        }

        public MoneyStorage GetStorage(string currency)
        {
            return _storages[currency];
        }
    }
}