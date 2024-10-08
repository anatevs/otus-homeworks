using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.MoneyNamespace
{
    public sealed class MoneyStoragesData
    {
        [JsonProperty]
        private Dictionary<string, MoneyStorageParams> _moneyStoragesParams = new();

        private readonly Dictionary<string, MoneyStorage> _storages = new();

        public void SetupData()
        {
            _storages.Clear();

            if (_moneyStoragesParams != null)
            {
                foreach (var currency in _moneyStoragesParams.Keys)
                {
                    var storage = new MoneyStorage(_moneyStoragesParams[currency]);
                    _storages.Add(currency, storage);
                }
                Debug.Log($"set number of moneyStorages are {_moneyStoragesParams.Count}");
            }
            else
            {
                Debug.Log("empty money storages params");
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

        //public void AddData(MoneyStorage storage)
        //{
        //    var storageParams = new MoneyStorageParams
        //    {
        //        Currency = storage.Currency,
        //        Value = storage.Value
        //    };

        //    if (_storages.ContainsKey(storage.Currency))
        //    {
        //        _storages[storage.Currency] = storage;

        //        _moneyStoragesParams[storage.Currency] = storageParams;
        //    }
        //    else
        //    {
        //        _storages.Add(storage.Currency, storage);

        //        _moneyStoragesParams.Add(storage.Currency, storageParams);
        //    }
        //}

        public MoneyStorage GetStorage(string currency)
        {
            return _storages[currency];
        }

        public string ParamsStr()
        {
            var res = "";

            foreach (var storage in _moneyStoragesParams.Values)
            {
                var restemp = res + $"{storage.Currency}: {storage.Value};";
                res = restemp;
            }

            return res;
        }
    }
}