using Scripts.MoneyNamespace;
using UnityEngine;

namespace Scripts.SaveLoadNamespace
{
    public class SaveLoadMoney : SaveLoad<MoneyStoragesData>
    {
        protected override string SaveLoadKey => "MoneyStorages";

        private readonly CurrencyConfig _config;

        public SaveLoadMoney(CurrencyConfig config)
        {
            _config = config;
        }

        protected override MoneyStoragesData LoadDefaultData()
        {
            var storageParams = new MoneyStorageParams[_config.Names.Length];

            Debug.Log(_config.Names.Length);

            for (int i = 0; i < storageParams.Length; i++)
            {
                var moneyParams = new MoneyStorageParams
                {
                    Currency = _config.Names[i],
                    Value = 0
                };

                storageParams[i] = moneyParams;
            }

            var data = new MoneyStoragesData();
            data.AddParamsData(storageParams);

            return data;
        }

        protected override void SetupLoadData(MoneyStoragesData data)
        {
            _data.SetupData();
        }

        protected override void SetupSaveData(MoneyStoragesData data)
        {
            _data.PrepareToSave();

            Debug.Log($"money save params {_data.ParamsStr()}");
        }
    }
}